//OpenDocumentException.cs
//compile with: /doc:OpenDocumentException.xml
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ReportCompiler
{
    /// <summary>
    /// Eigene Exception wird ausgelöst, falls eine Exception während dem Aufbau des Dokuments geworfen wird. Sie erbt von der Klasse Exception
    /// </summary>
    public class OpenDocumentException : Exception
    {
        /// <summary>
        /// Exception ohne Parameter
        /// </summary>
        public OpenDocumentException() : base() { }
        /// <summary>
        /// Exception mit gewünschter Nachricht
        /// </summary>
        /// <param name="message">Gewünschte Nachricht</param>
        public OpenDocumentException(string message) : base(message) {}
        /// <summary>
        /// Exception mit gewünschter Nachricht und innerer Exception
        /// </summary>
        /// <param name="message">Gewünschte Nachricht</param>
        /// <param name="inner">Innere Exception</param>
        public OpenDocumentException(string message, Exception inner) : base(message, inner) { }
        
    }
}
