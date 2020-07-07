using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExcel.Models
{
    public class Videos
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string LastUpdate { get; set; }
        public byte[] HashRow { get; set; }


        public int Genre { get; set; }
        [ForeignKey("Genre")]
        public Genres Genres { get; set; }

    }
}
