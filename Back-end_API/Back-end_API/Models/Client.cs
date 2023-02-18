using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_end_API.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Adress { get; set; }
        public int Phone { get; set; }
        public int AssuranceNumber { get; set; }
        public string Password { get; set; }
        


    }
}
