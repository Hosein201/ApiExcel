using ApiExcel.Models;
using ApiExcel.Utility;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiExcel.Repository
{
    public class GenresRepository : IRepositoryImpert<Genres>, IRepositoryRead
    {
        private readonly IFileExtension<Genres> fileExtension;
        private readonly DbContextApi dbContextApi;
        public GenresRepository(DbContextApi _dbContextApi)
        {
            fileExtension = new FileExtension<Genres>();
            dbContextApi = _dbContextApi;
        }
        public async Task AddOrUpdateAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            var listInsertToDb = new List<Genres>();
            var listUpdateToDb = new List<Genres>();
            var listDeleteToDb = new List<Genres>();

            var dataFile = (List<Genres>)fileExtension.ParseExcel(stream);
            var listcodeDb = dbContextApi.Genres.Select(s => s.Code).ToList();

            // insert  // توی فایل هست ولی توی دیتابیس نیست 
            var insert = dataFile.Select(s => s.Code).Except(listcodeDb).ToList();
            if (insert.Count > 0)
            {
                var length = insert.Count;
                for (int i = 0; i < length; i++)
                {
                    var Genres = dataFile.FirstOrDefault(w => w.Code == insert[i]);
                    listInsertToDb.Add(Genres);
                }
            }

            //update    توی دیتابیس و فایل هست 
            var update = listcodeDb.Intersect(dataFile.Select(s => s.Code)).ToList();
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

        public object Get()
        {
            var result = from Genres in dbContextApi.Genres
                         join Videos in dbContextApi.Videos on Genres.Code equals Videos.Genre into Video
                         where Genres.Status
                         from m in Video
                         select new Result
                         {
                             Name = Genres.Name,
                             Description = Genres.Description,
                             NameVideos = m.Name
                         };

            return result.ToList(); 
        }
    }
}
