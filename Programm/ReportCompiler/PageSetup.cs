//PageSetup.cs
//compile with: /doc:PageSetup.xml
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCompiler
{
    /// <summary>
    /// Schreibt alle Werte des Formatknotens,
    /// welches sich in der XML Datei befindet,
    /// in ein PageSetup Objekt.
    /// </summary>
    public class PageSetup
    {
        /// <summary>
        /// Property für den Pfad des zu erstellenden Dokuments (Unterknoten)
        /// </summary>
        public string Save
        {
            get;
            set;
        }
        /// <summary>
        /// Property für den Pfad der Vorlage (Unterknoten)
        /// </summary>
        public string Template
        {
            get;
            set;
        }
    }
}
