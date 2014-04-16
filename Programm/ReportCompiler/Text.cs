//Text.cs
//compile with: /doc:Text.xml
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCompiler
{
    /// <summary>
    /// Schreibt alle Werte eines Text Knoten
    /// welches sich in der XML Datei befindet,
    /// in ein Text Objekt.
    /// </summary>
    public class Text
    {
        /// <summary>
        /// Property für den Inhalt Texts ab (Unterknoten)
        /// </summary>
        public string Content
        {
            get;
            set;
        }
        /// <summary>
        /// Property für die Schriftart des Texts ab (Unterknoten)
        /// </summary>
        public string Font
        {
            get;
            set;
        }
        /// <summary>
        /// Property für die Schriftgrösse des Texts ab (Unterknoten)
        /// </summary>
        public int Size
        {
            get;
            set;
        }
        /// <summary>
        /// Property für den Schriftstil des Texts ab (Unterknoten)
        /// </summary>
        public int Style
        {
            get;
            set;
        }
        /// <summary>
        /// Property für die Schriftfarbe des Texts ab (Unterknoten)
        /// </summary>
        public string Color
        {
            get;
            set;
        }
        /// <summary>
        /// Property für die Absatzgrösse nach dem Text (Unterknoten)
        /// </summary>
        public int Paragraph
        {
            get;
            set;
        }
        /// <summary>
        /// Property für den Seitenumbruch nach dem Text (Unterknoten)
        /// </summary>
        public bool PageBreak
        {
            get;
            set;
        }
    }
}
