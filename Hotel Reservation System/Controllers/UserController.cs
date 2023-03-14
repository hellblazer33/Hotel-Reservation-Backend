namespace BookStore.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;

    //using Microsoft.Extensions.Caching.Distributed;
    //using Microsoft.Extensions.Caching.Memory;

    /// <summary>
    /// User Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       
        private readonly IUserBL userBL;

        const string SessionFullName = "FullName";
        const string SessionEmail = "Email";

       


        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }


        [HttpPost("Register")]
        public IActionResult AddUser(UserModel userRegistration)
        {
            try
            {
                HttpContext.Session.SetString(SessionFullName, userRegistration.Fullname);
                HttpContext.Session.SetString(SessionEmail, userRegistration.Email);
                var user = this.userBL.Register(userRegistration);
                if (user != null)
                {
                    var name = HttpContext.Session.GetString(SessionFullName);
                    var email = HttpContext.Session.GetString(SessionEmail);
                    return this.Ok(new { Success = true, message = "User Added Sucessfully", Response = user });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User Added Unsuccessfully" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }


        [HttpPost("Login")]
        public IActionResult UserLogin(string email, string password)
        {
            try
            {
                var loginToken = this.userBL.UserLogin(email, password);
                if (loginToken != null)
                {
                    return this.Ok(new { Success = true, message = "Logged In Sucessfully", Response = loginToken });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Login Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

       


        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var forgotPasswordToken = this.userBL.ForgotPassword(email);
                if (forgotPasswordToken != null)
                {
                    return this.Ok(new { Success = true, message = " Token Sent on Mail To Reset The Password", Response = forgotPasswordToken });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Forgot Password Token Generation Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {
                var email = User.Claims.FirstOrDefault(e => e.Type == "Email").Value.ToString();
                if (this.userBL.ResetPassword(email, newPassword, confirmPassword))
                {
                    return this.Ok(new { Success = true, message = " Password Changed Sucessfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " Password Change Failed ! Try Again " });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }


        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            
            
            var UserList = new List<UserModel>();
            
            if (UserList != null)
            {

                UserList = this.userBL.GetAllUsers();
            }
            else
            {
                return this.BadRequest(new { Success = false, message = " No Users Found " });
            }
            return Ok(UserList);
        }


    }
}