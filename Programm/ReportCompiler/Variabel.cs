//Variabel.cs
//compile with: /doc:Variabel.xml
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCompiler
{
    /// <summary>
    /// Schreibt alle Werte aller Variablen 
    /// die sich in der XML Datei befinden,
    /// in jeweils einem Variabel Objekt.
    /// </summary>
    public class Variabel
    {
        /// <summary>
        /// Property für die Verbindungs-Id der Variable (Unterknoten)
        /// </summary>
        public string Id
        {
            get;
            set;
        }
        /// <summary>
        /// Property für den Variablennamen (Unterknoten)
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Property für den Typ der Variable (Unterknoten)
        /// </summary>
        public string Type
        {
            get;
            set;
        }
        /// <summary>
        /// Property für die Quelle der Variable (Unterknoten)
        /// </summary>
        public string Source
        {
            get;
            set;
        }
        /// <summary>
        /// Property für den Quelltext der Variable (Unterknoten)
        /// </summary>
        public string Text
        {
            get;
            set;
        }
        /// <summary>
        /// Property für das Resultat und den endgültigen Wert der Variable
        /// Wird gefüllt, falls der Typ ein string ist
        /// </summary>
        public string Content
        {
            get;
            set;
        }
        /// <summary>
        /// Property für das Resultat und den endgültigen Wert der Variable
        /// Wird gefüllt, falls der Typ ein int ist
        /// </summary>
        public decimal Number
        {
            get;
            set;
        }
        /// <summary>
        /// Variable wird ausgewertet
        /// Falls die Quelle lokal ist, wird die Methode getLocalContent() aufgrufen.
        /// Falls die Quelle sql ist wird zuerst die Methode getSqlContent() aufgerufen und danach die Methode getLocalContent().
        /// </summary>
        public void getContent(List<Connection> connections)
        {
            if (Source == "local")
            {
                try
                {
                    getLocalContent();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            if (Source == "sql")
            {
                try
                {
                    Text = getSqlContent(connections);
                    getLocalContent();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Überprüft ob die Variable einen numerischen oder alphabetischen Wert haben soll.
        /// Falls der Wert nummerisch sein muss. Wird der Wert gecastet und in die Property Number geschrieben.
        /// Ansonsten wird der Wert in die Property Content geschrieben.
        /// </summary>
        public void getLocalContent()
        {
            if (Type == "int")
            {
                try
                {
                    Number = ResultToDec(Text);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            if (Type == "string")
                Content = Text;
        }
        /// <summary>
        /// Zuerst wird die richtige Datenbankverbindung anhand der ID ausgefiltert. Falls die ID nicht existiert, wird eine Exception geworfen und weitergeleitet.
        /// Als nächstes wird überprüft, ob der Datenbankserver ein Microsoft SQL Server ist oder ein MySQL Server.
        /// Danach wird das Resultat mit Hilfe der Datenbankklassen ausgewertet.
        /// Bei einem Fehler wird eine Exception gefangen und weitergeleitet.
        /// </summary>
        /// <param name="connections">Liste aller verfügbaren Datenbankverbindungen</param>
        /// <returns></returns>
        public string getSqlContent(List<Connection> connections)
        {
            Connector connector;
            Connection connection;
            try
            {
                connection = connections.Single(s => s.Id == Id);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            if (connection.Type == "MSSQL")
                connector = new MSConnector(connection);
            else
                connector = new MyConnector(connection);
            try
            {
                connector.Connect();
                string result = connector.Result(Text);
                connector.Close();
                return result;
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Wandelt einen string wert in einen dezimalen Wert um
        /// Falls eine Exception zurückgegeben wird, wird diese gefangen und weitergeleiten.
        /// </summary>
        /// <param name="text">Als Parameter wird der Inhalt der Variable übergeben</param>
        /// <returns>Gibt als Resultat, die konvertierte Zahl zurück</returns>
        public decimal ResultToDec(string text)
        {
            decimal number = 0;
            try
            {
                number = Convert.ToDecimal(text);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Variable ist nicht vom Typ int");
                throw new Exception(e.Message);
            }
            catch (Exception)
            {
                throw;
            }
            return number;
        }
    }
}
