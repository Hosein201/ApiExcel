using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExcel.Models
{
    public class Genres
    {
        public int Id { get; set; }

        public int Code { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string LastUpdate { get; set; }
        public int Priority { get; set; }
        public byte[] HashRow { get; set; }
        public ICollection<Videos> Videos { get; set; }
    }
}
