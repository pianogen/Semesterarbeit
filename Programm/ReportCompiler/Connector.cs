// Connector.cs
// compile with:ReportCompiler.exe /doc:Connector.xml
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ReportCompiler
{
    /// <summary>
    /// Interface für die beiden Typen von Datenbankverbindung
    /// </summary>
    public interface Connector
    {
        /// <summary>
        /// Methode, um eine Datenbankverbindung zu öffnen
        /// </summary>
        void Connect();
        /// <summary>
        /// Methode, um eine Datenbankverbindung zu schliessen
        /// </summary>
        void Close();
        /// <summary>
        /// Methode, um eine Variable von einer Datenbank auszulesen
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string Result(string text);
        /// <summary>
        /// Gibt eine Tabelle aus einer Datenbankabfrage zurück
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        DataTable loadTable(string text);
    }
}
