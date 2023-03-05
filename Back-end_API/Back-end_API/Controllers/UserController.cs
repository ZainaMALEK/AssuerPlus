using Back_end_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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

        [HttpPost("api/declareAccident")]
        public async Task<IActionResult> declareAccident([FromForm] Accident accident) {
            Console.WriteLine(accident.images.ContentDisposition);
            //return Ok(accident);

            try
            {
                var file = accident.images;
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (accident.images.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
