using Bogus;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Test.Extensions;
using CursoOnline.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CursoOnline.Dominio.Test
{
    public class EnrollmentTest
    {
        public Faker faker;

        public EnrollmentTest()
        {
            faker = new Faker();
        }

        [Fact]
        public void ShouldNotCreateEnrollmentWithNegativePricePaid()
        {
            var course = new Course(faker.Random.Word(), faker.Random.Double(0.1), TargetAudience.Student, faker.Random.Double(0.1), faker.Random.Words());
            var student = new Student(faker.Name.FullName(), faker.Random.Words(), faker.Internet.Email(), TargetAudience.Student);

            Assert.Throws<DomainException>(() => new Enrollment(faker.Random.Double(-10, -0.1), false, student, course))
                .WithMessage("Price cannot be below zero");
        }

        [Fact]
        public void ShouldNotCreateEnrollmentWithPricePaidGreaterThanCourseValue()
        {
            var course = new Course(faker.Random.Word(), faker.Random.Double(0.1), TargetAudience.Student, 150, faker.Random.Words());
            var student = new Student(faker.Name.FullName(), faker.Random.Words(), faker.Internet.Email(), TargetAudience.Student);

            Assert.Throws<DomainException>(() => new Enrollment(200, false, student, course))
                .WithMessage("Price cannot be greater than course value");
        }

        [Fact]
        public void ShouldHaveDiscount()
        {
            var course = new Course(faker.Random.Word(), faker.Random.Double(0.1), TargetAudience.Student, 150, faker.Random.Words());
            var student = new Student(faker.Name.FullName(), faker.Random.Words(), faker.Internet.Email(), TargetAudience.Student);

            var enrollment = new Enrollment(75, false, student, course);

            Assert.True(enrollment.Discounted);
        }
    }
}
