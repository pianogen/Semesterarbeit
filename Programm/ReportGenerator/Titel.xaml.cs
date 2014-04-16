using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReportGenerator
{
    /// <summary>
    /// Interaction logic for Titel.xaml
    /// </summary>
    public partial class Titel : Window
    {
        /// <summary>
        /// /// Initalisiert die WPF Komponente
        /// Ruft folgende Methoden auf
        /// ChooseFont()
        /// ChooseFontStyle()
        /// ChooseColor()
        /// </summary>
        public Titel()
        {
            InitializeComponent();
            ChooseFont();
            ChooseFontStyle();
            ChooseColor();
        }
        /// <summary>
        /// Property für die Texteingabe,
        /// Die Eingabe wird maskiert um die Xml Syntax nicht zu verletzen
        /// </summary>
        public string ClearType
        {
            get { return NameChooser.Text; }
        }
        /// <summary>
        /// Property für den Schriftart des Texts
        /// </summary>
        public string Font
        {
            get { return FontChooser.Text; }
        }
        /// <summary>
        /// Property für den Schriftstil des Texts
        /// </summary>
        public string Size
        {
            get { return SizeChooser.Text; }
        }
        /// <summary>
        /// Property für den Schriftstil des Texts
        /// </summary>
        public int StyleConverter
        {
            get { return ConverterStyleToInt(FontStyleChooser.Text); }
        }
        /// <summary>
        /// Wandelt für den späteren Gebrauch die Auswahl des Schriftstil, welches als Text weitergegeben wird, in Zahlen um
        /// 0 Steht für normal
        /// 1 Steht für dick
        /// 2 Steht für kursiv
        /// 3 Steht für unterstrichen
        /// </summary>
        /// <param name="value">Als Parameter wird der ausgewählte Schriftstil mitgegeben</param>
        /// <returns>Gibt die konvertierte Zahl zurück</returns>
        public int ConverterStyleToInt(string value)
        {
            switch (value)
            {
                case "Bold":
                    return 1;
                case "Italic":
                    return 2;
                case "Underlined":
                    return 3;
            }
            return 0;
        }
        /// <summary>
        /// Property für die Schriftfarbe des Texts
        /// </summary>
        public string Color
        {
            get { return FontColorChooser.Text; }
        }
        /// <summary>
        /// Property für den Absatz nach dem Text
        /// </summary>
        public string Paragraph
        {
            get { return ParagraphAfter.Text; }
        }
        /// <summary>
        /// Property für einen möglichen Seitenumbruch
        /// </summary>
        public bool PageBreak
        {
            get { return Convert.ToBoolean(PageBreakSetter.IsChecked); }
        }
        /// <summary>
        /// Ruft alle möglichen Schriftarten von Windows auf und speichert si in eine ComboBox
        /// Standardwert ist Arial
        /// </summary>
        public void ChooseFont()
        {
            foreach (System.Windows.Media.FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                FontChooser.Items.Add(fontFamily.Source);
            }
            FontChooser.SelectedValue = "Arial";
        }
        /// <summary>
        /// Generiert eine Liste mit den vier möglichen Schriftstils
        /// Standardwert ist Normal
        /// </summary>
        private void ChooseFontStyle()
        {
            FontStyleChooser.Items.Add("Normal");
            FontStyleChooser.Items.Add("Bold");
            FontStyleChooser.Items.Add("Italic");
            FontStyleChooser.Items.Add("Underlined");
            FontStyleChooser.SelectedValue = "Normal";
        }
        /// <summary>
        /// Ruft alle möglichen Farben in Windows auf und speichert sie in eine Liste
        /// Standardwert ist schwarz
        /// </summary>
        private void ChooseColor()
        {
            List<string> colors = new List<string>();
            colors.AddRange(System.Enum.GetNames(typeof(KnownColor)));
            colors.Sort();
            foreach (string color in colors)
            {
                FontColorChooser.Items.Add(color);
            }
            FontColorChooser.SelectedValue = "Black";
        }
        /// <summary>
        /// Button Handler um die Eingaben zu bestätigen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EmptyTextBox();
                this.DialogResult = true;
                this.Close();
            }
            catch (EmptyTextBoxException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        /// <summary>
        /// Button Handler um die Eingabe zurückzusetzen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Überprüft ob alle Textfelder etwas beinhalten
        /// </summary>
        private void EmptyTextBox()
        {
            if (ClearType == "" || Size == "" || Paragraph == "")
                throw new EmptyTextBoxException("Es darf kein Feld leer sein");
        }
        /// <summary>
        /// Lässt bei den Textfelder 'Size' und 'Paragraph' nur numerische Werte zu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void int_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
    }
}
