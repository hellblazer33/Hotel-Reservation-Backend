namespace CommonLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// User Register Model Class
    /// </summary>
    public class UserModel
    {
        public int UserId { get; set; }
        public string Fullname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        
        public string MobileNumber { get; set; }
    }
}