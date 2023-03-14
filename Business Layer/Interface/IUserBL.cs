namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;
    using CommonLayer.Token;

   
    public interface IUserBL
    {
        
        public UserModel Register(UserModel user);

       
        public UserAccount UserLogin(string email, string password);

      
        public string ForgotPassword(string email);

       
        public bool ResetPassword(string email, string newPassword, string confirmPassword);

        public List<UserModel> GetAllUsers();
    }
}