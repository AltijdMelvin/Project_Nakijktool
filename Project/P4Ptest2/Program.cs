using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace P4Ptest2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        //static TextBox tentamens = Application.OpenForms["StartForm"].Controls["TentamensBox"] as TextBox;
        //private static string directoryExamResults = tentamens.Text;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartForm());
        }
        
    }
}
