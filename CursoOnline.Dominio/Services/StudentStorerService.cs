using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Repositories;
using CursoOnline.Dominio.Resources;
using CursoOnline.Utils;
using System.Threading.Tasks;

namespace CursoOnline.Dominio.Services
{
    public class StudentStorerService
    {
        private readonly IStudentRepository studentRepository;

        public StudentStorerService(IStudentRepository _studentRepository)
        {
            studentRepository = _studentRepository;
        }

        public async Task Add(StudentDto dto)
        {
            var validator = DomainValidator.New();
            DtoValidator.ValidateEnum(dto.TargetAudience, validator, MessageResource.InvalidTargetAudience, out TargetAudience targetAudienceEnum);
            validator.ThrowExceptionIfHasErrors();

            var student = new Student(dto.Name, dto.Cpf, dto.Email, targetAudienceEnum);

            await studentRepository.Add(student);
        }

        public async Task Edit(StudentDto dto)
        {
            var student = await studentRepository.GetById(dto.Id);

            if(student != null)
            {
                student.ChangeName(dto.Name);
            }
        }
    }
}
