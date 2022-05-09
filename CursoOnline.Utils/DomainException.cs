using System;
using System.Collections.Generic;

namespace CursoOnline.Utils
{
    public class DomainException : ArgumentException
    {
        public List<string> Errors { get; set; }

        public DomainException(List<string> msgs)
        {
            Errors = msgs;
        }
    }
}
