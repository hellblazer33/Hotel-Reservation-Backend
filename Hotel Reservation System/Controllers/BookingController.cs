namespace BookStore.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    
    [Route("api/[controller]")]
    [Authorize(Roles = Role.User)]
    [ApiController]
    public class BookingController : ControllerBase
    {
        
        private readonly IBookingBL bookingBL;

      
        public BookingController(IBookingBL bookingBL)
        {
            this.bookingBL = bookingBL;
        }

       
        [HttpPost("AddBooking")]
        public IActionResult AddBooking(BookingModel ordersModel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "Id").Value);
                var bookingData = this.bookingBL.AddBooking(ordersModel, userId);
                if (bookingData != null)
                {
                    return this.Ok(new { Status = true, Message = "Booking Placed Successfully", Response = bookingData });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Room not booked" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete("DeleteBooking")]
        public IActionResult DeleteBooking(int bookingId)
        {
            try
            {
                if (this.bookingBL.DeleteBooking(bookingId))
                {
                    return this.Ok(new { Success = true, message = "Booking Deleted Sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Booking Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }


        [HttpGet("GetBookings")]
        public IActionResult GetAllBookings()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "Id").Value);
                var bookingData = this.bookingBL.GetAllBookings(userId);
                if (bookingData != null)
                {
                    return this.Ok(new { Status = true, Message = "Bookings Fetched Successfully", Response = bookingData });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Please Login First" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, ex.Message });
            }
        }
    }
}