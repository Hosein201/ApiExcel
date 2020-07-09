using ApiExcel.Models;
using ApiExcel.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace ApiExcel.Controllers.Api
{
    [Route("api/ApiImport")]
    [ApiController]
    public class ApiImportController : ControllerBase
    {
        private readonly IRepositoryImpert<Genres> _repositoryGenres;
        private readonly IRepositoryImpert<Videos> _repositoryVideos;
        public ApiImportController(IRepositoryImpert<Genres> repositoryGenres,
            IRepositoryImpert<Videos> repositoryVideos)
        {
            _repositoryGenres = repositoryGenres;
            _repositoryVideos = repositoryVideos;
        }

        [HttpPost("UploadVideos")]
        public async Task<IActionResult> UploadVideos(IFormFile file, CancellationToken cancellationToken)
        {
            await _repositoryVideos.AddOrUpdateAsync(file.OpenReadStream(), cancellationToken);
            return Ok();
        }

        [HttpPost("UploadGenres")]
        public async Task<IActionResult> UploadGenres(IFormFile file, CancellationToken cancellationToken)
        {
            await _repositoryGenres.AddOrUpdateAsync(file.OpenReadStream(), cancellationToken);
            return Ok();
        }
    }
}
