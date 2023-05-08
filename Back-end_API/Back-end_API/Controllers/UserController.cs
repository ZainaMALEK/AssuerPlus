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
using Microsoft.EntityFrameworkCore;
using Back_end_API.Models.ressources;

namespace Back_end_API.Controllers
{
    public class UserController : Controller
    {
        public readonly Context _db;
        public UserController(Context db) {
            _db = db;

        }
        [HttpGet("api/test")]
        public string getTest()
        {
            return "api ok";
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
                string  folderName = @"C:\projects\Assuer\images";
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }
                var pathToSave =  folderName;
                if (files.Any(f => f.Length == 0))
                {
                    return BadRequest();
                }
                var images = new List<Image>();
                var imagesDto = new List<ImageDto>();
                foreach (var file in files)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(folderName, fileName);

                    Image image = new Image();
                    image.Path = fullPath;
                    images.Add(image);
                    _db.Images.Add(image);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var imageDto = new ImageDto()
                    {
                        Base64 = GetImageBase64(fullPath),
                        ContentType = Path.GetExtension(fullPath).ToLower()
                    };

                    imagesDto.Add(imageDto);

                }
                sinistre.Client = client;
                sinistre.Description = sinistreModel.description;
                sinistre.Images = images;
                _db.Sinistres.Add(sinistre);
               
                _db.SaveChanges();

                var createdSinistre = new SinistreDto();
                createdSinistre.Description = sinistre.Description;
                createdSinistre.Images = imagesDto;
                createdSinistre.SinistreID = sinistre.SinistreID;

                return Ok(createdSinistre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("api/userSinistres")]
        public List<SinistreDto> getUserSinistres(int userId)
        {
            var sinistres = _db.Sinistres
         .Include(s => s.Client)
         .Include(s => s.Images)
         .Where(s => s.Client.ClientID == userId)
         .ToList();

            var sinistresDto = sinistres.Select(s => new SinistreDto
            {
                SinistreID = s.SinistreID,
                Description = s.Description,
                Images = s.Images.Select(i => new ImageDto
                {
                    Base64 = GetImageBase64(i.Path),
                    ContentType = Path.GetExtension(i.Path).ToLower()
                }).ToList()
            }).ToList();

            return sinistresDto;
        }

        private string GetImageBase64(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
                var base64 = Convert.ToBase64String(buffer);
                return base64;
            }
        }
    }
}
