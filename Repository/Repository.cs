using ApiExcel.Models;
using ApiExcel.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiExcel.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void AddOrUpdateAsync(IFormFile files, CancellationToken cancellationToken);
        Task<List<Result>> GetAsync(CancellationToken cancellationToken);

    }
}
