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
        string queryinsert = "INSERT INTO Commentaar (commentaar, pluspunten, vraagid, commentaarnaam) VALUES (@commentaar, @pluspunten, @vraagid, @commentaarnaam)";
        string querycommentaar = "SELECT * FROM Commentaar WHERE vraagid = @vraagid";
        string querytentamen = "SELECT tentamenid, tentamen_naam FROM Tentamens";
        string queryvragendoel = "SELECT * FROM Vraag";
        public void KopieerCommentaar(int vraagID)
        {
            string connectionstring;
            SqlConnection connection;
            commentaar = new DataSet();
            tentamens = new DataSet();
            vragen = new DataSet();
            connectionstring = ConfigurationManager.ConnectionStrings["NakijkTool.Properties.Settings.Database_NakijktoolConnectionString"].ConnectionString;
            using (connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                using (SqlDataAdapter c = new SqlDataAdapter(querycommentaar, connection))
                {
                    c.SelectCommand.Parameters.AddWithValue("vraagid", vraagID);
                    c.Fill(commentaar);
                    for (int i = 0; i < commentaar.Tables[0].Rows.Count; i++)
                    {
                        selectBox.Items.Add(new KeyValuePair<int, string>((int)commentaar.Tables[0].Rows[i]["commentaarid"], commentaar.Tables[0].Rows[i]["commentaarnaam"].ToString()));
                    }
                    selectBox.SelectedValuePath = "Key";
                    selectBox.DisplayMemberPath = "Value";
                }
                using (SqlDataAdapter t = new SqlDataAdapter(querytentamen, connection))
                {
                    t.Fill(tentamens);
                    for (int i = 0; i < tentamens.Tables[0].Rows.Count; i++)
                    {
                        tentamenBox.Items.Add(new KeyValuePair<int, string>((int)tentamens.Tables[0].Rows[i]["tentamenid"], tentamens.Tables[0].Rows[i]["tentamen_naam"].ToString()));
                    }
                    tentamenBox.SelectedValuePath = "Key";
                    tentamenBox.DisplayMemberPath = "Value";
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
                DataRow[] rows = vragen.Tables[0].Select($"tentamenid = {tentamenBox.SelectedValue}");
                for (int i = 0; i < rows.Length; i++)
                {
                    questionBox.Items.Add(new KeyValuePair<int, string>((int)rows[i]["vraagid"], $"Vraag {rows[i]["vraagnummer"]}"));
                    questionBox.SelectedValuePath = "Key";
                    questionBox.DisplayMemberPath = "Value";
                }
            }
        }

        private void copyButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(queryinsert, connection))
                {
                    DataRow current = commentaar.Tables[0].Rows[selectBox.SelectedIndex];
                    command.Parameters.AddWithValue("commentaar", current["commentaar"]);
                    command.Parameters.AddWithValue("commentaarnaam", current["commentaarnaam"]);
                    command.Parameters.AddWithValue("pluspunten", current["pluspunten"]);
                    command.Parameters.AddWithValue("vraagid", questionBox.SelectedValue);
                    command.ExecuteScalar();
                    this.Close();
                }
                connection.Close();
            }
        }

        private void selectBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectBox.SelectedIndex >= 0 && commentaar.Tables[0].Rows.Count > 0)
            {
                commentBox.Text = commentaar.Tables[0].Rows[selectBox.SelectedIndex]["commentaar"].ToString();
            }
        }
    }

}
