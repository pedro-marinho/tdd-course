using Bogus;
using CursoOnline.Dominio.Builders;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Test.Extensions;
using CursoOnline.Utils;
using ExpectedObjects;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test
{
    public class CourseTest
    {
        private string _name;
        private double _workload;
        private TargetAudience _targetAudience;
        private double _value;

        private Faker faker;

        public CourseTest()
        {
            faker = new Faker();

            _name = faker.Random.Words();
            _workload = faker.Random.Double();
            _targetAudience = TargetAudience.Student;
            _value = faker.Random.Double();
        }

        [Fact]
        public void ShouldCreateCourse()
        {
            var expectedCourse = new
            {
                Name = _name,
                Workload = _workload,
                TargetAudience = _targetAudience,
                Value = _value,
                Description = "desc"
            };

            var curso = new Course(expectedCourse.Name, expectedCourse.Workload, expectedCourse.TargetAudience, expectedCourse.Value, expectedCourse.Description);

            expectedCourse.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotHaveInvalidName(string nomeInvalido)
        {
            Assert
                .Throws<DomainException>(() =>
                    CourseBuilder.New()
                        .WithName(nomeInvalido)
                        .Build()
                 )
                .WithMessage(MessageResource.NullOrEmptyName);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void ShouldNotHaveCargaHorariaLessOrEqualThanZero(double cargaHorariaInvalida)
        {
            Assert
                .Throws<DomainException>(() =>
                    CourseBuilder.New()
                        .WithName(_name)
                        .WithWorkload(cargaHorariaInvalida)
                        .Build()
                 )
                .WithMessage(MessageResource.LessThanZeroWorkload);
        }

        [Fact]
        public void ShouldChangeCourseName()
        {
            var expectedName = "ChangeTest";
            var course = CourseBuilder.New()
                .WithName(_name)
                .WithWorkload(1)
                .Build();

            course.ChangeName(expectedName);

            Assert.Equal(expectedName, course.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotChangeToInvalidName(string invalidName)
        {
            var course = CourseBuilder.New()
                .WithName(_name)
                .WithWorkload(1)
                .Build();

            Assert.Throws<DomainException>(() => course.ChangeName(invalidName))
                .WithMessage(MessageResource.NullOrEmptyName);
        }
    }
}
