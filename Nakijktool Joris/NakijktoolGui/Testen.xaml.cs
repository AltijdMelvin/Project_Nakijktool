using System;
using System.Collections.Generic;
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
    /// Interaction logic for Testen.xaml
    /// </summary>
    public partial class Testen : Window
    {
        public Testen()
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
    }
}
