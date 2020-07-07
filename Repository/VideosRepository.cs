using ApiExcel.Models;
using ApiExcel.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiExcel.Repository
{
    public class VideosRepository : IRepository<Videos>
    {
        private IFileExtension fileExtension;
        private DbContextApi dbContextApi;
        public VideosRepository(DbContextApi _dbContextApi)
        {
            fileExtension = new FileExtension();
            dbContextApi = _dbContextApi;
        }

        public DbContextApi DbContextApi { get; }

        public async void AddOrUpdateAsync(IFormFile files, CancellationToken cancellationToken)
        {
            var listCodeDb = dbContextApi.Videos.AsNoTracking().Select(s => s.Code);
            var dataFile = (List<MapDatasetDataModel>)fileExtension.ParseExcel(files , true);
            var jsonListVideos = JsonConvert.DeserializeObject<List<Videos>>(JsonConvert.SerializeObject(dataFile,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

            var dataCount = jsonListVideos.Count;

            var listCodeFile = jsonListVideos.Select(s => s.Code).ToList();

            var listcodeDb = listCodeDb.ToList();

            // insert  // توی فایل هست ولی توی دیتابیس نیست 
            var insert = listCodeFile.Except(listcodeDb).ToList();
            if (insert.Count > 0)
            {
                var length = insert.Count;
                for (int i = 0; i < length; i++)
                {
                    var Videos = jsonListVideos.FirstOrDefault(w => w.Code == insert[i]);
                    dbContextApi.Add(Videos);
                }
            }
            else if (insert.Count == 0)
            {
                //update    توی دیتابیس و فایل هست 
                var update = listCodeFile.Intersect(listcodeDb).ToList();
                if (update.Count > 0)
                {
                    var length = update.Count;
                    for (int i = 0; i < length; i++)
                    {
                        var VideosFile = jsonListVideos.FirstOrDefault(s => s.Code == update[i]);
                        var VideosDb =await dbContextApi.Videos.AsNoTracking().FirstOrDefaultAsync(s => s.Code == update[i] , cancellationToken);

                        if (!Md5Helper.CheckMd5(VideosDb.HashRow, VideosFile.HashRow))
                        {
                            dbContextApi.Entry(VideosDb).State = EntityState.Deleted;
                            dbContextApi.Add(VideosFile);
                        }
                    }

                }

                // delete    توی دیتابیس هست ولی توی فایل نیست 
                var delete = listcodeDb.Except(listCodeFile).ToList();
                if (delete.Count > 0)
                {
                    var length = delete.Count;
                    for (int i = 0; i < length; i++)
                    {
                        var VideosDb =await dbContextApi.Videos.AsNoTracking().FirstOrDefaultAsync(s => s.Code == delete[i], cancellationToken);
                        dbContextApi.Entry(VideosDb).State = EntityState.Deleted;
                    }
                }
            }
           await dbContextApi.SaveChangesAsync(cancellationToken);
        }


        public async Task<List<Result>> GetAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
