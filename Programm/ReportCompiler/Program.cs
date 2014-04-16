//Program.cs
//compile with: /doc:Program.xml
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportCompiler
{
    /// <summary>
    /// Hier startet die Ausführung des ReportCompilers
    /// </summary>
    public class Program
    {
   
        /// <summary>
        /// Speicher für den Speicherort der XML Datei
        /// </summary>
        private static string path;
        /// <summary>
        /// Führt das Programm aus
        /// Falls im Programm irgendeine Exception geworfen wurde
        /// wird sie hier abgefangen und die Laufzeit abgebrochen.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Program.path = Program.OpenXML(args);
            if (Program.path != null)
            {
                if (FileType(Program.path))
                {
                    ImportInWord doc = new ImportInWord();
                    ReadXml xml = new ReadXml(path, doc);
                    xml.CreateXml();
                    Console.WriteLine("Dokument wird erstellt");
                    try
                    {
                        xml.XML();
                        doc.Exit();
                        Console.WriteLine("Dokument erfolgreich gespeichert");
                    }
                    catch (OpenDocumentException e)
                    {
                        Console.WriteLine("Fehler während der Laufzeit");
                        Console.WriteLine();
                        Console.WriteLine("Grund: " + e.Message);
                        Console.WriteLine();
                        Console.WriteLine("Erstellte Datei wurde gelöscht");
                        doc.Quit();
                        Console.WriteLine("Programm mit Fehler beendet");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Fehler während der Laufzeit");
                        Console.WriteLine();
                        Console.WriteLine("Grund: " + e.Message);
                        Console.WriteLine();
                        Console.WriteLine("Dokument wurde nicht erstellt");
                        Console.WriteLine("Programm mit Fehler beendet");
                    }
                }
                else
                {
                    Console.WriteLine("Angegebene Datei ist keine XML Datei");
                    Console.WriteLine("Programm wird beendet");
                }
            }
        }
        /// <summary>
        /// Überprüft ob der angegebene Pfad der XML Datei gültig ist
        /// </summary>
        /// <param name="args"></param>
        public static string OpenXML(string[] args)
        {
            string temppath;
            if (args.Length == 1)
            {
                temppath = args[0];
                if (File.Exists(temppath))
                    return temppath;
                else
                    Console.WriteLine("Pfad ungültig");
            }
            else
            {
                Console.WriteLine("Bitte geben Sie den Pfad der gewünschten XML Datei an:");
                temppath = Console.ReadLine();
                if (!File.Exists(temppath))
                {
                    Console.WriteLine("Datei nicht gefunden");
                    Console.WriteLine("Bitte geben Sie den Pfad der gewünschten XML Datei an:");
                    temppath = Program.OpenXML(new string[0]);
                }                
            }
            return temppath;
        }
        /// <summary>
        /// Überprüft ob die angegebene Datei eine XML Datei ist.
        /// </summary>
        /// <param name="file">Datei, welche der Benutzer gerne einlesen möchte</param>
        /// <returns>Gibt einen Boolean Wert zurück, ob Datei eine Xml Datei ist oder nicht.</returns>
        public static bool FileType(string file)
        {
            string extension = Path.GetExtension(file);
            {
                if (extension == ".xml")
                    return true;
                return false;
            }
        }
    }
}
