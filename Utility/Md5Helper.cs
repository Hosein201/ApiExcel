using ApiExcel.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ApiExcel.Utility
{
    public static class Md5Helper
    {
        public static bool CheckMd5(byte[] md5Db, byte[] md5File )
        {
            if (md5File.SequenceEqual(md5Db))
                return true;
            return false;
        }

        public static byte[] Makebyte(object  value)
        {
            //TypeConverter obj = TypeDescriptor.GetConverter(value.GetType());
            //byte[] bt = (byte[])obj.ConvertTo(value, typeof(byte[]));

            var x = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(value));
            MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider();
            byte[] md5File = mD5.ComputeHash(x);
            return md5File;
        }
    }
}
