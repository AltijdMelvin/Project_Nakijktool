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
        private int vraagid = -1;
        string connectionstring = ConfigurationManager.ConnectionStrings["NakijkTool.Properties.Settings.Database_NakijktoolConnectionString"].ConnectionString;
        private DataSet commentaar = new DataSet();
        private DataSet tentamens = new DataSet();
        private DataSet vragen = new DataSet();
        string queryinsert = "INSERT INTO Commentaar (commentaar, pluspunten, vraagid, commentaarnaam) VALUES (@commentaar, @pluspunten, @vraagid, @commentaarnaam)";
        string querycommentaar = "SELECT * FROM Commentaar WHERE vraagid = @vraagid";
        string querytentamen = "SELECT tentamenid, tentamen_naam, aantal_vragen FROM Tentamens";
        string queryvragendoel = "SELECT * FROM Vraag";
        string oldText = "";
        public void KopieerCommentaar(int vraagID)
        {
            vraagid = vraagID;
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
                        selectBox.Items.Add(new KeyValuePair<int, string>((int)commentaar.Tables[0].Rows[i]["commentaarid"], $"{commentaar.Tables[0].Rows[i]["commentaarnaam"].ToString()} ({commentaar.Tables[0].Rows[i]["pluspunten"].ToString()} punten)"));
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
                connection.Close();
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
                if (allQuestionsBox.IsChecked == false)
                {
                    if (selectBox.SelectedIndex >= 0 && questionBox.SelectedIndex >= 0 && vraagid >= 0)
                    {
                        using (SqlCommand command = new SqlCommand(queryinsert, connection))
                        {
                            DataRow current = commentaar.Tables[0].Rows[selectBox.SelectedIndex];
                            command.Parameters.AddWithValue("commentaar", commentBox.Text);
                            command.Parameters.AddWithValue("commentaarnaam", current["commentaarnaam"]);
                            command.Parameters.AddWithValue("pluspunten", current["pluspunten"]);
                            command.Parameters.AddWithValue("vraagid", questionBox.SelectedValue);
                            command.ExecuteScalar();
                        }
                    }
                }
                else if (allQuestionsBox.IsChecked == true)
                {
                    if (vraagid >= 0 && tentamenBox.SelectedIndex >= 0)
                    {
                        int tentamenIdA = (int)vragen.Tables[0].Select($"vraagid = {vraagid}")[0]["tentamenid"];
                        int tentamenIdB = (int)tentamenBox.SelectedValue;
                        int goal = 0;
                        int aantalVragenA = (int)vragen.Tables[0].Select($"tentamenid = {tentamenIdA}").Length;
                        int aantalVragenB = (int)vragen.Tables[0].Select($"tentamenid = {tentamenIdB}").Length;
                        if (aantalVragenA <= aantalVragenB) goal = aantalVragenA;
                        else goal = aantalVragenB;
                        DataRow[] vA = vragen.Tables[0].Select($"tentamenid = {tentamenIdA}");
                        DataRow[] vB = vragen.Tables[0].Select($"tentamenid = {tentamenIdB}");
                        for (int i = 0; i < goal; i++)
                        {
                            int vraagid = (int)vA[i]["vraagid"];
                            DataRow[] y = commentaar.Tables[0].Select($"vraagid = {vraagid}");
                            for (int j = 0; j < y.Length; j++)
                            {
                                using (SqlCommand command = new SqlCommand(queryinsert, connection))
                                {
                                    command.Parameters.AddWithValue("commentaar", y[j]["commentaar"]);
                                    command.Parameters.AddWithValue("commentaarnaam", y[j]["commentaarnaam"]);
                                    command.Parameters.AddWithValue("pluspunten", y[j]["pluspunten"]);
                                    command.Parameters.AddWithValue("vraagid", vB[i]["vraagid"]);
                                    command.ExecuteScalar();
                                }
                            }
                        }
                    }
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
        private void allQuestionsBox_Click(object sender, RoutedEventArgs e)
        {
            if (allQuestionsBox.IsChecked == true)
            {
                commentBox.IsEnabled = false;
                oldText = commentBox.Text;
                selectBox.IsEnabled = false;
                questionBox.IsEnabled = false;
                commentBox.Text = "Kopieër al het commentaar naar een tentamen (aantal vragen moeten hetzelfde of meer zijn!)";
            }
            else
            {
                commentBox.IsEnabled = true;
                selectBox.IsEnabled = true;
                questionBox.IsEnabled = true;
                commentBox.Text = oldText;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
