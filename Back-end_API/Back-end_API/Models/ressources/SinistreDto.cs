using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_end_API.Models.ressources
{
    public class SinistreDto
    {
        public int SinistreID { get; set; }
        public string Description { get; set; }
        public List<ImageDto> Images { get; set; }
    }
    public class ImageDto
    {
        public string Base64 { get; set; }
        public string ContentType { get; set; }
    }

}

