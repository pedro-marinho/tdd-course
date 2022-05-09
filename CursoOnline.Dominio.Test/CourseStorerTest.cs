using Bogus;
using CursoOnline.Dominio.Builders;
using CursoOnline.Dominio.Repositories;
using CursoOnline.Dominio.Resources;
using CursoOnline.Dominio.Services;
using CursoOnline.Dominio.Test.Extensions;
using CursoOnline.Utils;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CursoOnline.Dominio.Test
{
    public class CourseStorerTest
    {
        private readonly CourseDto dto;

        private readonly Faker faker;
        private readonly Mock<ICourseRepository> courseRepoMock;
        private readonly CourseStorerService courseStorer;

        public CourseStorerTest()
        {
            faker = new Faker();
            courseRepoMock = new Mock<ICourseRepository>();
            courseStorer = new CourseStorerService(courseRepoMock.Object);

            dto = new CourseDto
            {
                Id = 1,
                Name = faker.Random.Words(),
                Workload = faker.Random.Double(0.1),
                TargetAudience = "Student",
                Value = faker.Random.Double(),
                Description = faker.Random.Words()
            };
        }

        [Fact]
        public async Task ShouldAddCourse()
        {
            await courseStorer.Store(dto);

            courseRepoMock.Verify(r => r.Add(It.Is<Course>(c => c.Name == dto.Name && c.Workload == dto.Workload)));
        }

        [Fact]
        public async Task ShouldNotAcceptInvalidTargetAudience()
        {
            var invalidTargetAudience = "InvalidTargetAudience";
            dto.TargetAudience = invalidTargetAudience;

            var ex = await Assert.ThrowsAsync<DomainException>(() => courseStorer.Store(dto));
            ex.WithMessage(MessageResource.InvalidTargetAudience);
        }

        [Fact]
        public async Task ShouldNotCreateCourseWithRepeatedName()
        {
            var courseAlreadySaved = CourseBuilder.New()
                .WithName(dto.Name)
                .WithWorkload(dto.Workload)
                .Build();

            courseRepoMock.Setup(r => r.GetByName(dto.Name)).Returns(Task.FromResult(courseAlreadySaved));

            var ex = await Assert.ThrowsAsync<DomainException>(() => courseStorer.Store(dto));
            ex.WithMessage(MessageResource.CourseAlreadyExists);
        }

        [Fact]
        public async Task ShouldNotChangeToExistingName()
        {
            var courseAlreadySaved = CourseBuilder.New()
                .WithName(dto.Name)
                .WithWorkload(dto.Workload)
                .Build();

            courseRepoMock.Setup(r => r.GetByName(dto.Name)).Returns(Task.FromResult(courseAlreadySaved));

            var ex = await Assert.ThrowsAsync<DomainException>(() => courseStorer.Edit(dto));
            ex.WithMessage(MessageResource.CourseAlreadyExists);
        }

        [Fact]
        public async Task ShouldChangeCourse()
        {
            dto.Name = "Name Edited";
            dto.Workload = 6.5;

            var courseAlreadySaved = CourseBuilder.New()
                .WithName("Original Name")
                .WithWorkload(0.1)
                .Build();

            courseRepoMock.Setup(r => r.GetById(dto.Id)).Returns(Task.FromResult(courseAlreadySaved));

            await courseStorer.Edit(dto);

            Assert.Equal(dto.Name, courseAlreadySaved.Name);
            Assert.Equal(dto.Workload, courseAlreadySaved.Workload);
            Assert.Equal(dto.Value, courseAlreadySaved.Value);
            Assert.Equal(dto.Description, courseAlreadySaved.Description);
        }
    }    
}
