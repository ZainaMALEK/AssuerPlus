using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Back_end_API.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Adress { get; set; }
        public  string Phone { get; set; }
        public string AssuranceNumber { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        


    }
}
