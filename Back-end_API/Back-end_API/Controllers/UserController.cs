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
        public readonly Context _db;
        public UserController(Context db) {
            _db = db;

        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("api/login")]
        public IActionResult Login([FromBody] UserModel user)
        {
            var userfound = _db.Clients.FirstOrDefault(c=> c.AssuranceNumber == user.AssuranceNumber && c.Password ==user.Password );
            if (userfound == null)
            {
                return BadRequest(new { message = "NoUser" });
            }
            return Ok(userfound);
        }
    }
}
