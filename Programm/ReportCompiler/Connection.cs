// Connection.cs
// compile with: /doc:Connection.xml
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCompiler
{
    /// <summary>
    /// Schreibt alle Werte aller Connection 
    /// die sich in der XML Datei befinden,
    /// in jeweils einem Connection Objekt.
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Property für die Verbindugs-Id (Unterknoten)
        /// </summary>
        public string Id
        {
            get;
            set;
        }
        /// <summary>
        /// Property für den Verbindungstyp (Unterknoten)
        /// </summary>
        public string Type
        {
            get;
            set;
        }
        /// <summary>
        /// Property für die IP Adresse des Datenbankservers(Unterknoten)
        /// </summary>
        public string Server
        {
            get;
            set;
        }
        /// <summary>
        /// Property für den Datenbanknamen (Unterknoten)
        /// </summary>
        public string Database
        {
            get;
            set;
        }
        /// <summary>
        /// Property für den Benutzername der Datenbank (Unterknoten)
        /// </summary>
        public string Username
        {
            get;
            set;
        }
        /// <summary>
        /// Property für das Password des Benutzernamens (Unterknoten)
        /// </summary>
        public string Password
        {
            get;
            set;
        }
    }
}
