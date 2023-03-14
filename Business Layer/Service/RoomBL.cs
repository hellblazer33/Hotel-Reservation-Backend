namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using RepoLayer.Interface;

    /// <summary>
    ///  Service class for Interface 
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.IBookBL" />
    public class RoomBL : IRoomBL
    {
       
        private readonly IRoomRL roomRL;

        public RoomBL(IRoomRL roomRL)
        {
            this.roomRL = roomRL;
        }

        
        public RoomModel Addroom(RoomModel room)
        {
            try
            {
                return this.roomRL.Addroom(room);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public bool DeleteRoom(int roomId)
        {
            try
            {
                return this.roomRL.DeleteRoom(roomId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public List<RoomModel> GetAllRooms()
        {
            try
            {
                return this.roomRL.GetAllRooms();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public RoomModel GetRoomByRoomId(int roomId)
        {
            try
            {
                return this.roomRL.GetRoomByRoomId(roomId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public RoomModel UpdateRoom(RoomModel room)
        {
            try
            {
                return this.roomRL.UpdateRoom(room);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}