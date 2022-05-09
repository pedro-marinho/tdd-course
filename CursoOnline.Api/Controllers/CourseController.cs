using CursoOnline.Dominio.Resources;
using CursoOnline.Dominio.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CursoOnline.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : Controller
    {
        private readonly ILogger<CourseController> _logger;
        private readonly CourseStorerService _courseStorerService;

        public CourseController(ILogger<CourseController> logger, CourseStorerService courseStorerService)
        {
            _logger = logger;
            _courseStorerService = courseStorerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseDto courseDto)
        {
            await _courseStorerService.Store(courseDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] CourseDto courseDto)
        {
            await _courseStorerService.Edit(courseDto);
            return Ok();
        }
    }
}
