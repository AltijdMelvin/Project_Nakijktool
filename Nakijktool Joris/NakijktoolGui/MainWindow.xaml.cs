﻿using System;
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
    public partial class MainWindow : Window
    {
        private List<string> filePaths;
        private string[] files;
        private string directoryExamResults;
        private string TestsFileSrc;
        private int nrOfQuestions;

        public MainWindow()
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
            files = Directory.GetFiles(
                directoryExamResults,
                searchPattern: "*.cs");

            Program p = new Program(TestsFileSrc);

            for (int i = 2; i <= nrOfQuestions; i++)
            {
                p.QuestionNumber = i;
                List<TestRapport> repports = new List<TestRapport>(); //maakt een lijst van testrapporten
                foreach (var stundentCsFilePath in files/*.Skip(16).Take(5)*/)
                {
                    Console.WriteLine($"Processing {stundentCsFilePath}");
                    TestRapport testRapport = p.GetTestRapport(stundentCsFilePath);
                    repports.Add(testRapport);
                }
                p.FileWriterReport(files, repports, i);
            }
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
            directoryExamResults = TentamenBox.Text;
            nakijk.CodeBox.Text = directoryExamResults;
            this.Hide();
            nakijk.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            nakijk.Show();
        }

    }
}
