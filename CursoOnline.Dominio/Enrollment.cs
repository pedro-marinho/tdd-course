using CursoOnline.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Dominio
{
    public class Enrollment : Entity
    {
        private double _pricePaid;
        public double PricePaid {
            get => _pricePaid;
            private set
            {
                Validator.When(value < 0, "Price cannot be below zero");
                Validator.When(value > Course.Value, "Price cannot be greater than course value");

                _pricePaid = value;
            }
        }

        public bool Discounted { get; set; }

        private double? _finalGrade;
        public double? FinalGrade {
            get => _finalGrade;
            private set
            {
                Validator.When(value < 0 || value > 10, "Grade must be between 0 and 10");
                _finalGrade = value;
            }
        }
        public bool Cancelled { get; private set; }

        public Student Student { get; private set; }
        public Course Course { get; private set; }

        public int StudentId { get; private set; }
        public int CourseId { get; private set; }

        private Enrollment() { }

        public Enrollment(double pricePaid, bool cancelled, Student student, Course course)
        {
            Student = student;
            Course = course;
            Validator.When(Student.TargetAudience != Course.TargetAudience, "Course and Student target audiences must be equal");
            PricePaid = pricePaid;
            Cancelled = cancelled;
            Discounted = course.Value != pricePaid;

            Check();
        }

        public void ChangeCancelled(bool status)
        {
            Cancelled = status;
            Check();
        }

        public void ChangeFinalGrade(double finalGrade)
        {
            FinalGrade = finalGrade;
            Check();
        }
    }
}
