namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using RepoLayer.Interface;

    /// <summary>
    /// Service Class For Business Layer  Interface
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.IBookingBL" />
    public class BookingBL : IBookingBL
    {
        /// <summary>
        /// The order RL
        /// </summary>
        private readonly IBookingRL bookingRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingBL"/> class.
        /// </summary>
        /// <param name="orderRL">The order RL.</param>
        public BookingBL(IBookingRL bookingRL)
        {
            this.bookingRL = bookingRL;
        }

        /// <summary>
        /// Adds the order.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Order Added in the System
        /// </returns>
        public BookingModel AddBooking(BookingModel order, int userId)
        {
            try
            {
                return this.bookingRL.AddBooking(order, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets all order.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Get All Order from Orders
        /// </returns>
        public List<BookingModel> GetAllBookings(int userId)
        {
            try
            {
                return this.bookingRL.GetAllBookings(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteBooking(int bookingId)
        {
            try
            {
                return this.bookingRL.DeleteBooking(bookingId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}