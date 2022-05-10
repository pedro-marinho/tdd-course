using Bogus;
using CursoOnline.Dominio.Repositories;
using CursoOnline.Dominio.Resources;
using CursoOnline.Dominio.Services;
using CursoOnline.Dominio.Test.Extensions;
using CursoOnline.Utils;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CursoOnline.Dominio.Test
{
    public class EnrollmentStorerServiceTest
    {
        public Faker faker;
        public Mock<ICourseRepository> courseRepoMock;
        public Mock<IStudentRepository> studentRepoMock;
        public Mock<IEnrollmentRepository> enrollmentRepoMock;

        public EnrollmentDto enrollmentDto;
        public EnrollmentStorerService enrollmentStorer;

        public EnrollmentStorerServiceTest()
        {
            faker = new Faker();
            courseRepoMock = new Mock<ICourseRepository>();
            studentRepoMock = new Mock<IStudentRepository>();
            enrollmentRepoMock = new Mock<IEnrollmentRepository>();

            enrollmentStorer = new EnrollmentStorerService(enrollmentRepoMock.Object, studentRepoMock.Object, courseRepoMock.Object);

            enrollmentDto = new EnrollmentDto()
            {
                PricePaid = 80,
                Cancelled = false,
                StudentId = 1,
                CourseId = 1
            };
        }

        [Fact]
        public async Task ShouldNotCreateEnrollmentForNonExistingStudent()
        {
            courseRepoMock.Setup(c => c.GetById(enrollmentDto.CourseId)).Returns(Task.FromResult<Course>(new TestableCourse { TestableId = 1 }));
            studentRepoMock.Setup(s => s.GetById(enrollmentDto.StudentId)).Returns(Task.FromResult<Student>(null));
            
            var ex = await Assert.ThrowsAsync<DomainException>(async () => await enrollmentStorer.Add(enrollmentDto));
            ex.WithMessage("Student not found");
        }

        [Fact]
        public async Task ShouldNotCreateEnrollmentForNonExistingCourse()
        {
            courseRepoMock.Setup(c => c.GetById(enrollmentDto.CourseId)).Returns(Task.FromResult<Course>(null));
            studentRepoMock.Setup(s => s.GetById(enrollmentDto.StudentId)).Returns(Task.FromResult<Student>(new TestableStudent { TestableId = 1 }));

            var ex = await Assert.ThrowsAsync<DomainException>(async () => await enrollmentStorer.Add(enrollmentDto));
            ex.WithMessage("Course not found");
        }
    }

    public class TestableCourse : Course
    {
        public int TestableId
        {
            get => Id;
            set
            {
                Id = value;
            }
        }
    }

    public class TestableStudent : Student
    {
        public int TestableId
        {
            get => Id;
            set
            {
                Id = value;
            }
        }
    }
}
