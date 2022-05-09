using Bogus;
using CursoOnline.Dominio.Enums;
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
    public class StudentStorerTest
    {
        public StudentDto dto;

        public StudentStorerService studentStorer;
        public Mock<IStudentRepository> studentRepoMock;

        public Faker faker;
        public StudentStorerTest()
        {
            faker = new Faker();

            studentRepoMock = new Mock<IStudentRepository>();
            studentStorer = new StudentStorerService(studentRepoMock.Object);

            dto = new StudentDto()
            {
                Id = faker.Random.Int(),
                Name = faker.Name.FullName(),
                Cpf = faker.Random.Word(),
                Email = faker.Internet.Email(),
                TargetAudience = "Student"
            };
        }

        [Fact]
        public async Task ShouldAddStudent()
        {
            await studentStorer.Add(dto);

            studentRepoMock.Verify(r => r.Add(It.Is<Student>(s => s.Name == dto.Name && s.Cpf == dto.Cpf && s.Email == dto.Email)));
        }

        [Fact]
        public async Task ShouldNotAcceptInvalidTargetAudience()
        {
            var invalidTargetAudience = "Invalid";
            dto.TargetAudience = invalidTargetAudience;

            var ex = await Assert.ThrowsAsync<DomainException>(async () => await studentStorer.Add(dto));
            ex.WithMessage(MessageResource.InvalidTargetAudience);
        }

        [Fact]
        public async Task ShouldChangeStudentName()
        {
            var differentNameToSave = "TestEditName";
            var studentAlreadySaved = new Student("OriginalName", dto.Cpf, dto.Email, TargetAudience.Student);
            dto.Name = differentNameToSave;

            studentRepoMock.Setup(r => r.GetById(dto.Id)).Returns(Task.FromResult(studentAlreadySaved));

            await studentStorer.Edit(dto);

            Assert.Equal(dto.Name, studentAlreadySaved.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task ShouldNotChangeToInvalidName(string invalidName)
        {
            var studentAlreadySaved = new Student("OriginalName", dto.Cpf, dto.Email, TargetAudience.Student);
            dto.Name = invalidName;

            studentRepoMock.Setup(s => s.GetById(dto.Id)).Returns(Task.FromResult(studentAlreadySaved));

            var ex = await Assert.ThrowsAsync<DomainException>(async () => await studentStorer.Edit(dto));
            ex.WithMessage(MessageResource.NullOrEmptyName);
        }
    }
}
