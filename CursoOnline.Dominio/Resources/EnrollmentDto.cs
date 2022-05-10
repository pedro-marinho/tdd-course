using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Dominio.Resources
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public double PricePaid { get; set; }
        public double? FinalGrade { get; set; }
        public bool Cancelled { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
