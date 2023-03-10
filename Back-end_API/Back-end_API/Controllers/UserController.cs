using Back_end_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

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
            try
            {
                var userfounds = _db.Clients.FirstOrDefault(c => c.AssuranceNumber == user.AssuranceNumber && c.Password == user.Password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            var userfound = _db.Clients.FirstOrDefault(c=> c.AssuranceNumber == user.AssuranceNumber && c.Password ==user.Password );
            if (userfound != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials

                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                var userjson = JsonSerializer.Serialize(userfound);
                return Ok(new { Token = tokenString, User = userjson });
            }
      
            else if (user is null)
            {
                return BadRequest("Invalid client request");
            }
            
            return Unauthorized();
        }

        [HttpPost("api/declareAccident")]
        public async Task<IActionResult> declareAccident([FromForm] SinistreModel sinistreModel) {
            
            try
            {
                int id = sinistreModel.clientID;
                Sinistre sinistre = new Sinistre();
                Client client = _db.Clients.FirstOrDefault(c => c.ClientID == id);
                
                var files = sinistreModel.images;
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (files.Any(f => f.Length == 0))
                {
                    return BadRequest();
                }
                var images = new List<Image>();
                foreach (var file in files)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    Image image = new Image();
                    image.Path = fullPath;
                    images.Add(image);
                    _db.Images.Add(image);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                   
                }
                sinistre.Client = client;
                sinistre.Description = sinistreModel.description;
                sinistre.Images = images;
                _db.Sinistres.Add(sinistre);
               
                _db.SaveChanges();
                return Ok("All the files are successfully uploaded.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
