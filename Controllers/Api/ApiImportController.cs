using ApiExcel.Models;
using ApiExcel.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiExcel.Controllers.Api
{
    [Route("api/ApiImport")]
    [ApiController]
    public class ApiImportController : ControllerBase
    {
        private readonly IRepository<Genres> _repositoryGenres;
        private readonly IRepository<Videos> _repositoryVideos;
        public ApiImportController(IRepository<Genres> repositoryGenres,
            IRepository<Videos> repositoryVideos)
        {
            this._repositoryGenres = repositoryGenres;
            this._repositoryVideos = repositoryVideos;
        }
        [HttpPost("UploadVideos")]
        public async Task<IActionResult> UploadVideos(List<IFormFile> files, CancellationToken cancellationToken)
        {
            foreach (var file in files)
                await Task.Run(() => _repositoryVideos.AddOrUpdateAsync(file, cancellationToken));
            return Ok();

        }

        [HttpPost("UploadGenres")]
        public async Task<IActionResult> UploadGenres(List<IFormFile> files, CancellationToken cancellationToken)
        {
            foreach (var file in files)
                await Task.Run(() => _repositoryGenres.AddOrUpdateAsync(file, cancellationToken));
            return Ok();

        }
    }
}
