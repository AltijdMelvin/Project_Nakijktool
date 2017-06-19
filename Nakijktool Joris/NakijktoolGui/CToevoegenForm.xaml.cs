using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NakijktoolGui;

namespace NakijktoolGui
{
    /// <summary>
    /// Interaction logic for CToevoegenForm.xaml
    /// </summary>
    public partial class CToevoegenForm : Window
    {
        private static string connectionstring;
        static SqlConnection connection;

        public CToevoegenForm()
        {
            InitializeComponent();
        }

        private void ToevoegButton_Click(object sender, RoutedEventArgs e)
        {
            connectionstring = ConfigurationManager.ConnectionStrings["NakijkTool.Properties.Settings.Database_NakijktoolConnectionString"].ConnectionString;

            string querytentamen = "INSERT INTO Commentaar VALUES (@commentaar, @minpunten, @vraagid, @commentaarnaam)";
            using (connection = new SqlConnection(connectionstring))
            using (SqlCommand command = new SqlCommand(querytentamen, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@commentaar", CommentaarBox.Text);
                command.Parameters.AddWithValue("@minpunten", Convert.ToInt32(PuntenAftrekBox.Text));
                command.Parameters.AddWithValue("@vraagid", Convert.ToInt32(VraagidBox.Text));
                command.Parameters.AddWithValue("@commentaarnaam", NaamBox.Text);
                command.ExecuteScalar();

            }

            this.Hide();

        }

        private void AnnuleerButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
