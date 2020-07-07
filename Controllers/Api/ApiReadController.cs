using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiExcel.Models;
using ApiExcel.Repository;
using ApiExcel.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiExcel.Controllers.Api
{
    [Route("api/ApiRead")]
    [ApiController]
    public class ApiReadController : ControllerBase
    {
        private readonly IRepository<Genres> _repositoryGenres;
        public ApiReadController(IRepository<Genres> repositoryGenres)
        {
            this._repositoryGenres = repositoryGenres;
        }
        [HttpGet("Read")]
        public async Task<IActionResult> Read(CancellationToken cancellationToken)
        {
            
            return Ok(_repositoryGenres.GetAsync(cancellationToken));
        }
    }
}
