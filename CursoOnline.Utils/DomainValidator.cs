using System.Collections.Generic;
using System.Linq;

namespace CursoOnline.Utils
{
    public class DomainValidator
    {
        private readonly List<string> _errors;

        private DomainValidator() {
            _errors = new List<string>();
        }

        public static DomainValidator New()
        {
            return new DomainValidator();
        }

        public DomainValidator When(bool hasError, string errorMessage)
        {
            if (hasError)
                _errors.Add(errorMessage);

            return this;
        }

        public void ThrowExceptionIfHasErrors()
        {
            if (_errors.Any())
                throw new DomainException(_errors);
        }

        public bool HasErrors()
        {
            return _errors.Any();
        }

        public List<string> GetErrors()
        {
            return _errors;
        }
    }
}
