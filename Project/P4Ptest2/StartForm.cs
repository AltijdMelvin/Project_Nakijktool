using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P4Ptest2
{
    public partial class StartForm : Form
    {
        public event EventHandler NakijkKlik;

        public StartForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NakijkForm form = new NakijkForm();
            form.Show();
            this.Hide();
            form.Closed += (s, args) => this.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TentamensButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TentamensBox.Text = fbd.SelectedPath;
            }
        }

        private void NakijkButton_Click(object sender, EventArgs e)
        {
            if (NakijkKlik != null)
                NakijkKlik(sender, e);

            NakijkForm nakijk = new NakijkForm();
            nakijk.Show();
            this.Hide();
        }

        private void AntwoordenmodelButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                AntwoordenmodelBox.Text = fd.FileName;
            }
        }

        private void ExporteerButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ExporteerBox.Text = fbd.SelectedPath;
            }
        }

        private void GeschiedenisButton_Click(object sender, EventArgs e)
        {

        }
    }
}
