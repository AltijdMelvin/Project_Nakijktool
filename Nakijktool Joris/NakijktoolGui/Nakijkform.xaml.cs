using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Windows.Threading;

namespace NakijktoolGui
{
    /// <summary>
    /// Interaction logic for Nakijkform.xaml
    /// </summary>
    public partial class Nakijkform : Window
    {
        static int i = 0;
        private static string connectionstring;
        static SqlConnection connection;
        DataSet rapporten;

        public Nakijkform()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() => { data(); }));
        }

        private void data()
        {
            connectionstring = ConfigurationManager.ConnectionStrings["NakijkTool.Properties.Settings.Database_NakijktoolConnectionString"].ConnectionString;

            string query = "SELECT * FROM Testrapport WHERE tentamenid = "+ TentamenIdBox.Text;
            using (connection = new SqlConnection(connectionstring))
            using (SqlDataAdapter command = new SqlDataAdapter(query, connection))
            {
                connection.Open();
                rapporten = new DataSet();
                command.Fill(rapporten);
                StudentCodeBox.Text = rapporten.Tables[0].Rows[i]["studentcode"].ToString();
                FoutmeldingBox.Text = rapporten.Tables[0].Rows[i]["errors"].ToString();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Vorige_Opdracht_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Volgende_Opdracht_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            GeschiedenisForm gform = new GeschiedenisForm();
            this.Hide();
            gform.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            gform.Show();
        }

        private void Vorige_Tentamen_Click(object sender, RoutedEventArgs e)
        {
            i--;
            if (i >= 0)
            {
                data();
            }
            else i++;
        }

        private void Volgende_Tentamen_Click(object sender, RoutedEventArgs e)
        {
            i++;
            if (i <= rapporten.Tables[0].Rows.Count - 1)
            {
                data();
            }
            else i--;
        }
    }
}
