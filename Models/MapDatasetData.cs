namespace ApiExcel.Models
{
    public class MapDatasetDataModel
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string LastUpdate { get; set; }
        public int Priority { get; set; } // الویت
        public byte[] HashRow { get; set; }
    }

    public class Result
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public string NameVideos { get; set; }
    }
}
