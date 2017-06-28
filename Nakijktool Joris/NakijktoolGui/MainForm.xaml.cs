using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using NakijkTool;

namespace NakijktoolGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainForm: Window
    {
        private List<string> filePaths;
        private string[] files;
        private string directoryExamResults;
        private string TestsFileSrc;
        private int nrOfQuestions;
        private bool selectPath;
        private bool selectModel;
        private static Random rng = new Random();
        List<string> tVragen = new List<string>();

        public MainForm()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public void Shuffle<T>(Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (TentamenDatum.SelectedDate == null)
            {
                System.Windows.MessageBox.Show("Er is geen datum opgegeven", "Oeps", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (!selectModel || !selectPath)
            {
                System.Windows.MessageBox.Show("Niet alle bestandspaden zijn opgegeven", "Oeps", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (NrOfQuestionsBox.Text == "")
            {
                System.Windows.MessageBox.Show("Het aantal vragen is niet opgegeven", "Oeps", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else {
                TestsFileSrc = @AntwoordenModelBox.Text;
                nrOfQuestions = Convert.ToInt32(NrOfQuestionsBox.Text);
                directoryExamResults = TentamenBox.Text;

                Program p = new Program(TestsFileSrc);

                files = Directory.GetFiles(
                    directoryExamResults,
                    searchPattern: "*.cs");

                Shuffle(new Random(), files);

                foreach (string path in files)
                {
                    string tVraag = File.ReadAllText(path);
                    string tVraagAntwoord = tVraag.Substring(tVraag.IndexOf("Vraag1"), tVraag.IndexOf("Vraag2") - tVraag.IndexOf("Vraag1"));
                    tVragen.Add(tVraagAntwoord);
                }

                p.FileWriterReport(files, TentamenNaam.Text, TentamenDatum.SelectedDate.Value, TestsFileSrc, nrOfQuestions);
            }
        }

        private void AntwoordenModelButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                AntwoordenModelBox.Text = fd.FileName;
                selectModel = true;
            }
        }

        private void TentamenButton_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TentamenBox.Text = fbd.SelectedPath;
                selectPath = true;
            }
        }

        private void NakijkButton_Click(object sender, RoutedEventArgs e)
        {
            GeschiedenisForm gform = new GeschiedenisForm();
            this.Hide();
            gform.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            gform.Show();
        }
    }
}
