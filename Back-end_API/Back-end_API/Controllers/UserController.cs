using Back_end_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_end_API.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        /*[HttpPost("login")]
        public IActionResult Login([FromBody] UserModel user)
        {
            var user = usersManager.Login(loginModel);
            if (user == null)
            {
                return BadRequest(new { message = "Login or password is incorrect" });
            }
            return Ok(user);
        }*/
    }
}
