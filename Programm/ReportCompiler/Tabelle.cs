//Tabelle.cs
//compile with: /doc:Tabelle.xml
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ReportCompiler
{
    /// <summary>
    /// Schreibt alle Werte eines table Knoten
    /// welches sich in der XML Datei befindet,
    /// in ein Tabellen Objekt.
    /// </summary>
    public class Tabelle
    {
        /// <summary>
        /// Property für die Verbindungs-Id der Tabelle (Unterknoten)
        /// </summary>
        public string Id
        {
            get;
            set;
        }
        /// <summary>
        /// Property über den Schriftstil des Tabellenkopfs (Unterknoten)
        /// </summary>
        public bool Bold
        {
            get;
            set;
        }
        /// <summary>
        /// Property für die Hintergrundfarbe des Tabellenkopfs (Unterknoten)
        /// </summary>
        public string BgColor
        {
            get;
            set;
        }
        /// <summary>
        /// Property für die Datenbankabfrage (Unterknoten)
        /// </summary>
        public string Text
        {
            get;
            set;
        }
        /// <summary>
        /// Property für das Resultat aus der Datenbankabfrage (Unterknoten)
        /// </summary>
        public DataTable Content
        {
            get;
            set;
        }
        /// <summary>
        /// Property für den Tabellenrand (Unterknoten)
        /// </summary>
        public bool Border
        {
            get;
            set;
        }
        /// <summary>
        /// Property für die Textgrösse innerhalb der Tabelle
        /// </summary>
        public int Size
        {
            get;
            set;
        }
        /// <summary>
        /// Property für die Absatzgrösse nach der Tabelle
        /// </summary>
        public int Paragraph
        {
            get;
            set;
        }
        /// <summary>
        /// Property für die Schrifart des Textes innerhalb der Tabelle
        /// </summary>
        public string Font
        {
            get;
            set;
        }
        /// <summary>
        /// Dient zur Abfrage der SQL Datenbank
        /// In der Connections Liste wird die entsprechende Id gesucht
        /// Danach wird anhand des Typs der Datenbank (Microsoft SQL oder MySQL) ein neues Objekt für die Datenbankabfrage instaziiert
        /// Nach dem das Datenbank Objekt instanziiert wurde wird die Abfrage durchgeführt und in der Content Eigenschaft geschrieben
        /// Falls die Abfrage eine Exception geworfen hat, wird diese gefangen und weitergeleitet.
        /// </summary>
        public void getContent(List<Connection> connections)
        {
            Connection connection;
            try
            {
                connection = connections.Single(s => s.Id == Id);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Connector connector;
            if (connection.Type == "MSSQL")
                connector = new MSConnector(connection);
            else
                connector = new MyConnector(connection);
            try
            {
                connector.Connect();
                Content = connector.loadTable(Text);
                connector.Close();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                throw;
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                throw;
            }
        }
    }
}
