using System.Collections.Generic;

namespace ApiExcel.Models
{
    public class MapDatasetDataModel
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string LastUpdate { get; set; }
        public int Priority { get; set; } // الویت
        public byte[] HashRow { get; set; }
    }

    public class SelectDb
    {
        public int Code { get; set; }
        public byte[] HashRow { get; set; }
    }
    public class Result
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public List<ListVideos> Videos { get; set; } = new List<ListVideos>();
    }
    public class ListVideos
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
