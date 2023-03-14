namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;

    
    public interface IRoomBL
    {

        public RoomModel Addroom(RoomModel room);



        public RoomModel UpdateRoom(RoomModel room);


        public bool DeleteRoom(int roomId);


        public RoomModel GetRoomByRoomId(int roomId);


        public List<RoomModel> GetAllRooms();
    }
}