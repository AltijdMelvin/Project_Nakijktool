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
using NakijkTool;

namespace NakijktoolGui
{
    /// <summary>
    /// Interaction logic for InzienStudentForm.xaml
    /// </summary>
    public partial class InzienStudentForm : Window
    {
        static int v = 0;
        static int q = 1;
        static int aantalvragen;
        static int aantalpunten;
        static int studentpunten;
        private static string connectionstring;
        static SqlConnection connection;
        DataSet rapporten;
        DataSet commentaar;
        public ObservableCollection<BoolStringClass> TheList { get; set; }

        public InzienStudentForm()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() => { data(q); CheckedListBox(Convert.ToInt32(VraagIdBox.Text)); PuntenData(); }));
        }

        public void CheckedListBox(int vraagid)
        {
            studentpunten = 0;
            List<BoolStringClass> leeg = new List<BoolStringClass>() { };
            ListCheckBox.ItemsSource = leeg;

            TheList = new ObservableCollection<BoolStringClass>();

            connectionstring = ConfigurationManager.ConnectionStrings["NakijkTool.Properties.Settings.Database_NakijktoolConnectionString"].ConnectionString;

            string tof;
            string commentaarquery = "SELECT commentaar FROM Testrapport WHERE rapportid = " + RapportIdBox.Text;
            string query = "SELECT * FROM Commentaar WHERE vraagid = " + vraagid;
            using (connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(commentaarquery, connection))
                {
                    connection.Open();
                    tof = command.ExecuteScalar().ToString();
                }
                using (SqlDataAdapter command = new SqlDataAdapter(query, connection))
                {
                    commentaar = new DataSet();
                    command.Fill(commentaar);
                }
                tof = tof + "00000000000000000000000000000000000000000000000000000000000000000000000";

                for (int i = 0; i < commentaar.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToInt32(tof[i]) - 48 == 1)
                    {
                        TheList.Add(new BoolStringClass { IsSelected = true, TheText = commentaar.Tables[0].Rows[i]["commentaarnaam"].ToString() });
                        studentpunten = studentpunten + Convert.ToInt32(commentaar.Tables[0].Rows[i]["pluspunten"].ToString());
                    }
                    else TheList.Add(new BoolStringClass { IsSelected = false, TheText = commentaar.Tables[0].Rows[i]["commentaarnaam"].ToString() });
                }
            }
            
            ListCheckBox.ItemsSource = TheList;
        }

        public void data(int vraagnummer)
        {
            connectionstring = ConfigurationManager.ConnectionStrings["NakijkTool.Properties.Settings.Database_NakijktoolConnectionString"].ConnectionString;
            int vraag = v + 1;
            string query = "SELECT * FROM Testrapport WHERE tentamenid = " + TentamenIdBox.Text + " AND vraagnummer = " + vraagnummer + " AND studentnummer = '" + StudennummerBox.Text + "'";
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
                    StudentCodeBox.Text = rapporten.Tables[0].Rows[0]["studentcode"].ToString();
                    FoutmeldingBox.Text = rapporten.Tables[0].Rows[0]["errors"].ToString();
                    VraagIdBox.Text = rapporten.Tables[0].Rows[0]["vraagid"].ToString();
                    PuntenLabel.Content = rapporten.Tables[0].Rows[0]["studentpunten"].ToString();
                    CommentaarTextBox.Text = rapporten.Tables[0].Rows[0]["commentaartext"].ToString();
                    RapportIdBox.Text = rapporten.Tables[0].Rows[0]["rapportid"].ToString();
                    StudentNaamLabel.Content = "Studentnummer: " + rapporten.Tables[0].Rows[0]["studentnummer"].ToString();
                    Studentnaam.Content = "Studentnaam: " + rapporten.Tables[0].Rows[0]["student_naam"].ToString();
                    InfoLabel.Content = "Opdracht " + vraagnummer + "/" + aantalvragen;
                }
            }
        }

        private void PuntenData()
        {
            string puntenquery = "SELECT vraagpunten FROM Vraag WHERE vraagid = " + VraagIdBox.Text;
            using (connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(puntenquery, connection))
                {
                    connection.Open();
                    aantalpunten = Convert.ToInt32(command.ExecuteScalar());
                    PuntenLabel.Content = studentpunten + " van de " + aantalpunten + " punten.";
                }
            }
        }

        public void DataOpslaan()
        {
            string binairtf = string.Empty;

            foreach (BoolStringClass item in TheList)
            {
                if (item.IsSelected == true) binairtf += 1;
                else binairtf += 0;
            }

            string queryvraag = "UPDATE Testrapport SET commentaartext = '" + CommentaarTextBox.Text + "', commentaar = '" + binairtf + "', studentpunten = '" + studentpunten + "'  WHERE rapportid = " + RapportIdBox.Text + " AND studentnummer = '" + StudennummerBox.Text + "'";
            using (connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(queryvraag, connection))
                {
                    connection.Open();
                    command.ExecuteScalar();
                }
            }
        }

        private void Commentaar_Click(object sender, RoutedEventArgs e)
        {
            CToevoegenForm ctv = new CToevoegenForm(this);
            ctv.VraagidBox.Text = rapporten.Tables[0].Rows[v]["vraagid"].ToString();
            ctv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ctv.Show();
        }

        private void Vorige_Opdracht_Click(object sender, RoutedEventArgs e)
        {
            q--;
            if (q >= 1)
            {
                v = 0;
                DataOpslaan();
                data(q);
                CheckedListBox(Convert.ToInt32(VraagIdBox.Text));
                PuntenData();
            }
            else q++;
        }

        private void Volgende_Opdracht_Click(object sender, RoutedEventArgs e)
        {
            q++;
            if (q <= aantalvragen)
            {
                v = 0;
                DataOpslaan();
                data(q);
                CheckedListBox(Convert.ToInt32(VraagIdBox.Text));
                PuntenData();
            }
            else q--;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            DataOpslaan();
            GeschiedenisForm gform = new GeschiedenisForm();
            this.Close();
            gform.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            gform.Show();
        }

        private void duplicateButton_Click(object sender, RoutedEventArgs e)
        {
            CDuplicerenForm dup = new CDuplicerenForm();
            dup.KopieerCommentaar(Convert.ToInt16(VraagIdBox.Text));
            dup.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dup.Show();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            DataOpslaan();
            data(q);
            CheckedListBox(Convert.ToInt32(VraagIdBox.Text));
            PuntenData();
        }
    }
}