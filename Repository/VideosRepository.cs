using ApiExcel.Models;
using ApiExcel.Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiExcel.Repository
{
    public class VideosRepository : IRepositoryImpert<Videos>
    {
        private readonly IFileExtension<Videos> fileExtension;
        private readonly DbContextApi dbContextApi;
        public VideosRepository(DbContextApi _dbContextApi)
        {
            fileExtension = new FileExtension<Videos>();
            dbContextApi = _dbContextApi;
        }
        public async Task AddOrUpdateAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            var listInsertToDb = new List<Videos>();
            var listUpdateToDb = new List<Videos>();
            var listDeleteToDb = new List<Videos>();

            var dataFile = (List<Videos>)fileExtension.ParseExcel(stream);
            var listcodeDb = dbContextApi.Videos.Select(s => s.Code).ToList();

            // insert  // توی فایل هست ولی توی دیتابیس نیست 
            var insert = dataFile.Select(s => s.Code).Except(listcodeDb).ToList();
            if (insert.Count > 0)
            {
                var length = insert.Count;
                for (int i = 0; i < length; i++)
                {
                    var Videos = dataFile.FirstOrDefault(w => w.Code == insert[i]);
                    listInsertToDb.Add(Videos);
                }
            }

            //update    توی دیتابیس و فایل هست 
            var update =listcodeDb.Intersect(dataFile.Select(s => s.Code)).ToList();
            if (update.Count > 0)
            {
                var length = update.Count;
                for (int i = 0; i < length; i++)
                {
                    var modelFile = dataFile.FirstOrDefault(f => f.Code == update[i]);
                    var modelDb = dbContextApi.Videos.FirstOrDefault(f => f.Code == update[i]);
                    if (!Md5Helper.CheckMd5(modelDb.HashRow, modelFile.HashRow))
                    {
                        listUpdateToDb.Add(modelFile);
                    }
                }
            }

            // delete    توی دیتابیس هست ولی توی فایل نیست 
            var delete = listcodeDb.Except(dataFile.Select(s => s.Code)).ToList();
            if (delete.Count > 0)
            {
                var length = delete.Count;
                for (int i = 0; i < length; i++)
                {
                    listDeleteToDb.Add(dataFile.FirstOrDefault(f => f.Code == delete[i]));
                }
            }
            await dbContextApi.AddRangeAsync(listInsertToDb, cancellationToken);
            dbContextApi.UpdateRange(listUpdateToDb);
            dbContextApi.RemoveRange(listDeleteToDb);
            await dbContextApi.SaveChangesAsync(cancellationToken);
        }
    }
}
