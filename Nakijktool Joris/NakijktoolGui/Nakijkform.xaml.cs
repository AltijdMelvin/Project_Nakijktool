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
        static int v = 0;
        static int q = 2;
        static int aantalvragen;
        private static string connectionstring;
        static SqlConnection connection;
        DataSet rapporten;

        public Nakijkform()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() => { data(q); }));
        }

        private void data(int vraagnummer)
        {
            connectionstring = ConfigurationManager.ConnectionStrings["NakijkTool.Properties.Settings.Database_NakijktoolConnectionString"].ConnectionString;

            string query = "SELECT * FROM Testrapport WHERE tentamenid = "+ TentamenIdBox.Text + " AND vraagnummer = " + vraagnummer;
            string aantalquery = "SELECT aantal_vragen FROM Tentamens WHERE tentamenid = " + TentamenIdBox.Text;
            using (connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(aantalquery, connection))
                {
                    connection.Open();
                    aantalvragen = (int)command.ExecuteScalar();
                }
                using (SqlDataAdapter command = new SqlDataAdapter(query, connection))
                {
                    rapporten = new DataSet();
                    command.Fill(rapporten);
                    StudentCodeBox.Text = rapporten.Tables[0].Rows[v]["studentcode"].ToString();
                    FoutmeldingBox.Text = rapporten.Tables[0].Rows[v]["errors"].ToString();
                    VraagIdBox.Text = rapporten.Tables[0].Rows[v]["vraagnummer"].ToString();
                }
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Vorige_Opdracht_Click(object sender, RoutedEventArgs e)
        {
            q--;
            if (q >= 2)
            {
                data(q);
                v = 0;
            }
            else q++;
        }

        private void Volgende_Opdracht_Click(object sender, RoutedEventArgs e)
        {
            q++;
            if (q <= aantalvragen)
            {
                data(q);
                v = 0;
            }
            else q--;
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
            v--;
            if (v >= 0)
            {
                data(q);
            }
            else v++;
        }

        private void Volgende_Tentamen_Click(object sender, RoutedEventArgs e)
        {
            v++;
            if (v <= rapporten.Tables[0].Rows.Count - 1)
            {
                data(q);
            }
            else v--;
        }
    }
}
