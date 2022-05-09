using CursoOnline.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CursoOnline.Dominio.Test.Extensions
{
    public static class AssertExtensions
    { 
        public static void WithMessage(this DomainException exception, string message)
        {
            if (exception.Errors.Contains(message))
                Assert.True(true);
            else
                Assert.False(true);
        }
    }
}
