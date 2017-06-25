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
        List<string> tVragen = new List<string>();

        public MainForm()
        {
            InitializeComponent();

            filePaths = new List<string>()
            {
                @"C:\Users\Emiell\Documents\GitHub\Project_Nakijktool\Anonieme tentamens\prg3Anoniem\Tentamen Programmeren 3_ALYS6101_attempt_2017-03-17-12-57-02_TentamenPrg3_1_2016_2017.cs"
            };
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            TestsFileSrc = @AntwoordenModelBox.Text;
            nrOfQuestions = Convert.ToInt32(NrOfQuestionsBox.Text);
            directoryExamResults = TentamenBox.Text;

            Program p = new Program(TestsFileSrc);

            files = Directory.GetFiles(
                directoryExamResults,
                searchPattern: "*.cs");

            foreach(string path in files)
            {
                string tVraag = File.ReadAllText(path);
                string tVraagAntwoord = tVraag.Substring(tVraag.IndexOf("Vraag1"), tVraag.IndexOf("Vraag2") - tVraag.IndexOf("Vraag1"));
                tVragen.Add(tVraagAntwoord);
            }
           
            p.FileWriterReport(files, TentamenNaam.Text, TentamenDatum.SelectedDate.Value, TestsFileSrc, nrOfQuestions);
        }

        private void AntwoordenModelButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                AntwoordenModelBox.Text = fd.FileName;
            }
        }

        private void TentamenButton_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TentamenBox.Text = fbd.SelectedPath;
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
