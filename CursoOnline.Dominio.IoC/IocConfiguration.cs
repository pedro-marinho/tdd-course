using CursoOnline.Dominio.Data;
using CursoOnline.Dominio.Data.Context;
using CursoOnline.Dominio.Data.UoW;
using CursoOnline.Dominio.Repositories;
using CursoOnline.Dominio.Services;
using CursoOnline.Dominio.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CursoOnline.Dominio.IoC
{
    public static class IocConfiguration
    {
        public static void ConfigureIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CourseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<CourseStorerService>();
            services.AddScoped<StudentStorerService>();
            services.AddScoped<EnrollmentStorerService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        }
    }
}
