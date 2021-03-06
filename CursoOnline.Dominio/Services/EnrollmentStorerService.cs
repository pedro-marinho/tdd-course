using CursoOnline.Dominio.Repositories;
using CursoOnline.Dominio.Resources;
using CursoOnline.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace CursoOnline.Dominio.Services
{
    public class EnrollmentStorerService
    {
        private readonly IEnrollmentRepository _enrollmentRepo;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public EnrollmentStorerService(IEnrollmentRepository enrollmentRepo,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository)
        {
            _enrollmentRepo = enrollmentRepo;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public async Task Add(EnrollmentDto dto)
        {
            var validator = DomainValidator.New();

            var student = await _studentRepository.GetById(dto.StudentId);
            validator.When(student == null, "Student not found");
            validator.ThrowExceptionIfHasErrors();

            var course = await _courseRepository.GetByIdWithEnrollments(dto.CourseId);
            validator.When(course == null, "Course not found");
            validator.ThrowExceptionIfHasErrors();

            validator.When(course != null && course.Enrollments != null && course.Enrollments.Any(e => e.StudentId == dto.StudentId && !e.Cancelled), "Student already enrolled in this course");
            validator.ThrowExceptionIfHasErrors();

            var enrollment = new Enrollment(dto.PricePaid, dto.Cancelled, student, course);

            await _enrollmentRepo.Add(enrollment);
        }
    }
}
