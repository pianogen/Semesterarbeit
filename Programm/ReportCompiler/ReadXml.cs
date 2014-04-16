//ReadXml.cs
//compile with: /doc:ReadXml.xml
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ReportCompiler
{
    /// <summary>
    /// In dieser Klasse wird das gesamte XML Dokument sequentiell abgearbeitet
    /// </summary>
    public class ReadXml
    {
        /// <summary>
        /// Speicher für den Pfad des XML Dokuments
        /// </summary>
        private string file;
        /// <summary>
        /// Speicher für das Word Dokument
        /// </summary>
        private ImportInWord doc;
        /// <summary>
        /// Speicher für eine Liste aller gesetzten Variablen
        /// </summary>
        private List<Variabel> variables = new List<Variabel>();
        /// <summary>
        /// Speicher für das Rechnungsobjekt
        /// </summary>
        private MathParser mathParser;
        /// <summary>
        /// Speicher für den XML Leser (XmlReader)
        /// </summary>
        private XmlReader reader;

        private List<Connection> connections = new List<Connection>();

        /// <summary>
        /// Konstruktor der Klasse
        /// Setzt den Pfad der Xml Datei und
        /// instanziert das Objekt für das zukünftige Word Dokument
        /// </summary>
        /// <param name="file">Beinhaltet den Pfad oder den Inhalt der Xml Datei</param>
        /// <param name="doc">Beinhaltet das Objekt des zukünftigen Word Dokuments</param>
        public ReadXml(string file, ImportInWord doc)
        {
            // TODO: Complete member initialization
            this.file = file;
            this.doc = doc;
        }
        /// <summary>
        /// Liest das XML Dokument mit Hilfe des Pfads ein
        /// </summary>
        public void CreateXml()
        {
            reader = XmlReader.Create(file);
        }
        /// <summary>
        /// Liest das XML Dokument welche als Klartext übergeben wurde ein
        /// </summary>
        public void OpenXml()
        {
            reader = XmlReader.Create(new System.IO.StringReader(file));
        }
        /// <summary>
        /// Gibt eine Liste aller gesetzten Variablen zurück
        /// </summary>
        public List<Variabel> ListOfVariables
        {
            get { return variables; }
        }
        /// <summary>
        /// Fügt eine Variable der Liste hinzu
        /// </summary>
        /// <param name="variable">Eine Variable mit allen Parameter</param>
        public void AddVariable(Variabel variable)
        {
            variables.Add(variable);
        }
        /// <summary>
        /// Gibt eine Liste aller verfügbaren Datenbankverbindungen zurück
        /// </summary>
        public List<Connection> ListOfConnections
        {
            get { return connections; }
        }
        /// <summary>
        /// Fügt eine Verbindung mit all den Parameter züruck
        /// </summary>
        /// <param name="connection"></param>
        public void AddConnection(Connection connection)
        {
            connections.Add(connection);
        }
        /// <summary>
        /// Erste Stufe der Verarbeitung des XML Dokuments
        /// Es wird überprüft, ob ein gewünschtes Start-Element vorkommt
        /// Sobald er fündig wird, wird die entsprechende Methode aufgerufen.
        /// Falls die Methode einen Exception zurückgibt, wird sie gefangen und weitergeleitet.
        /// </summary>
        ///  
        public void XML()
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "connections":
                            Connection(reader.ReadOuterXml());
                            break;
                        case "variables":
                            try
                            {
                                Variable(reader.ReadOuterXml());
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            break;
                        case "maths":
                            Mathe(reader.ReadOuterXml());
                            break;
                        case "format":
                            try
                            {
                                Format(reader.ReadOuterXml());
                            }
                            catch (System.IO.IOException)
                            {
                                reader.Close();
                                throw;
                            }
                            catch (UnauthorizedAccessException)
                            {
                                reader.Close();
                                throw;
                            }
                            break;
                        case "text":
                            try
                            {
                                Text(reader.ReadOuterXml());
                            }
                            catch (OpenDocumentException)
                            {
                                reader.Close();
                                throw;
                            }
                            break;
                        case "table":
                            try
                            {
                                Tabelle(reader.ReadOuterXml());
                            }
                            catch (OpenDocumentException)
                            {
                                throw;
                            }
                            break;
                    }
                }
            }
            if (variables.Count > 0)
                doc.Replace(variables);
            reader.Close();
        }
        /// <summary>
        /// Der Knoten Format wird verarbeitet
        /// Es wird ein Objekt des Typs PageSetup instanziiert und
        /// die gewünschten Eigenschaften übergeben.
        /// Sobald der Knoten abgearbeitet wird, werden im Word Dokument die Eigenschaften übernommen.
        /// Beim Vorkommen einer Exception wird sie gefangen und weitergeleitet.
        /// </summary>
        /// <param name="xml">Als Parameter wird der gesamte Knoten Format und seine Knoten übergeben</param>
        public void Format(string xml)
        {
            XmlReader formatReader = XmlReader.Create(new System.IO.StringReader(xml));
            PageSetup format = new PageSetup();
            while (formatReader.Read())
            {
                if (formatReader.NodeType == XmlNodeType.Element)
                {
                    switch (formatReader.Name)
                    {
                        case "savepath":
                            format.Save = formatReader.ReadElementString();
                            break;
                        case "templatepath":
                            format.Template = formatReader.ReadElementString();
                            break;
                    }
                }
            }
            formatReader.Close();
            try
            {
                doc.PageSetup(format);
            }
            catch (System.IO.IOException)
            {
                throw;
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
        }
        /// <summary>
        /// Der Knoten Connections wird verarbeitet
        /// Sobald der XmlReader einen Knoten namens Connection findet instanziiert er ein neues Connection Objekt
        /// ,die gewünschten Eigenschaften übergeben und das gesamte Objekt der Liste Connections hinzugefügt.
        /// 
        /// </summary>
        /// <param name="xml">Als Parameter wird der Knoten Connections und all seine Unterknoten übergeben</param>
        public void Connection(string xml)
        {
            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xml));
            Connection connection = null;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "connection":
                            connection = new Connection();
                            AddConnection(connection);
                            break;
                        case "id":
                            connection.Id = reader.ReadElementString();
                            break;
                        case "type":
                            connection.Type = reader.ReadElementString();
                            break;
                        case "server":
                            connection.Server = reader.ReadElementString();
                            break;
                        case "database":
                            connection.Database = reader.ReadElementString();
                            break;
                        case "user":
                            connection.Username = reader.ReadElementString();
                            break;
                        case "password":
                            connection.Password = reader.ReadElementString();
                            break;
                    }
                }     
            }
            reader.Close();
        }
        /// <summary>
        /// Der Knoten variables wird verarbeitet
        /// Sobald der XmlReader einen Unterknoten variable einliest wird ein neues Variabel Objekt instanziiert und die gewünschten Eigenschaften übergeben.
        /// Danach wird der Inhalt der Variable ausgewertet, falls eine Exception zurückgegeben wird, wird sie gefangen und weitergeleitet.
        /// Die ausgewertete Variable wird zur Liste der gesetzen Variablen hinzugefügt. 
        /// </summary>
        /// <param name="xml">Als Parameter wird der Knoten variables und all seine Unterknoten übergebenAls Parameter wird der Knoten Connections und all seine Unterknoten übergeben</param>
        public void Variable(string xml)
        {
            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xml));
            while (reader.Read())
            {
                if (reader.Name == "variable" && reader.NodeType == XmlNodeType.Element)
                {
                    XmlReader variableReader = XmlReader.Create(new System.IO.StringReader(reader.ReadOuterXml()));
                    Variabel variable = new Variabel();
                    while (variableReader.Read())
                    {
                        if (variableReader.NodeType == XmlNodeType.Element)
                        {
                            switch (variableReader.Name)
                            {
                                case "id":
                                    variable.Id = variableReader.ReadElementString();
                                    break;
                                case "name":
                                    variable.Name = variableReader.ReadElementString();
                                    break;
                                case "type":
                                    variable.Type = variableReader.ReadElementString();
                                    break;
                                case "source":
                                    variable.Source = variableReader.ReadElementString();
                                    break;
                                case "content":
                                    variable.Text = variableReader.ReadElementString();
                                    break;
                            }
                        }
                    }
                    variableReader.Close();
                    try
                    {
                        variable.getContent(connections);
                    }
                    catch (MySql.Data.MySqlClient.MySqlException)
                    {
                        throw;
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        throw;
                    }
                    catch (InvalidOperationException)
                    {
                        throw new Exception("Datenbankanbindg " + variable.Id + " für Variable mit Name " + variable.Name + " nicht gefunden");
                    }
                    catch (FormatException)
                    {
                        throw;
                    }
                    catch (OverflowException)
                    {
                        throw;
                    }
                    variables.Add(variable);
                }
            }
            reader.Close();
            mathParser = new MathParser(variables);
        }
        /// <summary>
        /// Der Knoten maths wird verarbeitet
        /// Sobald der XmlReader einen Knoten namens math findet instanziiert er ein neues Mathematics Objekt und übergibt die gewünschten Eigenschaften.
        /// Danach wertet er die Formel über die Klasse MathParser aus.
        /// Das Resultat wird als lokale int Variable gesetzt und in der Liste der gesetzten Variablen hinzugefügt
        /// </summary>
        /// <param name="xml">Als Parameter wird der Knoten Maths und all seine Unterknoten übergeben</param>
        public void Mathe(string xml)
        {

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xml));
            while (reader.Read())
            {
                if (reader.Name == "math" && reader.NodeType == XmlNodeType.Element)
                {
                    XmlReader mathReader = XmlReader.Create(new System.IO.StringReader(reader.ReadOuterXml()));
                    Mathematics math = new Mathematics();
                    while (mathReader.Read())
                    {
                        if (mathReader.NodeType == XmlNodeType.Element)
                        {
                            switch (mathReader.Name)
                            {
                                case "name":
                                    math.Name = mathReader.ReadElementString();
                                    break;
                                case "formula":
                                    math.Formula = mathReader.ReadElementString().Replace(" ","");
                                    break;
                            }
                        }
                    }
                    mathReader.Close();
                    math.Result = mathParser.Calculate(math.Formula);
                    Variabel variable = new Variabel();
                    variable.Name = math.Name;
                    variable.Type = "int";
                    variable.Source = "local";
                    variable.Number = math.Result;
                    variables.Add(variable);
                }
            }
            reader.Close();
        }
        /// <summary>
        /// Hier wird der Knoten Text ausgewertet
        /// Es wird ein neues Text Objekt instanziiert und die entsprechenden Eigenschaften übergeben
        /// Falls eine Exception beim Casten der Werte geworfen wird, wird diese gefangen und weitergeleitet
        /// Bei erfolreicher Erstellung des Objekts wird dieses in dem Word Dokument eingefügt
        /// </summary>
        /// <param name="xml">Der Parameter beinhaltet einen Textknoten mit all seinen Unterknoten</param>
        public void Text(string xml)
        {
            XmlTextReader subReader = new XmlTextReader(new System.IO.StringReader(xml));
            Text text = new Text();
            while (subReader.Read())
            {
                if (subReader.NodeType == XmlNodeType.Element)
                {
                    switch (subReader.Name)
                    {
                        case "string":
                            text.Content = subReader.ReadElementContentAsString();
                            break;
                        case "font":
                            text.Font = subReader.ReadElementString();
                            break;
                        case "size":
                            try
                            {
                                text.Size = subReader.ReadElementContentAsInt();
                            }
                            catch (XmlException e)
                            {
                                subReader.Close();
                                throw new OpenDocumentException(e.Message);
                            }
                            break;
                        case "paragraph":
                            try
                            {
                                text.Paragraph = subReader.ReadElementContentAsInt();
                            }
                            catch (XmlException e)
                            {
                                subReader.Close();
                                throw new OpenDocumentException(e.Message);
                            }
                            break;
                        case "style":
                            try
                            {
                                text.Style = subReader.ReadElementContentAsInt();
                            }
                            catch (XmlException e)
                            {
                                subReader.Close();
                                throw new OpenDocumentException(e.Message);
                            }
                            break;
                        case "color":
                            text.Color = subReader.ReadElementString();
                            break;
                        case "pageBreak":
                            try
                            {
                                text.PageBreak = subReader.ReadElementContentAsBoolean();
                            }
                            catch (XmlException e)
                            {
                                subReader.Close();
                                throw new OpenDocumentException(e.Message);
                            }
                            break;
                    }
                }
            }
            subReader.Close();
            doc.Text(text);
        }
        /// <summary>
        /// Dient zur Auswertung des Knoten Tabelle
        /// Es wird eine Tabellen Objekt instanziiert und die entsprechenden Eigenschaften übernommen.
        /// Falls eine Exception beim Casten der Werte geworfen wird, wird diese gefangen und weitergeleitet
        /// Bei erfolgreicher Erstellung des Objekts wird der Tabelleninhalt über eine Datenbankanbindung abgefragt.
        /// Falls eine Exception bei der Abfrage geworfen wird, wird diese gefangen und weitergeleitet.
        /// Bei erfolgreicher Abfrage wird die Tabelle im Word Dokument hinzugefügt
        /// </summary>
        /// <param name="xml"></param>
        public void Tabelle(string xml)
        {
            XmlTextReader subReader = new XmlTextReader(new System.IO.StringReader(xml));
            Tabelle table = new Tabelle();
            while (subReader.Read())
            {
                if (subReader.NodeType == XmlNodeType.Element)
                {
                    switch (subReader.Name)
                    {
                        case "id":
                            table.Id = subReader.ReadElementString();
                            break;
                        case "bold":
                            try
                            {
                                table.Bold = subReader.ReadElementContentAsBoolean();
                            }
                            catch (XmlException e)
                            {
                                subReader.Close();
                                throw new OpenDocumentException(e.Message);
                            }
                            break;
                        case "background":
                            table.BgColor = subReader.ReadElementString();
                            break;
                        case "sql":
                            table.Text = subReader.ReadElementString();
                            break;
                        case "border":
                            try
                            {
                                table.Border = subReader.ReadElementContentAsBoolean();
                            }
                            catch (XmlException e)
                            {
                                subReader.Close();
                                throw new OpenDocumentException(e.Message);
                            }
                            break;
                        case "font":
                            table.Font = subReader.ReadElementString();
                            break;
                        case "size":
                            try
                            {
                                table.Size = subReader.ReadElementContentAsInt();
                            }
                            catch (XmlException e)
                            {
                                subReader.Close();
                                throw new OpenDocumentException(e.Message);
                            }
                            break;
                        case "paragraph":
                            try
                            {
                                table.Paragraph = subReader.ReadElementContentAsInt();
                            }
                            catch (XmlException e)
                            {
                                subReader.Close();
                                throw new OpenDocumentException(e.Message);
                            }
                            break;
                    }
                }
            }
            subReader.Close();
            try
            {
                table.getContent(connections);
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                throw new OpenDocumentException(e.Message);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new OpenDocumentException(e.Message);
            }
            catch (InvalidCastException)
            {
                throw new OpenDocumentException("Datenbankverbindung " + table.Id + " für Tabelle mit Abfrage " + table.Text + " nicht gefunden");
            }
            catch (Exception e)
            {
                throw new OpenDocumentException(e.Message);
            }
            doc.Table(table);
        }
    }
}
