namespace RepoLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using CommonLayer.Models;
    using Microsoft.Extensions.Configuration;
    using RepoLayer.Interface;

    /// <summary>
    ///  Service Class For Repo Layer  Interface
    /// </summary>
    /// <seealso cref="RepoLayer.Interface.IBookingRL" />
    public class BookingRL : IBookingRL
    {
        
        private SqlConnection sqlConnection;

      
        public BookingRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

       
        private IConfiguration Configuration { get; }

       
        public BookingModel AddBooking(BookingModel order, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:HotelManagement"]);
                SqlCommand cmd = new SqlCommand("AddBooking", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@RoomId", order.RoomId);
                cmd.Parameters.AddWithValue("@StartDate", order.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", order.EndDate);

                this.sqlConnection.Open();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                this.sqlConnection.Close();
               
                return order;
                
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

    
        public List<BookingModel> GetAllBookings(int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:HotelManagement"]);
                SqlCommand cmd = new SqlCommand("GetBookings", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", userId);
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<BookingModel> orderModels = new List<BookingModel>();
                    while (reader.Read())
                    {
                        BookingModel orderModel = new BookingModel();
                        RoomModel roomModel = new RoomModel();
                        orderModel.BookingId = Convert.ToInt32(reader["BookingId"]);
                        orderModel.UserId = Convert.ToInt32(reader["UserId"]);
                        orderModel.RoomId = Convert.ToInt32(reader["roomId"]);
                        orderModel.Price = Convert.ToInt32(reader["Price"]);
                        orderModel.BookingDate = Convert.ToDateTime(reader["BookingDate"]);
                        orderModel.StartDate = Convert.ToDateTime(reader["StartDate"]);
                        orderModel.EndDate = Convert.ToDateTime(reader["EndDate"]);
                        roomModel.RoomNumber = Convert.ToInt32(reader["roomNumber"]);
                        
                        //orderModel.BookModel = bookModel;
                        orderModels.Add(orderModel);
                    }

                    this.sqlConnection.Close();
                    return orderModels;
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

        public bool DeleteBooking(int bookingId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:HotelManagement"]);
                SqlCommand cmd = new SqlCommand("DeleteBooking", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@bookingId", bookingId);
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
    }
}