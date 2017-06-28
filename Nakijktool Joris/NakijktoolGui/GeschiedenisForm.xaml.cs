using NakijkTool;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace NakijktoolGui
{
    /// <summary>
    /// Interaction logic for GeschiedenisForm.xaml
    /// </summary>
    public partial class GeschiedenisForm : Window
    {
        public GeschiedenisForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NakijkTool.Database_NakijktoolDataSet database_NakijktoolDataSet = ((NakijkTool.Database_NakijktoolDataSet)(this.FindResource("database_NakijktoolDataSet")));
            // Load data into the table Tentamens. You can modify this code as needed.
            NakijkTool.Database_NakijktoolDataSetTableAdapters.TentamensTableAdapter database_NakijktoolDataSetTentamensTableAdapter = new NakijkTool.Database_NakijktoolDataSetTableAdapters.TentamensTableAdapter();
            database_NakijktoolDataSetTentamensTableAdapter.Fill(database_NakijktoolDataSet.Tentamens);
            System.Windows.Data.CollectionViewSource tentamensViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tentamensViewSource")));
            tentamensViewSource.View.MoveCurrentToFirst();
        }

        private void tentamensDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NakijkButton_Click(object sender, RoutedEventArgs e)
        {
            var tentamenid = (((Button)sender).Tag.ToString());
            Nakijkform nakijk = new Nakijkform();
            this.Hide();
            nakijk.TentamenIdBox.Text = tentamenid;
            nakijk.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            nakijk.Show();
        }

        private void InzienButton_Click(object sender, RoutedEventArgs e)
        {
            var tentamenid = (((Button)sender).Tag.ToString());
            InzienForm inzien = new InzienForm();
            this.Hide();
            inzien.TentamenidBox.Text = tentamenid;
            inzien.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            inzien.Show();

        }

        private void ExclButton_Click(object sender, RoutedEventArgs e)
        {
            var tentamenid = (((Button)sender).Tag.ToString());
            ExcelWriter.CreateTestRapportFromSQL(Convert.ToInt16(tentamenid));
        }

        private void TerugButton_Click(object sender, RoutedEventArgs e)
        {
            MainForm main = new MainForm();
            this.Hide();
            main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            main.Show();
        }
    }
}
