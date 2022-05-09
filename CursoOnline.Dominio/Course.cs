using CursoOnline.Dominio.Enums;
using CursoOnline.Utils;

namespace CursoOnline.Dominio
{
    public class Course : Entity
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

        private double _workload;
        public double Workload {
            get => _workload;
            private set
            {
                Validator.When(value <= 0, MessageResource.LessThanZeroWorkload);

                _workload = value;
            }
        }

        public TargetAudience TargetAudience { get; private set; }
        public double Value { get; private set; }
        public string Description { get; private set; }

        private Course() { }

        public Course(string name, double workload, TargetAudience targetAudience, double value, string description) : base()
        {
            Name = name;
            Workload = workload;
            TargetAudience = targetAudience;
            Value = value;
            Description = description;

            Check();
        }

        public void ChangeName(string newName)
        {
            Name = newName;
            Check();
        }

        public void ChangeWorkload(double newWorkload)
        {
            Workload = newWorkload;
            Check();
        }

        public void ChangeTargetAudience(TargetAudience newTarget)
        {
            TargetAudience = newTarget;
            Check();
        }

        public void ChangeValue(double newValue)
        {
            Value = newValue;
            Check();
        }

        public void ChangeDescription(string newDescription)
        {
            Description = newDescription;
            Check();
        }
    }
}
