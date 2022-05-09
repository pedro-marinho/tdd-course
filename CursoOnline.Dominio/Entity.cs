using CursoOnline.Utils;

namespace CursoOnline.Dominio
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public DomainValidator Validator { get; protected set; }

        public bool IsValid => !Validator.HasErrors();

        public Entity()
        {
            Validator = DomainValidator.New();
        }

        public void Check()
        {
            Validator.ThrowExceptionIfHasErrors();
        }
    }
}
