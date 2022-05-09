using Bogus;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Test.Extensions;
using CursoOnline.Utils;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.Dominio.Test
{
    public class StudentTest
    {
        private string name;
        private string cpf;
        private string email;
        private TargetAudience targetAudience;

        private Faker faker;
        public StudentTest()
        {
            faker = new Faker();

            name = faker.Name.FindName();
            cpf = faker.Random.Words();
            email = faker.Internet.Email();
            targetAudience = TargetAudience.Student;
        }

        [Fact]
        public void ShouldCreateStudent()
        {
            var expectedStudent = new
            {
                Name = name,
                Cpf = cpf,
                Email = email,
                TargetAudience = targetAudience
            };

            var student = new Student(expectedStudent.Name, expectedStudent.Cpf, expectedStudent.Email, expectedStudent.TargetAudience);

            expectedStudent.ToExpectedObject().ShouldMatch(student);
        }

        [Fact]
        public void ShouldNotCreateStudentWithEmptyEmail()
        {
            Assert.Throws<DomainException>(() => new Student(name, cpf, "", targetAudience))
                .WithMessage("Email must not be null or empty");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateStudentWithEmptyOrNullCpf(string invalidCpf)
        {
            Assert.Throws<DomainException>(() => new Student(name, invalidCpf, email, targetAudience))
                .WithMessage("Cpf must not be null or empty");
        }

        [Fact]
        public void ShouldChangeStudentName()
        {
            var differentName = "OtherName";
            var student = new Student(name, cpf, email, targetAudience);

            student.ChangeName(differentName);

            Assert.Equal(differentName, student.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotChangeStudentNameToInvalidName(string invalidName)
        {
            var student = new Student(name, cpf, email, targetAudience);

            Assert.Throws<DomainException>(() => student.ChangeName(invalidName))
                .WithMessage(MessageResource.NullOrEmptyName);
        }
    }
}
