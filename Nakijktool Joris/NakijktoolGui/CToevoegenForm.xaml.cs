using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;
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
    /// Interaction logic for CToevoegenForm.xaml
    /// </summary>
    public partial class CToevoegenForm : Window
    {
        private static string connectionstring;
        static SqlConnection connection;
        private Nakijkform mainForm = null;

        public CToevoegenForm(Window callingForm)
        {
            mainForm = callingForm as Nakijkform;
            InitializeComponent();
        }

        private void ToevoegButton_Click(object sender, RoutedEventArgs e)
        {
            connectionstring = ConfigurationManager.ConnectionStrings["NakijkTool.Properties.Settings.Database_NakijktoolConnectionString"].ConnectionString;
            int vraagnummer;
            string nummerquery = "SELECT vraagnummer FROM Vraag WHERE vraagid = " + VraagidBox.Text;
            string querytentamen = "INSERT INTO Commentaar VALUES (@commentaar, @pluspunten, @vraagid, @commentaarnaam)";
            using (connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(querytentamen, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@commentaar", CommentaarBox.Text);
                    command.Parameters.AddWithValue("@pluspunten", Convert.ToDecimal(PlusPuntenBox.Text));
                    command.Parameters.AddWithValue("@vraagid", Convert.ToInt32(VraagidBox.Text));
                    command.Parameters.AddWithValue("@commentaarnaam", NaamBox.Text);
                    command.ExecuteScalar();

                }
                using(SqlCommand command = new SqlCommand(nummerquery, connection))
                {
                    vraagnummer = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            this.mainForm.DataOpslaan();
            this.mainForm.CheckedListBox(Convert.ToInt32(VraagidBox.Text));

            this.Close();
        }

        private void AnnuleerButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
