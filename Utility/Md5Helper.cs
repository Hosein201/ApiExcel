using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace ApiExcel.Utility
{
    public static class Md5Helper
    {
        public static bool CheckMd5(byte[] md5Db, byte[] md5File)
        {
            if (md5File.SequenceEqual(md5Db))
                return true;
            return false;
        }

        public static byte[] Makebyte(object value)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, value);
            return ms.ToArray();
        }
    }
}
