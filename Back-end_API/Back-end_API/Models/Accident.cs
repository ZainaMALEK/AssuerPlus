using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_end_API.Models
{
    public class Accident
    {
        public string description { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile images { get; set; }
    }
}
