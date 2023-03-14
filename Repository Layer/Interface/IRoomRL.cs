namespace RepoLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;

    /// <summary>room
    ///  Interface class
    /// </summary>
    public interface IRoomRL
    {

        public RoomModel Addroom(RoomModel room);



        public RoomModel UpdateRoom(RoomModel room);

        
        public bool DeleteRoom(int roomId);

        
        public RoomModel GetRoomByRoomId(int roomId);

        
        public List<RoomModel> GetAllRooms();
    }
}