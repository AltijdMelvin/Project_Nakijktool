using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;

namespace NakijkTool
{
    public class TestRapport
    {
        public List<RapportQuestion> RapportQuestions { get; set; }
        public StudentInfo StudentInfo { get; set; }
        public string CsCode { get; set; }

        public TestRapport()
        {
            RapportQuestions = new List<RapportQuestion>();
        }
    }

    public class RapportQuestion
    {
        public int Nr { get; set; }
        public List<RapportTestCase> RapportTestCases { get; set; }
        public int NrAsserts { get; set; }
        public CompilerUtil.CompileAndExecuteInfo CompileAndExecuteInfo { get; set; }
        public string StudentSourceCode { get; set; }

        [XmlIgnore()]
        public int NrErrors => RapportTestCases.Count;

        public List<Diagnostic> Diagnostics { get; set; }
        public Exception Exception { get; set; }

        public RapportQuestion()
        {
            RapportTestCases = new List<RapportTestCase>();
        }
    }

    public class RapportTestCase
    {
        public int Nr { get; set; }
        public string Error { get; set; }
        public string Code { get; set; }
        public string TestCode { get; set; }
    }

    public class StudentInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string StudentNr { get; set; }
        public string Klas { get; set; }
        public string Datum { get; set; }
    }
}
