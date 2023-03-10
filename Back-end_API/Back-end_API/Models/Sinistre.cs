using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_end_API.Models
{
    public class Sinistre
    {
        public int SinistreID { get; set; }
        public string Description { get; set; }
        public List<Image> Images { get; set; }
        public Client Client { get; set; }
    }
}
