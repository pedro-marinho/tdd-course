using CursoOnline.Dominio.Builders;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Repositories;
using CursoOnline.Dominio.Resources;
using CursoOnline.Utils;
using System;
using System.Threading.Tasks;

namespace CursoOnline.Dominio.Services
{
    public class CourseStorerService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseStorerService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task Store(CourseDto dto)
        {
            var validator = DomainValidator.New();
            DtoValidator.ValidateEnum(dto.TargetAudience, validator, MessageResource.InvalidTargetAudience, out TargetAudience targetAudienceEnum);
            await ValidateRepeatedName(dto.Name, validator);
            validator.ThrowExceptionIfHasErrors();

            var course = CourseBuilder.New()
                .WithName(dto.Name)
                .WithWorkload(dto.Workload)
                .WithTargetAudience(targetAudienceEnum)
                .WithValue(dto.Value)
                .WithDescription(dto.Description)
                .Build();

            await _courseRepository.Add(course);
        }
        
        public async Task Edit(CourseDto dto)
        {
            var validator = DomainValidator.New();
            DtoValidator.ValidateEnum(dto.TargetAudience, validator,  MessageResource.InvalidTargetAudience, out TargetAudience targetAudienceEnum);
            await ValidateRepeatedName(dto.Name, validator);
            validator.ThrowExceptionIfHasErrors();

            var course = await _courseRepository.GetById(dto.Id);

            if (course != null)
            {
                course.ChangeName(dto.Name);
                course.ChangeWorkload(dto.Workload);
                course.ChangeValue(dto.Value);
                course.ChangeTargetAudience(targetAudienceEnum);
                course.ChangeDescription(dto.Description);
            }
        }

        public async Task ValidateRepeatedName(string newName, DomainValidator validator)
        {
            var courseAlreadySaved = await _courseRepository.GetByName(newName);
            validator.When(courseAlreadySaved?.Name == newName, MessageResource.CourseAlreadyExists);
        }
    }
}
