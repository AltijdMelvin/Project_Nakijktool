﻿using System;
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
    /// Interaction logic for Nakijkform.xaml
    /// </summary>
    public partial class Nakijkform : Window
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

        public Nakijkform()
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
                tof = tof + "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";

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
            DataSet tentamen;
            connectionstring = ConfigurationManager.ConnectionStrings["NakijkTool.Properties.Settings.Database_NakijktoolConnectionString"].ConnectionString;

            string query = "SELECT * FROM Testrapport WHERE tentamenid = "+ TentamenIdBox.Text + " AND vraagnummer = " + vraagnummer;
            string aantalquery = "SELECT aantal_vragen FROM Tentamens WHERE tentamenid = " + TentamenIdBox.Text;
            string tentamenquery = "SELECT * FROM Tentamens WHERE tentamenid = " + TentamenIdBox.Text;
            using (connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(aantalquery, connection))
                {
                    connection.Open();
                    aantalvragen = Convert.ToInt32(command.ExecuteScalar());
                }
                using (SqlDataAdapter command = new SqlDataAdapter(tentamenquery, connection))
                {
                    tentamen = new DataSet();
                    command.Fill(tentamen);
                    v = Convert.ToInt32(tentamen.Tables[0].Rows[0]["v_tentamen"]);
                    q = Convert.ToInt32(tentamen.Tables[0].Rows[0]["q_vraag"]);
                }
                using (SqlDataAdapter command = new SqlDataAdapter(query, connection))
                {
                    int vraag = v + 1;
                    rapporten = new DataSet();
                    command.Fill(rapporten);
                    StudentCodeBox.Text = rapporten.Tables[0].Rows[v]["studentcode"].ToString();
                    FoutmeldingBox.Text = rapporten.Tables[0].Rows[v]["errors"].ToString();
                    VraagIdBox.Text = rapporten.Tables[0].Rows[v]["vraagid"].ToString();
                    PuntenLabel.Content = rapporten.Tables[0].Rows[v]["studentpunten"].ToString();
                    CommentaarTextBox.Text = rapporten.Tables[0].Rows[v]["commentaartext"].ToString();
                    RapportIdBox.Text = rapporten.Tables[0].Rows[v]["rapportid"].ToString();
                    InfoLabel.Content = "Opdracht " + q + "/" + aantalvragen + " (" + vraag + "/" + rapporten.Tables[0].Rows.Count + ")";
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

            string queryvraag = "UPDATE Testrapport SET commentaartext = '" + CommentaarTextBox.Text + "', commentaar = '" + binairtf + "', studentpunten = '" + studentpunten + "'  WHERE rapportid = " + RapportIdBox.Text;
            string querytentamen = "UPDATE Tentamens SET q_vraag = '" + q + "', v_tentamen = '" + v + "' WHERE tentamenid = " + TentamenIdBox.Text;
            using (connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(queryvraag, connection))
                {
                    connection.Open();
                    command.ExecuteScalar();
                }
                using (SqlCommand command = new SqlCommand(querytentamen, connection))
                {
                    command.ExecuteScalar();
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            DataOpslaan();
            data(q);
            CheckedListBox(Convert.ToInt32(VraagIdBox.Text));
            PuntenData();
            if (studentpunten > aantalpunten )
            {
                System.Windows.MessageBox.Show("Teveel punten toegekend. Niet mogelijk.", "Oeps", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                foreach (BoolStringClass item in TheList)
                {
                    item.IsSelected = false;
                }
                DataOpslaan();
                data(q);
                CheckedListBox(Convert.ToInt32(VraagIdBox.Text));
                PuntenData();
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            DataOpslaan();
            data(q);
            CheckedListBox(Convert.ToInt32(VraagIdBox.Text));
            PuntenData();
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

        private void Vorige_Tentamen_Click(object sender, RoutedEventArgs e)
        {
            v--;
            if (v >= 0)
            {
                DataOpslaan();
                data(q);
                CheckedListBox(Convert.ToInt32(VraagIdBox.Text));
                PuntenData();
            }
            else v++;
        }

        private void Volgende_Tentamen_Click(object sender, RoutedEventArgs e)
        {
            v++;
            if (v <= rapporten.Tables[0].Rows.Count - 1)
            {
                DataOpslaan();
                data(q);
                CheckedListBox(Convert.ToInt32(VraagIdBox.Text));
                PuntenData();
            }
            else v--;
        }

        private void duplicateButton_Click(object sender, RoutedEventArgs e)
        {
            CDuplicerenForm dup = new CDuplicerenForm();
            dup.KopieerCommentaar(Convert.ToInt16(VraagIdBox.Text));
            dup.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dup.Show();
        }
    }
}

public class BoolStringClass
{
    public string TheText { get; set; }
    public bool IsSelected { get; set; }
}