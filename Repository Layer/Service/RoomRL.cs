namespace RepoLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    //using CloudinaryDotNet;
    //using CloudinaryDotNet.Actions;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using RepoLayer.Interface;

    /// <summary>
    ///  Service class for Interface 
    /// </summary>
    /// <seealso cref="RepoLayer.Interface.IroomRL" />
    public class RoomRL : IRoomRL
    {
        
        private SqlConnection sqlConnection;

       
        public RoomRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

      
        private IConfiguration Configuration { get; }

      
        public RoomModel Addroom(RoomModel room)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:HotelManagement"]);
                SqlCommand cmd = new SqlCommand("AddRoom", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                
                cmd.Parameters.AddWithValue("@rating", room.Rating);
                cmd.Parameters.AddWithValue("@roomNumber", room.RoomNumber);
                cmd.Parameters.AddWithValue("@totalRating", room.TotalRating);
                cmd.Parameters.AddWithValue("@discountPrice", room.DiscountPrice);
                cmd.Parameters.AddWithValue("@originalPrice", room.OriginalPrice);
                cmd.Parameters.AddWithValue("@description", room.Description);
               
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return room;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        
        public RoomModel UpdateRoom(RoomModel room)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:HotelManagement"]);
                SqlCommand cmd = new SqlCommand("UpdateRoom", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@roomId", room.RoomId);
                cmd.Parameters.AddWithValue("@roomNumber", room.RoomNumber);
                
                cmd.Parameters.AddWithValue("@rating", room.Rating);
                cmd.Parameters.AddWithValue("@totalRating", room.TotalRating);
                cmd.Parameters.AddWithValue("@discountPrice", room.DiscountPrice);
                cmd.Parameters.AddWithValue("@originalPrice", room.OriginalPrice);
                cmd.Parameters.AddWithValue("@description", room.Description);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return room;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

       
        public bool DeleteRoom(int roomId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:HotelManagement"]);
                SqlCommand cmd = new SqlCommand("DeleteRoom", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@roomId", roomId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        
        public RoomModel GetRoomByRoomId(int roomId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:HotelManagement"]);
                SqlCommand cmd = new SqlCommand("GetroomByroomId", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@roomId", roomId);
                this.sqlConnection.Open();
                RoomModel roomModel = new RoomModel();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roomModel.RoomId = Convert.ToInt32(reader["roomId"]);
                        roomModel.RoomNumber = Convert.ToInt32(reader["roomNumber"]);
                        roomModel.Rating = Convert.ToInt32(reader["rating"]);
                        roomModel.TotalRating = Convert.ToInt32(reader["totalRating"]);
                        roomModel.DiscountPrice = Convert.ToDecimal(reader["discountPrice"]);
                        roomModel.OriginalPrice = Convert.ToDecimal(reader["originalPrice"]);
                        roomModel.Description = reader["description"].ToString();
                        
                    }

                    this.sqlConnection.Close();
                    return roomModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

       
        public List<RoomModel> GetAllRooms()
        {
            try
            {
                List<RoomModel> room = new List<RoomModel>();
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:HotelManagement"]);
                SqlCommand cmd = new SqlCommand("GetAllRooms", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        room.Add(new RoomModel
                        {
                            RoomId = Convert.ToInt32(reader["roomId"]),
                            RoomNumber = Convert.ToInt32(reader["roomNumber"]),
                            Rating = Convert.ToInt32(reader["rating"]),
                            TotalRating = Convert.ToInt32(reader["totalRating"]),
                            DiscountPrice = Convert.ToDecimal(reader["discountPrice"]),
                            OriginalPrice = Convert.ToDecimal(reader["originalPrice"]),
                            Description = reader["description"].ToString(),
                           
                        });
                    }

                    this.sqlConnection.Close();
                    return room;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

       
    }
}