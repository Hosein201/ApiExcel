using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiExcel.Models
{
    [Serializable]
    public class Videos
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "نام فیلم وارد شده ببش حد مجاز می باشد")]
        public string Name { get; set; }
        public int Code { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
        public byte[] HashRow { get; set; }
        public int Genre { get; set; }
    }
}
