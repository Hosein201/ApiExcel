using ApiExcel.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace ApiExcel.Utility
{
    public interface IFileExtension
    {
        IEnumerable<MapDatasetDataModel> ParseExcel(IFormFile files, bool Videos);
    }
    public class FileExtension : IFileExtension
    {

        public IEnumerable<MapDatasetDataModel> ParseExcel(IFormFile files, bool Videos)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                files.CopyTo(ms);
                var fileBytes = ms.ToArray();
                using (var reader = ExcelReaderFactory.CreateReader(files.OpenReadStream()))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        UseColumnDataType = true,
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });

                    return MapDatasetData(result.Tables.Cast<DataTable>().ToList()[0], Videos);

                }
            }
        }

        private IEnumerable<MapDatasetDataModel> MapDatasetData(DataTable dt, bool Videos)
        {
            //var x= dt.Rows..Cast<List<Videos>>().ToList();
            var row = new List<MapDatasetDataModel>();
            foreach (DataRow dr in dt.Rows)
            {
                var model = new MapDatasetDataModel();
                model.Code = dr["کد"].ToString().Fa2En();
                model.Name = dr["نام"].ToString();
                model.Status = dr["وضعیت"].ToString();
                model.Description = dr["توضیحات"].ToString();
                model.LastUpdate = dr["اخرین تاریخ بروزرسانی"].ToString();
                if (!Videos) //  اولیت داخل ژانر 
                    model.Priority = dr["الویت"].ToString().Fa2En();
                if (Videos) // زانر داخل فیلم
                    model.Genre = dr["ژانر"].ToString().Fa2En().ToString();
                model.HashRow = Md5Helper.Makebyte(dr);
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
