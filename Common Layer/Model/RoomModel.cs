namespace CommonLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Book Model Class for Update And Fetching Records
    /// </summary>
    public class RoomModel
    {

        public int RoomId { get; set; }


        public int RoomNumber { get; set; }


        public int Rating { get; set; }


        public int TotalRating { get; set; }


        public decimal DiscountPrice { get; set; }


        public decimal OriginalPrice { get; set; }


        public string Description { get; set; }

    }
}