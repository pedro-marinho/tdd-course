using CursoOnline.Dominio.Enums;

namespace CursoOnline.Dominio.Builders
{
    public class CourseBuilder
    {
        private string _name;
        private double _workload;
        private TargetAudience _targetAudience;
        private double _value;
        private string _description;

        public static CourseBuilder New()
        {
            return new CourseBuilder();
        }

        public CourseBuilder WithName(string nome)
        {
            _name = nome;
            return this;
        }

        public CourseBuilder WithWorkload(double cargaHoraria)
        {
            _workload = cargaHoraria;
            return this;
        }

        public CourseBuilder WithTargetAudience(TargetAudience publicoAlvo)
        {
            _targetAudience = publicoAlvo;
            return this;
        }

        public CourseBuilder WithValue(double valor)
        {
            _value = valor;
            return this;
        }

        public CourseBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public Course Build()
        {
            return new Course(_name, _workload, _targetAudience, _value, _description);
        }
    }
}
