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
    public class StudentController : Controller
    {
        private readonly StudentStorerService studentStorerService;

        public StudentController(StudentStorerService _studentStorerService)
        {
            studentStorerService = _studentStorerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentDto dto)
        {
            await studentStorerService.Add(dto);
            return Ok();
        }
    }
}
