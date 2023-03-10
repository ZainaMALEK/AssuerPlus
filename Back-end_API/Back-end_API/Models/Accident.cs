using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_end_API.Models
{
    public class Accident
    {
        public string description { get; set; }
        public List<IFormFile>  images { get; set; }
    }
}
