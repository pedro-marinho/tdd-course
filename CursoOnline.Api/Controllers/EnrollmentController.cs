using CursoOnline.Dominio.Resources;
using CursoOnline.Dominio.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoOnline.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnrollmentController : Controller
    {
        private readonly EnrollmentStorerService _enrollmentStorerService;

        public EnrollmentController(EnrollmentStorerService enrollmentStorerService)
        {
            _enrollmentStorerService = enrollmentStorerService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(EnrollmentDto dto)
        {
            await _enrollmentStorerService.Add(dto);
            return Ok();
        }
    }
}
