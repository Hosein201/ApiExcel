using ApiExcel.Models;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace ApiExcel.Utility
{
    public interface IFileExtension<T> where T : class
    {
        IEnumerable<object> ParseExcel(Stream files);
    }
    public class FileExtension<T> : IFileExtension<T> where T : class
    {
        public IEnumerable<object> ParseExcel(Stream files)
        {
            var data = typeof(T);

            using (MemoryStream ms = new MemoryStream())
            {
                files.CopyTo(ms);
                var fileBytes = ms.ToArray(); Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (var reader = ExcelReaderFactory.CreateReader(files))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        UseColumnDataType = true,
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });
                    if (data == typeof(Genres))
                    {
                        return MapDatasetDataGenres(result.Tables.Cast<DataTable>().ToList()[0]);
                    }
                    else
                    {
                        return MapDatasetDataVideos(result.Tables.Cast<DataTable>().ToList()[0]);
                    }
                }
            }
        }

        private List<Genres> MapDatasetDataGenres(DataTable dt)
        {
            var row = new List<Genres>();
            foreach (DataRow dr in dt.Rows)
            {
                var model = new Genres
                {
                    Code = dr["کد"].ToString().Fa2En(),
                    Name = dr["نام"].ToString(),
                    Status = dr["وضعیت"].ToString() == "فعال" ? true : false,
                    Description = dr["توضیحات"].ToString(),
                    LastUpdate = Convert.ToDateTime(dr["اخرین تاریخ بروزرسانی"]),
                    Priority = dr["الویت"].ToString().Fa2En(),
                    HashRow = null
                };
                model.HashRow = Md5Helper.Makebyte(model);
                row.Add(model);
            }
            return row;
        }

        private List<Videos> MapDatasetDataVideos(DataTable dt)
        {
            var row = new List<Videos>();
            foreach (DataRow dr in dt.Rows)
            {
                var model = new Videos
                {
                    Code = dr["کد"].ToString().Fa2En(),
                    Name = dr["نام"].ToString(),
                    Status = dr["وضعیت"].ToString() == "فعال" ? true : false,
                    Description = dr["توضیحات"].ToString(),
                    LastUpdate = Convert.ToDateTime(dr["اخرین تاریخ بروزرسانی"]),
                    Genre = dr["ژانر"].ToString().Fa2En(),
                    HashRow = null
                };
                model.HashRow = Md5Helper.Makebyte(model);
                row.Add(model);
            }
            return row;
        }
    }

    public static class Fa2EnClass

    {
        public static int Fa2En(this string str)
        {
            str.Replace("۰", "0")
               .Replace("۱", "1")
               .Replace("۲", "2")
               .Replace("۳", "3")
               .Replace("۴", "4")
               .Replace("۵", "5")
               .Replace("۶", "6")
               .Replace("۷", "7")
               .Replace("۸", "8")
               .Replace("۹", "9")
               //iphone numeric
               .Replace("٠", "0")
               .Replace("١", "1")
               .Replace("٢", "2")
               .Replace("٣", "3")
               .Replace("٤", "4")
               .Replace("٥", "5")
               .Replace("٦", "6")
               .Replace("٧", "7")
               .Replace("٨", "8")
               .Replace("٩", "9");
            var v = str.Replace(",", "");
            return Convert.ToInt32(v);
        }


    }

}
