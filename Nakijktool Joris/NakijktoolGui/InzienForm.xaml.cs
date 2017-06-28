using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;

namespace NakijktoolGui
{
    /// <summary>
    /// Interaction logic for InzienForm.xaml
    /// </summary>
    public partial class InzienForm : Window
    {
        static string connectionstring;
        static SqlConnection connection;
        DataTable rapporten;

        public InzienForm()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() => { TestrapVullen(); }));
        }


        public void TestrapVullen()
        {
            connectionstring = ConfigurationManager.ConnectionStrings["NakijkTool.Properties.Settings.Database_NakijktoolConnectionString"].ConnectionString;

            string rapportquery = "SELECT DISTINCT studentnummer, student_naam, tentamenid FROM Testrapport WHERE tentamenid = " + TentamenidBox.Text + "ORDER BY studentnummer";
            using (connection = new SqlConnection(connectionstring))
            {
                using (SqlDataAdapter command = new SqlDataAdapter(rapportquery, connection))
                {
                    connection.Open();
                    rapporten = new DataTable();
                    command.Fill(rapporten);
                    TestrapData.ItemsSource = rapporten.DefaultView;
                    TestrapData.AutoGenerateColumns = false;
                    TestrapData.CanUserAddRows = false;
                }
            }
        }

        private void InzienButton_Click(object sender, RoutedEventArgs e)
        {
            var studentnummer = (((Button)sender).Tag.ToString());

            InzienStudentForm inst = new InzienStudentForm();
            this.Hide();
            inst.TentamenIdBox.Text = TentamenidBox.Text;
            inst.StudennummerBox.Text = studentnummer;
            inst.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            inst.Show();
        }

        private void TerugButton_Click(object sender, RoutedEventArgs e)
        {
            GeschiedenisForm gsf = new GeschiedenisForm();
            this.Hide();
            gsf.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            gsf.Show();
        }
    }
}
