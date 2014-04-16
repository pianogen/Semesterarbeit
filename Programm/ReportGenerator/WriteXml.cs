using System;
//WriteXml.cs
//compile with: /doc:WriteXml.xml
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ReportGenerator
{
    /// <summary>
    /// Diese Klasse ist für den gesamten Aufbau des XML Dokuments zuständig
    /// </summary>
    public class WriteXml
    {
        /// <summary>
        /// Speicher für ein XDocument Objekt
        /// </summary>
        private XDocument doc;
        /// <summary>
        /// Konstruktor der Klasse
        /// Instanziert ein neues XDocument Objekt mit den benötigten Informationen
        /// </summary>
        public WriteXml()
        {
            doc = new XDocument(
                new XDeclaration("1.0","iso-8859-1","yes"),
                new XElement("rapport",
                    new XElement("connections") , 
                    new XElement("variables"), 
                    new XElement("maths"),
                    new XElement("document",
                        new XElement("format")
                    )
                )
            );
        }
        /// <summary>
        /// Fügt dem XDocument Objekt die Format Parameter hinzu
        /// </summary>
        /// <param name="pageSetup">Ein PageSetup Objekt, welches die gewünschten Parameter beinhaltet</param>
        public void Format(PageSetup pageSetup)
        {
            doc.Root.Element("document").Element("format").Add(
                new XElement("savepath", pageSetup.Save),
                new XElement("templatepath", pageSetup.TemplatePath)
            );
        }
        /// <summary>
        /// Fügt dem XDocument Objekt einen Connection Knoten mitsamt Parameter hinzu
        /// </summary>
        /// <param name="connection">Ein Connection Objekt, welches die gewünschten Parameter beinhaltet</param>
        public void Connection(Connection connection)
        {
            doc.Root.Element("connections").Add(
                new XElement("connection",
                    new XElement("id", connection.Id),
                    new XElement("type", connection.Type),
                    new XElement("server", connection.ConnectionString),
                    new XElement("database", connection.Database),
                    new XElement("user", connection.User),
                    new XElement("password", connection.Password)
                )
             );
        }
        /// <summary>
        /// Fügt dem XDocument Objekt einen Variable Konten mitsamt Paramter hinzu
        /// </summary>
        /// <param name="variable">Ein Variabel Objekt, welches die gewünschten Paramter beinhaltet</param>
        public void Variables(Variabel variable)
        {
            doc.Root.Element("variables").Add(
                new XElement("variable",
                    new XElement("id", variable.Id),
                    new XElement("name", variable.Identificator),
                    new XElement("type", variable.Type),
                    new XElement("source", variable.Source),
                    new XElement("content", variable.Text)
                )
            );
        }
        /// <summary>
        /// Fügt dem XDocument Objekt einen Math Knoten mitsamt Parameter hinzu
        /// </summary>
        /// <param name="mathe">Ein Mathematic Objekt, welches die gewünschten Paramter beihnaltet</param>
        public void Mathe(Mathematic mathe)
        {
            doc.Root.Element("maths").Add(
                new XElement("math",
                    new XElement("name", mathe.Identificator),
                    new XElement("formula", mathe.Formula)
                )
             );
        }
        /// <summary>
        /// Fügt dem XDocument Objekt einen Textknoten mitsamt Parameter hinzu
        /// </summary>
        /// <param name="text">Ein Text Objekt welches die gewünschten Paramter beinhaltet</param>
        public void Text(Text text)
        {
            doc.Root.Element("document").Add(
                new XElement("text",
                    new XElement("string", text.ClearType),
                    new XElement("font", text.Font),
                    new XElement("size", text.Size),
                    new XElement("paragraph", text.Paragraph),
                    new XElement("style", text.StyleConverter),
                    new XElement("color", text.Color)
                )
            );
        }
        /// <summary>
        /// Erstellt einen "orphanen" Textknoten mitsamt Parameter und wandelt es in einen string um
        /// </summary>
        /// <param name="text">Ein Text Objekt welches die gewünschten Parameter beinhaltet</param>
        /// <returns>Den gesamten Textknoten als string</returns>
        public static string TextManually(Text text)
        {
            XElement element = new XElement("text",
                new XElement("string", text.ClearType),
                new XElement("font", text.Font),
                new XElement("size", text.Size),
                new XElement("paragraph", text.Paragraph),
                new XElement("style", text.StyleConverter),
                new XElement("color", text.Color)
            );
            return element.ToString();
        }

        /// <summary>
        /// Fügt dem XDocument Objekt einen Titel mitsamt Parameter hinzu
        /// </summary>
        /// <param name="titel">Ein Titel Objekt welches die gewünschten Paramter beinhaltet</param>
        public void Titel(Titel titel)
        {
            doc.Root.Element("document").Add(
                new XElement("text",
                    new XElement("string", titel.ClearType),
                    new XElement("font", titel.Font),
                    new XElement("size", titel.Size),
                    new XElement("paragraph", titel.Paragraph),
                    new XElement("style", titel.StyleConverter),
                    new XElement("color", titel.Color),
                    new XElement("pageBreak", titel.PageBreak)
                )
            );
        }

        /// <summary>
        /// Erstellt einen "orphanen" Titelknoten mitsamt Parameter und wandelt es in einen string um
        /// </summary>
        /// <param name="titel">Ein Titel Objekt welches die gewünschten Parameter beinhaltet</param>
        /// <returns>Den gesamten Titelknoten als string</returns>
        public static string TitelManually(Titel titel)
        {
            XElement element = new XElement("text",
                new XElement("string", titel.ClearType),
                new XElement("font", titel.Font),
                new XElement("size", titel.Size),
                new XElement("paragraph", titel.Paragraph),
                new XElement("style", titel.StyleConverter),
                new XElement("color", titel.Color),
                new XElement("pageBreak", titel.PageBreak)
            );
            return element.ToString();
        }

        /// <summary>
        /// Fügt dem XDocument Objekt einen Tabellenknoten mitsamt Parameter hinzu
        /// </summary>
        /// <param name="table">Ein Tabellenobjekt welches die gewünschten Paramter beinhaltet</param>
        public void Tabelle(Tabelle table)
        {
            doc.Root.Element("document").Add(
                new XElement("table",
                    new XElement("id", table.Id),
                    new XElement("bold", table.Bold),
                    new XElement("background", table.BgColor),
                    new XElement("border", table.Border),
                    new XElement("sql", table.Sql),
                    new XElement("font", table.Font),
                    new XElement("size", table.Size),
                    new XElement("paragraph", table.Paragraph)
                )
             );
        }

        /// <summary>
        /// Erstellt einen "orphanen" Tabellenknoten mitsamt Parameter und wandelt es in einen string um
        /// </summary>
        /// <param name="table">Ein Tabellenobjekt welches die gewünschten Parameter beinhaltet</param>
        /// <returns>Den gesamten Tabellenknoten als string</returns>
        public static string TabelleManually(Tabelle table)
        {
            XElement element = new XElement("table",
                new XElement("id", table.Id),
                new XElement("bold", table.Bold),
                new XElement("background", table.BgColor),
                new XElement("border", table.Border),
                new XElement("sql", table.Sql),
                new XElement("font", table.Font),
                new XElement("size", table.Size),
                new XElement("paragraph", table.Paragraph)
             );
            return element.ToString();
        }
        /// <summary>
        /// Liest alle gesetzen Verbindungen eines XML Objekts aus und speichert die Id
        /// </summary>
        /// <returns>Eine Liste mit all den Verbindungen</returns>
        public List<string> ReadConnections()
        {
            List<string> connections = new List<string>();
            var q = from b in doc.Descendants("connection")
                    select new
                    {
                        id = (string)b.Element("id")
                    };
            foreach (var t in q)
                connections.Add(t.id);
            return connections;
        }
        /// <summary>
        /// Liest alle Variablen aus einem XML Objekt aus und speichert den Namen und den Typ
        /// </summary>
        /// <returns>Eine sortierte Liste, die alle Namen und Typen der gesetzten Variablen beinhalten</returns>
        public SortedList<string,string> ReadVariables()
        {
            SortedList<string, string> variables = new SortedList<string, string>();
            var q = from b in doc.Descendants("variable")
                    select new
                    {
                        name = (string)b.Element("name"),
                        type = (string)b.Element("type")
                    };
            foreach (var t in q)
                variables.Add(t.name, t.type);
            return variables;
        }
        /// <summary>
        /// Gibt den gesamte XML Objekt (Deklaration inbegriffen) als string zurück
        /// </summary>
        /// <returns>Gesamtes XML Objekt als string</returns>
        public string getXML()
        {
            try
            {
                return doc.Declaration.ToString() + Environment.NewLine + doc.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Speichert das XML Objekt in ein Xml Dokument auf dem Dateisystem
        /// </summary>
        /// <param name="path">Der gewünschte Pfad mitsamt Dateinamen</param>
        public void Save(string path)
        {
            try
            {
                doc.Save(path);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message, "Fehler beim Speichern");
            }
        }
        /// <summary>
        /// Parset den gesamten Inhalt der Textbox in das XDocument Objekt ein 
        /// </summary>
        /// <param name="xml">Der gesamte Inhalt der Textbox</param>
        public void WriteFromString(string xml)
        {
            try
            {
                doc = XDocument.Parse(xml, LoadOptions.SetBaseUri);
            }
            catch (XmlException)
            {
                throw;
            }
        }
        /// <summary>
        /// Öffnet eine existierende Xml Datei auf dem Dateisystem und schreibt es in das XDocument Objekt
        /// </summary>
        /// <param name="fileName">Dateipfad der Xml Datei</param>
        public void Open(string fileName)
        {
            try
            {
                doc = XDocument.Load(fileName, LoadOptions.SetBaseUri);

            }
            catch (XmlException)
            {
                throw;
            }
        }
    }
}
