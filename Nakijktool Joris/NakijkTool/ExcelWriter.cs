using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace NakijkTool
{
    public class ExcelWriter
    {
        public static void CreateTestRapportFromSQL(int tentamenID)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["NakijkTool.Properties.Settings.Database_NakijktoolConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                DataSet test = new DataSet();
                DataSet vragen = new DataSet();
                DataSet students = new DataSet();
                DataSet commentaar = new DataSet();
                DataSet opdr = new DataSet();
                decimal totaalpunten = 0;
                int totaalvragen = 0;
                int totaalstudenten = 0;
                string queryTest = $"SELECT * FROM Tentamens WHERE tentamenid = {tentamenID}";
                string queryStudents = $"SELECT distinct studentnummer, student_naam FROM Testrapport WHERE tentamenid = {tentamenID}";
                string queryOpdrachten = "SELECT * FROM Testrapport WHERE tentamenid = @tentamenid";
                string queryVragen = $"SELECT * FROM Vraag WHERE tentamenid = {tentamenID} ORDER BY vraagnummer ASC";
                string queryCommentaar = "SELECT * FROM Commentaar WHERE vraagid in (SELECT DISTINCT vraagid FROM Testrapport WHERE tentamenid = @tentamenid)";
                using (SqlDataAdapter command = new SqlDataAdapter(queryTest, connection))
                {
                    command.Fill(test);
                    totaalpunten = 10.0m + Convert.ToDecimal(test.Tables[0].Rows[0]["aantal_punten"]);
                }
                using (SqlDataAdapter command = new SqlDataAdapter(queryVragen, connection))
                {
                    command.Fill(vragen);
                    totaalvragen = vragen.Tables[0].Select($"tentamenid = {tentamenID}").Count();
                }
                using (SqlDataAdapter command = new SqlDataAdapter(queryStudents, connection))
                {
                    command.Fill(students);
                    totaalstudenten = students.Tables[0].Rows.Count;
                }
                using (SqlDataAdapter command = new SqlDataAdapter(queryCommentaar, connection))
                {
                    command.SelectCommand.Parameters.AddWithValue("tentamenid", tentamenID);
                    command.Fill(commentaar);
                }
                using (SqlDataAdapter command = new SqlDataAdapter(queryOpdrachten, connection))
                {
                    command.SelectCommand.Parameters.AddWithValue("tentamenid", tentamenID);
                    command.Fill(opdr);
                }
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                object misvalue = System.Reflection.Missing.Value;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(""));
                oSheet = oWB.Worksheets[1];
                oSheet.Name = "Toetsresultaten";

                for (int o = 0; o < totaalvragen; o++)
                {
                    // Opdrachtensheets
                    Microsoft.Office.Interop.Excel.Worksheet newSheet;
                    newSheet = oWB.Worksheets.Add(After: oWB.Sheets[oWB.Sheets.Count]);
                    newSheet.Name = $"Opdracht {o + 1}";
                    newSheet.Cells[1, "A"] = "Studentnaam";
                    newSheet.Cells[1, "B"] = "Studentnummer";
                    newSheet.Cells[1, "C"] = "Resultaat";
                    newSheet.Cells[1, "D"] = "Punten";
                    newSheet.Cells[1, "E"] = "Commentaar";
                    newSheet.Range["A1"].EntireRow.Font.Bold = true;
                    newSheet.Range["A1"].EntireRow.EntireColumn.AutoFit();
                }

                // Resultatensheet
                oSheet.Cells[1, "A"] = "Toetsnaam";
                oSheet.Cells[1, "B"] = "Datum";
                oSheet.Cells[2, "A"] = test.Tables[0].Rows[0]["tentamen_naam"].ToString();
                oSheet.Cells[2, "B"] = test.Tables[0].Rows[0]["datum"].ToString();
                oSheet.Cells[4, "A"] = "Naam";
                oSheet.Cells[4, "B"] = "Studentnummer";
                oSheet.Cells[4, "C"] = "Cijfer";
                oSheet.Cells[4, "D"] = "Testresultaat";

                oSheet.Range["A1"].EntireRow.Font.Bold = true;
                oSheet.Range["A4"].EntireRow.Font.Bold = true;
                oSheet.Range["A1", "A4"].EntireRow.EntireColumn.AutoFit();

                for (int s = 0; s < totaalstudenten; s++)
                {
                    int c = s + 2;
                    int m = 3;
                    int punten = 0;
                    int ec = 0; // compile error
                    int er = 0; // execution error
                    int ac = 0; // correct
                    bool unknown = false;
                    string studentnr = students.Tables[0].Rows[s]["studentnummer"].ToString();
                    string studentnaam = students.Tables[0].Rows[s]["student_naam"].ToString();
                    oSheet.Cells[c + m, "A"] = studentnaam;
                    oSheet.Cells[c + m, "B"] = studentnr;
                    // Opdrachten
                    DataRow[] o = opdr.Tables[0].Select($"studentnummer = '{studentnr}'", "studentnummer ASC");
                    for (int v = 0; v < totaalvragen; v++)
                    {
                        punten =+ Convert.ToInt16(o[v]["studentpunten"]);
                        Microsoft.Office.Interop.Excel.Worksheet newSheet;
                        newSheet = oWB.Worksheets[v + 2];
                        newSheet.Cells[c, "A"] = studentnaam;
                        newSheet.Cells[c, "B"] = studentnr;
                        newSheet.Cells[c, "D"] = o[v]["studentpunten"];
                        newSheet.Cells[c, "E"] = o[v]["commentaartext"];
                        newSheet.Range[$"A{c}"].EntireRow.EntireColumn.AutoFit();
                        string error = o[v]["errors"].ToString();
                        if (error.StartsWith("Correct"))
                        {
                            ac++;
                            newSheet.Cells[c, "C"] = "Correct!";
                            newSheet.Range[$"C{c}"].Interior.ColorIndex = 43;
                        }
                        else if (error.StartsWith("ExceptionDuringExecution"))
                        {
                            er++;
                            newSheet.Cells[c, "C"] = "Runtime error";
                            newSheet.Range[$"C{c}"].Interior.ColorIndex = 44;
                        }
                        else if (error.StartsWith("CompileError"))
                        {
                            ec++;
                            newSheet.Cells[c, "C"] = "Compile error";
                            newSheet.Range[$"C{c}"].Interior.ColorIndex = 46;
                        }
                        else if (error.StartsWith("Open vraag"))
                        {
                            newSheet.Cells[c, "C"] = "Open vraag";
                            newSheet.Range[$"C{c}"].Interior.ColorIndex = 15;
                        }
                        else
                        {
                            unknown = true;
                            newSheet.Cells[c, "C"] = "Anders...";
                            newSheet.Range[$"C{c}"].Interior.ColorIndex = 48;
                        }
                    }
                    decimal cijfer = Math.Round(((punten / totaalpunten) * 9) + 1, 1);
                    if (cijfer > 10.0m) cijfer = 10.0m;
                    else if (cijfer < 1.0m) cijfer = 1.0m;
                    oSheet.Cells[c + m, "C"] = cijfer;
                    if (cijfer >= 5.5m) oSheet.Range[$"C{c + m}"].Interior.ColorIndex = 43;
                    else oSheet.Range[$"C{c + m}"].Interior.ColorIndex = 46;
                    if (er == 0 && ec == 0)
                    {
                        oSheet.Cells[c + m, "D"] = "Alles correct!";
                        oSheet.Range[$"D{c + m}"].Interior.ColorIndex = 43;
                    }
                    else if (er != 0 && ec == 0)
                    {
                        oSheet.Cells[c + m, "D"] = $"{er} runtime error(s), {ac} correct.";
                        oSheet.Range[$"D{c + m}"].Interior.ColorIndex = 44;
                    }
                    else if (er == 0 && ec != 0)
                    {
                        oSheet.Cells[c + m, "D"] = $"{ec} compiler error(s), {ac} correct.";
                        oSheet.Range[$"D{c + m}"].Interior.ColorIndex = 46;
                    }
                    else if (unknown)
                    {
                        oSheet.Cells[c + m, "D"] = $"Onbekende fout(en)... {ec} compiler error(s), {er} runtime error(s), {ac} correct.";
                        oSheet.Range[$"D{c + m}"].Interior.ColorIndex = 48;
                    }
                    else
                    {
                        oSheet.Cells[c + m, "D"] = $"{ec} compiler error(s), {er} runtime error(s), {ac} correct.";
                        oSheet.Range[$"D{c + m}"].Interior.ColorIndex = 46;
                    }
                    oSheet.Range[$"A{c}"].EntireRow.EntireColumn.AutoFit();
                }
                for (int i = 0; i < totaalvragen; i++)
                {
                    Microsoft.Office.Interop.Excel.Worksheet commentSheet;
                    commentSheet = oWB.Worksheets[i + 2];
                    int x = totaalstudenten + 4;
                    commentSheet.Cells[x - 1, "A"] = "Commentaarnaam";
                    commentSheet.Cells[x - 1, "B"] = "Aantal";
                    commentSheet.Range[$"A{x - 1}", $"B{x - 1}"].Font.Bold = true;
                    DataRow r = vragen.Tables[0].Rows[i];
                    DataRow[] q = commentaar.Tables[0].Select($"vraagid = {r["vraagid"]}");
                    DataRow[] t = opdr.Tables[0].Select($"vraagid = {r["vraagid"]}");
                    int[] count = new int[q.Length];
                    for (int j = 0; j < q.Length; j++)
                    {
                        foreach (DataRow ts in t)
                        {
                            //todo: werkt nog niet helemaal goed
                            if (ts["commentaar"].ToString().Length > j)
                                if (ts["commentaar"].ToString()[j] == '1')
                                {
                                    count[j]++;
                                }
                        }
                        commentSheet.Cells[x, "A"] = q[j]["commentaarnaam"];
                        commentSheet.Cells[x, "B"] = count[j];
                        x++;
                    }
                }
                oXL.Visible = true;
            }
        }
        /// <summary>
        /// Achterhaald: gebruik nu CreateTestRapportSQL();
        /// </summary>
        public static void CreateTestRapport(List<TestRapport> repports)
        {
            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel.Workbook oWB;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;
            object misvalue = System.Reflection.Missing.Value;
            //try
            //{
            //Start Excel and get Application object.
            // Link aan Excel
            oXL = new Microsoft.Office.Interop.Excel.Application();
            // Zorg ervoor dat Excel opent (zodat je kan zien wat er gebeurd, kan misschien later uit worden gezet)
            oXL.Visible = true;
            // Maakt een nieuw werkboek
            oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(""));
            // Maakt een nieuw werkblad in het werkboek
            oSheet = oWB.Worksheets[1];
            // Hernoemt het werkblad
            oSheet.Name = "Toetsresultaten";

            // Maak een header
            // met sheet.Cells[kolom, rij]
            oSheet.Cells[1, "A"] = "Voornaam";
            oSheet.Cells[1, "B"] = "Achternaam";
            oSheet.Cells[1, "C"] = "Studentnummer";
            oSheet.Cells[1, "D"] = "Gebruikersnaam";
            oSheet.Cells[1, "E"] = "Klas";
            oSheet.Cells[1, "F"] = "Datum";
            oSheet.Cells[1, "G"] = "% goed";
            oSheet.Cells[1, "H"] = "Testresultaat";

            // Maak de header dikgedrukt en zorg ervoor dat de tekst in de vakken past.
            // Bij Range moet ipv [kolom, rij] de celnaam staan ["A1"] bijvoorbeeld
            oSheet.Range["A1"].EntireRow.Font.Bold = true;
            oSheet.Range["A1"].EntireRow.EntireColumn.AutoFit();

            // Voor elk opgegeven TestReport, voer info in.
            for (int i = 0; i < repports.Count; i++)
            {
                // Teller voor het celnummer.
                int c = i + 2;
                // Aantal fouten (compiler en runtime).
                int ec = 0;
                int er = 0;
                int te = 0;
                // Bekijk hoeveel fouten er zijn.
                foreach (RapportQuestion q in repports[i].RapportQuestions)
                {
                    if (q.CompileAndExecuteInfo.Result == CompilerUtil.CompileAndExecuteInfo.eStatus.CompileError) ec++;
                    if (q.CompileAndExecuteInfo.Result == CompilerUtil.CompileAndExecuteInfo.eStatus.ExceptionDuringExecution) er++;
                }

                oSheet.Cells[c, "A"] = repports[i].StudentInfo.FirstName;
                oSheet.Cells[c, "B"] = repports[i].StudentInfo.LastName;
                oSheet.Cells[c, "C"] = repports[i].StudentInfo.StudentNr;
                oSheet.Cells[c, "D"] = repports[i].StudentInfo.UserName;
                oSheet.Cells[c, "E"] = repports[i].StudentInfo.Datum;
                oSheet.Cells[c, "F"] = repports[i].RapportQuestions.Count;
                oSheet.Cells[c, "G"] = "?";
                oSheet.Cells[c, "H"] = "?";

                // Bereken score
                // Elke vraag heeft hier dus dezelfde aantal punten!
                double score = 100.0 - Math.Round(((ec + er) / repports[i].RapportQuestions.Count) * 100.0, 2);
                oSheet.Cells[c, "G"] = score;
                if (score >= 5.5)
                {
                    oSheet.Range[$"G{c}"].Interior.ColorIndex = 43;
                }
                else
                {
                    oSheet.Range[$"G{c}"].Interior.ColorIndex = 46;
                }

                // Als er geen fouten zijn...
                if (ec == 0 && er == 0)
                {
                    oSheet.Cells[c, "H"] = "Geen fouten.";
                    oSheet.Range[$"H{c}"].Interior.ColorIndex = 43;
                }
                // Als er runtime fouten zijn...
                else if (er > 0 && ec == 0)
                {
                    oSheet.Cells[c, "H"] = $"{er} runtimefout(en).";
                    oSheet.Range[$"H{c}"].Interior.ColorIndex = 44;
                }
                // Als er een compilatiefout is...
                else if (ec > 0 && er == 0)
                {
                    oSheet.Cells[c, "H"] = $"{ec} compilatiefout(en).";
                    oSheet.Range[$"H{c}"].Interior.ColorIndex = 46;
                }
                else if (ec > 0 && er > 0)
                {
                    oSheet.Cells[c, "H"] = $"{ec} compilatiefout(en) en {er} runtimefout(en). ";
                    oSheet.Range[$"H{c}"].Interior.ColorIndex = 46;
                }
                // Zorg ervoor dat de tekst past in de vakjes.
                oSheet.Range[$"A{c}"].EntireRow.EntireColumn.AutoFit();
            }

            // Voor elke opdracht een apart werkblad maken en daar info invullen...
            // Werkt dit goed?
            for (int i = 0; i < repports[i].RapportQuestions.Count; i++)
            {
                // Maakt een nieuw werkblad in het werkboek
                Microsoft.Office.Interop.Excel.Worksheet newSheet;
                newSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.Worksheets.Add(After: oWB.Sheets[oWB.Sheets.Count]);
                // Hernoemt het werkblad
                newSheet.Name = $"Opdracht {i + 1}";
                newSheet.Activate();
                // TODO: maak dit nog
                newSheet.Cells[1, "A"] = "Voornaam";
                newSheet.Cells[1, "B"] = "Achternaam";
                newSheet.Cells[1, "C"] = "Studentnummer";
                newSheet.Cells[1, "D"] = "Gebruikersnaam";
                newSheet.Cells[1, "E"] = "Klas";
                newSheet.Cells[1, "F"] = $"Resultaat opdracht {i + 1}";
                newSheet.Cells[1, "G"] = $"Resultaat opdracht {i + 1}";
                newSheet.Cells[1, "H"] = $"Resultaat opdracht {i + 1}";
                newSheet.Range["A1"].EntireRow.Font.Bold = true;
                newSheet.Range["A1"].EntireRow.EntireColumn.AutoFit();
                for (int j = 0; j < repports.Count; j++)
                {
                    int c2 = j + 2;
                    newSheet.Cells[c2, "A"] = repports[j].StudentInfo.FirstName;
                    newSheet.Cells[c2, "B"] = repports[j].StudentInfo.LastName;
                    newSheet.Cells[c2, "C"] = repports[j].StudentInfo.StudentNr;
                    newSheet.Cells[c2, "D"] = repports[j].StudentInfo.UserName;
                    newSheet.Cells[c2, "E"] = repports[j].StudentInfo.Klas;
                    newSheet.Cells[c2, "F"] = "?";
                    if (repports[j].RapportQuestions[i].CompileAndExecuteInfo.Result == CompilerUtil.CompileAndExecuteInfo.eStatus.CompileError)
                    {
                        newSheet.Cells[c2, "F"] = "Compilatiefout.";
                        newSheet.Range[$"F{c2}"].Interior.ColorIndex = 46;
                    }
                    else if (repports[j].RapportQuestions[i].CompileAndExecuteInfo.Result == CompilerUtil.CompileAndExecuteInfo.eStatus.ExceptionDuringExecution)
                    {
                        newSheet.Cells[c2, "F"] = "Runtimefout.";
                        newSheet.Range[$"F{c2}"].Interior.ColorIndex = 44;
                    }
                    else
                    {
                        newSheet.Cells[c2, "F"] = "Correct!";
                        newSheet.Range[$"F{c2}"].Interior.ColorIndex = 43;
                    }
                    oSheet.Range[$"A{c2}"].EntireRow.EntireColumn.AutoFit();
                }
            }
            //oXL.Visible = false;
            //oXL.UserControl = false;
            //oWB.SaveAs(@"C:\Dev\Werk\NakijkTool\NakijkTool\test505.xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook,
            //        Type.Missing, Type.Missing,
            //        false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
            //        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


            //}
            //catch (Exception ex)
            //{
            //    ;
            //}
        }
    }
    public class WriteToExcel
    {
        List<TestRapport> rapp = null;
        public WriteToExcel(string toetsnaam)
        {
            string _naam = toetsnaam;
        }
        public void AddRapport(List<TestRapport> r)
        {
            if (rapp == null) rapp = r;
            else
            {
                for (int i = 0; i < rapp.Count; i++)
                {
                    rapp[i].RapportQuestions.Add(r[i].RapportQuestions[0]);
                }
            }
        }
        public void CreateRapport()
        {
            ExcelWriter.CreateTestRapport(rapp);
        }
    }
}
