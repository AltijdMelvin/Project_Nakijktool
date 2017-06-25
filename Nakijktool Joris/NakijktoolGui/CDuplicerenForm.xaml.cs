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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace NakijktoolGui
{
    /// <summary>
    /// Interaction logic for CDuplicerenForm.xaml
    /// </summary>
    public partial class CDuplicerenForm : Window
    {
        public CDuplicerenForm()
        {
            InitializeComponent();
        }
        string connectionstring = ConfigurationManager.ConnectionStrings["NakijkTool.Properties.Settings.Database_NakijktoolConnectionString"].ConnectionString;
        private DataSet commentaar = new DataSet();
        private DataSet tentamens = new DataSet();
        private DataSet vragen = new DataSet();
        public void KopieerCommentaar(int vraagID)
        {
            string connectionstring;
            SqlConnection connection;
            commentaar = new DataSet();
            tentamens = new DataSet();
            vragen = new DataSet();
            string querycommentaar = "SELECT * FROM Commentaar WHERE vraagid = @vraagid";
            string querytentamen = "SELECT tentamenid, tentamen_naam FROM Tentamens";
            string queryvragendoel = "SELECT * FROM Vraag";
            connectionstring = ConfigurationManager.ConnectionStrings["NakijkTool.Properties.Settings.Database_NakijktoolConnectionString"].ConnectionString;
            using (connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                using (SqlDataAdapter c = new SqlDataAdapter(querycommentaar, connection))
                {
                    c.SelectCommand.Parameters.AddWithValue("vraagid", vraagID);
                    c.Fill(commentaar);
                    questionBox.DataContext = commentaar.Tables[0];
                    questionBox.SelectedValuePath = "commentaarid";
                    questionBox.DisplayMemberPath = "commentaarnaam";
                }
                using (SqlDataAdapter t = new SqlDataAdapter(querytentamen, connection))
                {
                    t.Fill(tentamens);
                    
                }
                using (SqlDataAdapter v = new SqlDataAdapter(queryvragendoel, connection))
                {
                    v.Fill(vragen);
                }
            }
        }

        private void tentamenBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tentamenBox.SelectedIndex >= 0)
            {
                questionBox.DataContext = vragen.Tables[0].Select($"tentamenid = {(DataTable)tentamenBox.SelectedItem}");
            }
        }

        private void copyButton_Click(object sender, RoutedEventArgs e)
        {
            ;
        }
    }
}
