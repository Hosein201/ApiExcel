using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiExcel.Models
{
    [Serializable]
    public class Genres
    {
        public int Id { get; set; }
        public int Code { get; set; }
        [MaxLength(50, ErrorMessage = "نام ژانر وارد شده ببش حد مجاز می باشد")]
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
        public int Priority { get; set; }
        public byte[] HashRow { get; set; }
    }
}
