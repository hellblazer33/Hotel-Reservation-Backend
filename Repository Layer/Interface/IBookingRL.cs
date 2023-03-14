﻿
namespace RepoLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;

    /// <summary>
    ///  Interface Class of Repo Layer
    /// </summary>
    public interface IBookingRL
    {
        /// <summary>
        /// Adds the order.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns> Order Added in the System </returns>
        public BookingModel AddBooking(BookingModel order, int userId);

        /// <summary>
        /// Gets all order.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns> Get All Order from Orders </returns>
        public List<BookingModel> GetAllBookings(int userId);

        public bool DeleteBooking(int bookingId);
    }
}