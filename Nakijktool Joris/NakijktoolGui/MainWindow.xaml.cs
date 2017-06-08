using System;
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
    public partial class MainWindow : Window
    {
        private List<string> filePaths;
        public MainWindow()
        {
            InitializeComponent();

            //filePaths = Directory.GetFiles(
            //    @"C:\Users\Joris Lops\Desktop\gradebook_TECH_E_16_185_Tentamen20Programmeren202_2016-12-15-12-39-42",
            //    searchPattern: "*.cs");

            filePaths = new List<string>()
            {
                @"C:\Dev\Werk\Programmeren\Programmeren2Tests2\Tentamens\TentamenPrg2Tentamen20162017.cs"
            };
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Program p = new Program();
            TestRapport r = p.GetTestRapport(filePaths[0]);

            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AntwoordenModelButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                AntwoordenModelBox.Text = fd.FileName;
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TentamenButton_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TentamenBox.Text = fbd.SelectedPath;
            }
        }

        private void ExportButton_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ExportBox.Text = fbd.SelectedPath;
            }
        }

        private void NakijkButton_Click(object sender, RoutedEventArgs e)
        {
            Nakijkform nakijk = new Nakijkform();
            this.Hide();
            nakijk.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            nakijk.Show();
        }
    }
}
