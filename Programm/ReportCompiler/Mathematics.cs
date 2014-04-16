//Mathematics.xml
//compile with /doc:Mathematics.xml
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCompiler
{
    /// <summary>
    /// Schreibt alle Werte aller Mathemetik Knoten 
    /// die sich in der XML Datei befinden,
    /// in jeweils einem Mathematic Objekt.
    /// </summary>
    public class Mathematics
    {
        /// <summary>
        /// Property für den Namen der algebraischen Formel (Unterknoten)
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Property für die Formel der algebraischen Formel (Unterknoten)
        /// </summary>
        public string Formula
        {
            get;
            set;
        }
        /// <summary>
        /// Property für das Resultat der algebraischen Formel
        /// </summary>
        public decimal Result
        {
            get;
            set;
        }
    }
}
