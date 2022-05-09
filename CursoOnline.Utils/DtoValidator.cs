using System;

namespace CursoOnline.Utils
{
    public static class DtoValidator
    {
        public static void ValidateEnum<T>(string enumText, DomainValidator validator, string errorMessage, out T resultEnum) where T : struct
        {
            var isValidEnum = Enum.TryParse(enumText, out resultEnum);
            validator.When(!isValidEnum, errorMessage);
        }
    }
}
