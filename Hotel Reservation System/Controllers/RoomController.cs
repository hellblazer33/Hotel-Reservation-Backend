namespace BookStore.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;

    /// <summary>
    ///  Book Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {

        private readonly IRoomBL roomBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public RoomController(IRoomBL roomBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.roomBL = roomBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }


        [Authorize(Roles = Role.Admin)]
        [HttpPost("AddRoom")]
        public IActionResult AddRoom(RoomModel room)
        {
            try
            {
                var roomDetail = this.roomBL.Addroom(room);
                if (roomDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Room Added Sucessfully", Response = roomDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Failed to add Room" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

       
        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateRoom")]
        public IActionResult UpdateRoom(RoomModel room)
        {
            try
            {
                var updatedRoomDetail = this.roomBL.UpdateRoom(room);
                if (updatedRoomDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Room Updated Sucessfully", Response = updatedRoomDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Failed to update Room" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteRoom")]
        public IActionResult DeleteBook(int roomId)
        {
            try
            {
                if (this.roomBL.DeleteRoom(roomId))
                {
                    return this.Ok(new { Success = true, message = "Room Deleted Sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Room Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }


        //[Authorize(Roles = Role.User)]
        [Authorize]
        [HttpGet("{roomId}/Get")]
        public IActionResult GetRoomByRoomId(int roomId)
        {
            try
            {
                var updatedRoomDetail = this.roomBL.GetRoomByRoomId(roomId);
                if (updatedRoomDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Room Detail Fetched Sucessfully", Response = updatedRoomDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Correct Room Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        
        //[Authorize(Roles = Role.User)]
        //[Authorize]
        [HttpGet("redisGetAllRooms")]
        public async Task<IActionResult> GetAllBooksUsingRedisCache()
        {
            var cacheKey = "RoomList";
            string serializedRoomList;
            var RoomList = new List<RoomModel>();
            var redisRoomList = await distributedCache.GetAsync(cacheKey);
            if (redisRoomList != null)
            {
                serializedRoomList = Encoding.UTF8.GetString(redisRoomList);
                RoomList = JsonConvert.DeserializeObject<List<RoomModel>>(serializedRoomList);
            }
            else
            {
                RoomList = (List<RoomModel>)roomBL.GetAllRooms();
                serializedRoomList = JsonConvert.SerializeObject(RoomList);
                redisRoomList = Encoding.UTF8.GetBytes(serializedRoomList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisRoomList, options);
            }
            return Ok(RoomList);
        }
    }
}