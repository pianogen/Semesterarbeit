using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportGenerator
{
    /// <summary>
    /// Eigene Exception. Wird geworfen, falls beim definieren eines Word Objekt ein Textfeld leer ist.
    /// </summary>
    class EmptyTextBoxException : Exception
    {
        public EmptyTextBoxException() : base() { }
        public EmptyTextBoxException(string message) : base(message) { }
        public EmptyTextBoxException(string message, Exception innerException) : base(message, innerException) { }
    }
}
