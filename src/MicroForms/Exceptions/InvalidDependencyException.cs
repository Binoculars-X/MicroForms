using System;
using System.Collections.Generic;
using System.Text;

namespace MicroForms.Exceptions;
internal class InvalidDependencyException : Exception
{
    public InvalidDependencyException() : base()
    {
    }

    public InvalidDependencyException(string message) : base(message)
    {
    }
}
