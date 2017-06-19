using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        DataSet commentaar;
        public ObservableCollection<BoolStringClass> TheList { get; set; }

        public Nakijkform()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() => { data(q); }));
        }

        public void CheckedListBox(int vraagid)
        {
            TheList = new ObservableCollection<BoolStringClass>();

            connectionstring = ConfigurationManager.ConnectionStrings["NakijkTool.Properties.Settings.Database_NakijktoolConnectionString"].ConnectionString;

            string query = "SELECT * FROM Commentaar WHERE vraagid = " + vraagid;
            using (connection = new SqlConnection(connectionstring))
            {
                using (SqlDataAdapter command = new SqlDataAdapter(query, connection))
                {
                    commentaar = new DataSet();
                    command.Fill(commentaar);
                }
            }
            for (int i = 0; i < commentaar.Tables[0].Rows.Count; i++)
            {
                TheList.Add(new BoolStringClass { IsSelected = false, TheText = commentaar.Tables[0].Rows[i]["commentaarnaam"].ToString() });
            }

            DataContext = this;
        }

        private void data(int vraagnummer)
        {
            connectionstring = ConfigurationManager.ConnectionStrings["NakijkTool.Properties.Settings.Database_NakijktoolConnectionString"].ConnectionString;
            int vraag = v + 1;
            string query = "SELECT * FROM Testrapport WHERE tentamenid = "+ TentamenIdBox.Text + " AND vraagnummer = " + vraagnummer;
            string aantalquery = "SELECT aantal_vragen FROM Tentamens WHERE tentamenid = " + TentamenIdBox.Text;
            using (connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(aantalquery, connection))
                {
                    connection.Open();
                    aantalvragen = Convert.ToInt32(command.ExecuteScalar());
                }
                using (SqlDataAdapter command = new SqlDataAdapter(query, connection))
                {
                    rapporten = new DataSet();
                    command.Fill(rapporten);
                    StudentCodeBox.Text = rapporten.Tables[0].Rows[v]["studentcode"].ToString();
                    FoutmeldingBox.Text = rapporten.Tables[0].Rows[v]["errors"].ToString();
                    VraagIdBox.Text = rapporten.Tables[0].Rows[v]["vraagid"].ToString();
                    InfoLabel.Content = "Opdracht " + vraagnummer + "/" + aantalvragen + " (" + Convert.ToInt32(vraag) + "/" + rapporten.Tables[0].Rows.Count + ")";
                }
            }

            CheckedListBox(Convert.ToInt32(rapporten.Tables[0].Rows[v]["vraagid"].ToString()));
        }

        private void Commentaar_Click(object sender, RoutedEventArgs e)
        {
            CToevoegenForm ctv = new CToevoegenForm();
            ctv.VraagidBox.Text = rapporten.Tables[0].Rows[v]["vraagid"].ToString();
            ctv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ctv.Show();
        }

        private void Vorige_Opdracht_Click(object sender, RoutedEventArgs e)
        {
            q--;
            if (q >= 2)
            {
                v = 0;
                data(q);
            }
            else q++;
        }

        private void Volgende_Opdracht_Click(object sender, RoutedEventArgs e)
        {
            q++;
            if (q <= aantalvragen)
            {
                v = 0;
                data(q);
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

public class BoolStringClass
{
    public string TheText { get; set; }
    public bool IsSelected { get; set; }
}