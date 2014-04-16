//ImportInWord.cs
// compile with: /doc:ImportInWord.xml
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;


namespace ReportCompiler
{
    /// <summary>
    /// Diese Klasse erstellt das Word Dokument, fügt alle gewünschten Parameter ein und formatiert das gesamte Dokument
    /// </summary>
    public class ImportInWord
    {
        /// <summary>
        /// Speicher für die Word Objekt
        /// </summary>
        private WordprocessingDocument word;
        /// <summary>
        /// Objekt welches Speicherort und Namen der Datei beinhaltet.
        /// </summary>
        private string filename;
        /// <summary>
        /// Entnimmt aus dem Objekt PageSetup die benötigten Informationen
        /// um die Grundformatierung des Word-Dokuments durchzuführen.
        /// Dazu gehört Speicherort und Vorlage
        /// Um die Datei zu erstellen wird die Methode CreateFile aufgerufen
        /// 
        /// </summary>
        /// <param name="pageSetup"></param>
        public void PageSetup(PageSetup pageSetup)
        {
            filename = pageSetup.Save;
            CreateFile(pageSetup.Template, filename);
            word.ChangeDocumentType(WordprocessingDocumentType.Document);
            Body body = word.MainDocumentPart.Document.Body;
            word.MainDocumentPart.Document.Body.GetFirstChild<Paragraph>().Remove();
        }
        /// <summary>
        /// Erstellt das Word Dokument aus einer Vorlage
        /// Falls Datei schon vorhanden ist, oder die Vorlage nicht existiert,
        /// wird die entsprechende Exception geworfen
        /// </summary>
        /// <param name="template"></param>
        /// <param name="saveAs"></param>
        private void CreateFile(string template, string saveAs)
        {
            try
            {
                File.Copy(template, saveAs);
                word = WordprocessingDocument.Open(saveAs, true);
            }
            catch (IOException)
            {
                throw;
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
        }
        /// <summary>
        /// Erstellt einen Text im Word Dokument
        /// durch die entsprechenden Properties des Objekts Test
        /// wird die gewünschte Formatierung übernommen
        /// </summary>
        /// <param name="text"></param>
       public void Text(Text text)
        {
            Body body = word.MainDocumentPart.Document.Body;
            text.Color = TransformColor(text.Color);
            Paragraph paragraph = new Paragraph(ParagraphSpacingAfter(text.Paragraph));
            RunProperties rPr = new RunProperties(Font(text.Font));
            Run run_paragraph = new Run();
            StyleCreator(rPr, text.Style);
            rPr.Append(FontColor(text.Color), Size(text.Size));
            run_paragraph.Append(rPr);
            if (text.PageBreak == true)
                run_paragraph.Append(PBreak());
            run_paragraph.Append(InsertText(text.Content));
            paragraph.Append(run_paragraph);
            body.Append(paragraph);
        } 
        /// <summary>
        /// Erstellt eine Word-Tabelle mit der gewünschten Formatierung
        /// und fügt sie im Word Dokument ein
        /// </summary>
        /// <param name="dataTable"></param>
        public void Table(Tabelle dataTable)
        {
            Body body = word.MainDocumentPart.Document.Body;
            Table table = new Table();
            TableProperties tableProp = new TableProperties();
            TableRow t = new TableRow();
            foreach (DataColumn column in dataTable.Content.Columns)
            {
                Run run_paragraph = new Run();
                RunProperties rPr = new RunProperties(Font(dataTable.Font));
                rPr.Append(Size(dataTable.Size));
                if (dataTable.Bold == true)
                {
                    rPr.Append(new Bold());
                }
                Paragraph para = new Paragraph(ParagraphSpacingAfter(0));
                TableCellProperties headerProp = new TableCellProperties(TableBackground(dataTable.BgColor));
                if (dataTable.Border == true)
                    headerProp.Append(TableGrid());
                run_paragraph.Append(rPr);
                run_paragraph.Append(InsertText(column.ColumnName));
                para.Append(run_paragraph);
                TableCell tc = new TableCell(para);
                tc.Append(headerProp);
                t.Append(tc);
            }
            table.Append(t);

            foreach (DataRow row in dataTable.Content.Rows)
            {
                TableRow tr = new TableRow();
                foreach (DataColumn column in dataTable.Content.Columns)
                {
                    Paragraph para = new Paragraph(ParagraphSpacingAfter(0));
                    Run run_paragraph = new Run();
                    RunProperties rPr = new RunProperties(Font(dataTable.Font));
                    rPr.Append(Size(dataTable.Size));
                    run_paragraph.Append(rPr);
                    run_paragraph.Append(InsertText(row[column].ToString()));
                    para.Append(run_paragraph);
                    TableCell tc = new TableCell(para);
                    if (dataTable.Border == true)
                        tc.Append(new TableCellProperties(TableGrid()));
                    tr.Append(tc);
                }
                table.Append(tr);
            }
            table.Append(tableProp);
            body.Append(table);
        }
        /// <summary>
        /// Ersetzt alle vorhandenen Variablen im Text
        /// mit den entsprechenden Werten
        /// </summary>
        /// <param name="variables"></param>
        public void Replace(List<Variabel> variables)
        {
            Body body = word.MainDocumentPart.Document.Body;
            foreach (Variabel variable in variables)
            {
                foreach (DocumentFormat.OpenXml.Wordprocessing.Text text in body.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>())
                {
                    if (text.Text.Contains("%" + variable.Name + "%"))
                    {
                        if (variable.Type == "string")
                            text.Text = text.Text.Replace("%" + variable.Name + "%", variable.Content);
                        if (variable.Type == "int")
                            text.Text = text.Text.Replace("%" + variable.Name + "%", variable.Number.ToString());
                    }
                }
            }
        }
        /// <summary>
        /// Setzt die Textformatierung auf fett
        /// wird in der Methode Tabelle aufgerufen
        /// </summary>
        /// <returns></returns>
        private RunProperties TextBold()
        {
            return new RunProperties(new Bold());
        }
        /// <summary>
        /// Wertet den XML Knoten style aus und 
        /// setzt die entsprechende Formatierung ein
        /// 1 steht für fett
        /// 2 für kursiv
        /// 3 für unterstrichen
        /// Wird für Texte benutzt
        /// </summary>
        /// <param name="rPr"></param>
        /// <param name="style"></param>
        private void StyleCreator(RunProperties rPr, int style)
        {
            switch (style)
            {
                case 1:
                    rPr.Append(new Bold());
                    break;
                case 2:
                    rPr.Append(new Italic());
                    break;
                case 3:
                    rPr.Append(new Underline(){ Val = UnderlineValues.Single });
                    break;
            }
        }

        /// <summary>
        /// Farbenwert wird als String übergeben.
        /// Diese Methode verwandelt Stringwert in Hex Code
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private string TransformColor(string color)
        {
            System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(color);
            string colVal = col.ToArgb().ToString("X").Substring(2, 6);
            return colVal;
        }

        /// <summary>
        /// Setzt den Hintergrund in die gewünschte Farbe
        /// Wird für den Tabellenkopf benötigt
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private Shading TableBackground(string value)
        {
            return new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = TransformColor(value) };
        }
        /// <summary>
        /// Setzt die Ränder einer Tabelle auf einfache Linien
        /// </summary>
        /// <returns></returns>
        private TableCellBorders TableGrid()
        {
            return new TableCellBorders
           (
               new TopBorder() { Val = BorderValues.Single },
               new LeftBorder() { Val = BorderValues.Single },
               new RightBorder() { Val = BorderValues.Single },
               new BottomBorder() { Val = BorderValues.Single }
           );
        }
        /// <summary>
        /// Setzt den Absatz nach einem Text oder einer 
        /// Tabelle auf den gewünschten Wert
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private ParagraphProperties ParagraphSpacingAfter(int value)
        {
            string size = (20 * value).ToString();
             return new ParagraphProperties
            (
                new SpacingBetweenLines() { After = size }
            );
        }
        /// <summary>
        /// Fügt einen Seitenumbruch ein
        /// </summary>
        /// <returns></returns>
        private ParagraphProperties PBreak()
        {
            return new ParagraphProperties
            (
                new Break() { Type = BreakValues.Page }
            );
        }

        /// <summary>
        /// Definiert die Schrifart
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private RunFonts Font(string value)
        {
            return new RunFonts() { Ascii = value, HighAnsi = value, ComplexScript = value };
        }
        /// <summary>
        /// Definiert die Schriftgrösse
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private FontSize Size(int value)
        {
            string size = (2 * value).ToString(); ;
            return new FontSize() { Val = size };
        }
        /// <summary>
        /// Definiert die Schriftfarbe
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private Color FontColor(string value)
        {
            return new Color() { Val = value };
        }
        /// <summary>
        /// Fügt Text in Word ein
        /// Diese Methode wurde erstellt, damit nicht immer der ganze Pfad angegeben werden muss
        /// da sich Text aus ReportCompiler.Text und aus DocumentFormat.OpenXml.Wordprocessing.Text
        /// überschneiden
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private DocumentFormat.OpenXml.Wordprocessing.Text InsertText(string value)
        {
            DocumentFormat.OpenXml.Wordprocessing.Text text = new DocumentFormat.OpenXml.Wordprocessing.Text();
            text.Text = value;
            return text;
        }

        /// <summary>
        /// Wird bei einer Exception aufgerufen
        /// Word Dokument wird geschlossen und 
        /// begonnenes Dokument gelöscht
        /// </summary>
        public void Quit()
        {
            word.Close();
            File.Delete(filename);
        }
        /// <summary>
        /// Wird bei erfolgreicher Erstellung aufgerufen
        /// Änderungen werden gespeichert und Dokument wird geschlossen
        /// </summary>
        public void Exit()
        {
            word.MainDocumentPart.Document.Save();
            word.Close();
        }
    }
}