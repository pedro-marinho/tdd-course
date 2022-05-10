using CursoOnline.Dominio.Enums;
using CursoOnline.Utils;
using System.Collections.Generic;

namespace CursoOnline.Dominio
{
    public class Student : Entity
    {
        private string _name;
        public string Name {
            get => _name;
            private set
            {
                Validator.When(string.IsNullOrEmpty(value), MessageResource.NullOrEmptyName);
                _name = value;
            }
        }

        private string _cpf;
        public string Cpf {
            get => _cpf;
            private set
            {
                Validator.When(string.IsNullOrEmpty(value), "Cpf must not be null or empty");
                _cpf = value;
            }
        }

        private string _email;
        public string Email {
            get => _email;
            private set
            {
                Validator.When(string.IsNullOrEmpty(value), "Email must not be null or empty");
                _email = value;
            }
        }
        public TargetAudience TargetAudience { get; protected set; }

        protected Student() { }

        public Student(string name, string cpf, string email, TargetAudience targetAudience)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            TargetAudience = targetAudience;

            Check();
        }

        public void ChangeName(string newName)
        {
            Name = newName;
            Check();
        }
    }
}
