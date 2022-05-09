using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Dominio.Resources
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string TargetAudience { get; set; }
    }
}
