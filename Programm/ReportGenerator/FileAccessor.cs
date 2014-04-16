//FileAccessor.cs
//compile with: /doc:FileAccessor.html
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportGenerator
{
    /// <summary>
    /// Diese Klasse handelt den gesamten Dateizugriff der XML Datei
    /// </summary>
    public class FileAccessor
    {
        /// <summary>
        /// Statische Methode
        /// Speichert den gesamten Xml Quellcode in eine *.xml Datei 
        /// Falls die Datei bereits geladen wurde, wird sie gelöscht und neu erstellt
        /// Falls die Datei neu ist wird ein Fenster geöffnet, um den Speicherort zu bestimmen.
        /// Bei einem IO Fehler wird eine Exception geworfen.
        /// </summary>
        /// <param name="fileName">Dieser Paramater ist von dem Typ string und beihnaltet den Pfad der XML Datei</param>
        /// <param name="xml">Dieser Parameter ist vom Typ WriteXml und beinhaltet den gesamten Xml Quellcode</param>
        public static void Save(ref string fileName, WriteXml xml)
        {
            if (!(fileName == null))
            {
                try
                {
                    File.Delete(fileName);
                    xml.Save(fileName);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.Filter = "eXtensible Markup Language (*.xml)|*.xml";
                dlg.Title = "Query";
                dlg.ShowDialog();
                if (dlg.FileName != "")
                {
                    fileName = dlg.FileName;
                    try
                    {
                        xml.Save(fileName);
                    }
                    catch (System.Xml.XmlException)
                    {
                        throw;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// Statische Methode
        /// Öffnet ein Fenster, um die Datei auszuwählen, welche eingelesen werden muss
        /// Es wird ein Xml Filter gesetzt, somit werden nur Xml Dateien angezeigt.
        /// </summary>
        /// <returns>Gibt den Pfad der Xml Datei zurück</returns>
        public static string Open()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "XML files (*.xml)|*.xml";
            Nullable<bool> result = dlg.ShowDialog();
            string fileName = dlg.FileName;
            string path = "";
            try
            {
                path = System.IO.Path.GetDirectoryName(fileName);
            }
            catch (Exception)
            {
                return null;
            }
            if (result == true)
            {
                return fileName;
            }
            return null;
        }
    }
}
