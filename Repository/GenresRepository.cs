using ApiExcel.Models;
using ApiExcel.Utility;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiExcel.Repository
{
    public class GenresRepository : IRepository<Genres>
    {
        private IFileExtension fileExtension;
        private DbContextApi dbContextApi;
        public GenresRepository(DbContextApi _dbContextApi)
        {
            fileExtension = new FileExtension();
            dbContextApi = _dbContextApi;
        }
        public async void AddOrUpdateAsync(IFormFile files , CancellationToken cancellationToken)
        {
            var listCodeDb = dbContextApi.Genres.AsNoTracking().Select(s =>s.Code);
            var dataFile = (List<MapDatasetDataModel>)fileExtension.ParseExcel(files , false);
            var jsonListGenres = JsonConvert.DeserializeObject<List<Genres>>(JsonConvert.SerializeObject(dataFile, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

            var dataCount = jsonListGenres.Count;

            var listCodeFile = jsonListGenres.Select(s => s.Code).ToList();

            var listcodeDb = listCodeDb.ToList();

            // insert  // توی فایل هست ولی توی دیتابیس نیست 
            var insert = listCodeFile.Except(listcodeDb).ToList();
            if (insert.Count>0)
            {
                var length= insert.Count;
                for (int i = 0; i < length; i++)
                {
                   var genres= jsonListGenres.FirstOrDefault(w => w.Code == insert[i]);
                    dbContextApi.Add(genres);
                }
            }
            else if(insert.Count==0)
            {
                //update    توی دیتابیس و فایل هست 
                var update = listCodeFile.Intersect(listcodeDb).ToList();
                if (update.Count>0)
                {
                    var length = update.Count;
                    for (int i = 0; i < length; i++)
                    {
                        var genresFile = jsonListGenres.FirstOrDefault(s => s.Code == update[i]);
                        var genresDb =await dbContextApi.Genres.FirstOrDefaultAsync(s => s.Code == update[i], cancellationToken);

                        if (!Md5Helper.CheckMd5(genresDb.HashRow, genresFile.HashRow))
                        {
                            dbContextApi.Entry(genresDb).State = EntityState.Deleted;
                            dbContextApi.Add(genresFile);
                        }
                    }
                    
                }

                // delete    توی دیتابیس هست ولی توی فایل نیست 
                var delete = listcodeDb.Except(listCodeFile).ToList();
                if (delete.Count>0)
                {
                    var length = delete.Count;
                    for (int i = 0; i < length; i++)
                    {
                        var genresDb =await dbContextApi.Genres.FirstOrDefaultAsync(s => s.Code == delete[i], cancellationToken);
                        dbContextApi.Entry(genresDb).State = EntityState.Deleted;
                    }
                }
            }
            await dbContextApi.SaveChangesAsync(cancellationToken);
        }

      

        public async Task<List<Result>> GetAsync(CancellationToken cancellationToken)
        {
          var result=await Task.Run(()=>  dbContextApi.Genres.OrderByDescending(o => o.LastUpdate).Take(6).Include(i => i.Videos)
                .Where(w => w.Status == "فعال" && w.Videos.Any(f => f.Code == w.Code))
                .Select(s=> new Result(){
                   Code=s.Code,
                    Description=s.Description ,
                    Name=s.Name, 
                    Videos=s.Videos.OrderByDescending(o => o.LastUpdate).Where(w => w.Status == "فعال")
                    .Take(10).Select(vs=> new ListVideos { Name=vs.Name ,Description=vs.Description}).ToList()
                }).ToList(),cancellationToken);

            return result;

        }
    }

}
