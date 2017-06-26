using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace NakijkTool
{
    public class Program
    {
        readonly int? questionNumber = 2;
        const string Prefix = "Tentamen Programmeren 3_";

        readonly string[] _questionNamesTest = { "TestVraag1", "Test_Vraag2", "TestVraag3", "TestVraag4", "Test_Vraag5" };
        readonly string[] _questionNames = { "Vraag1", null, "Vraag3", "Vraag4", "Vraag5" };
        private readonly string[] _loadExtraCode = null; //{ "", "", "", "", @"C:\Dev\Werk\Programmeren\Programmeren2Tests2\Tentamens\StudentDatabase.cs" };
        private string[] _testMethodeCode;

        private const string directoryExamResults = @"C:\Users\Joris Lops\Desktop\gradebook_TECH_E_13_256_Tentamen20Programmeren203_2017-03-17-15-18-38";
        private const string TestsFileSrc = @"C:\Dev\Werk\Programmeren\Programmeren2Tests2\Tentamens\TentamenPrg3-1-2016-2017-AntwoordModel.cs";
        
        MetadataReference[] references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(NUnit.Framework.Assert).Assembly.Location)
        };

        public Program()
        {
            _testMethodeCode = LoadTestMethodsCode(TestsFileSrc);
        }

        static void Main(string[] args)
        {
            List<TestRapport> repports = new List<TestRapport>();


            var files = Directory.GetFiles(
                directoryExamResults,
                searchPattern: "*.cs");

            Program p = new Program();

            foreach (var stundentCsFilePath in files.Skip(16).Take(5))
            {
                Console.WriteLine($"Processing {stundentCsFilePath}");
                TestRapport testRapport = p.GetTestRapport(stundentCsFilePath);
                repports.Add(testRapport);
            }
            //ExcelWriter.CreateTestRapport(repports);

            string[] usernames = files.Select(f => GetUsernNameFromFile(f, Prefix)).ToArray();

            using (StreamWriter writer = File.CreateText(@"c:\dev\tmp\test.txt"))
            {
                var reportsByName = repports.Where(rep => rep.StudentInfo.UserName != "NA").ToDictionary(rep => rep.StudentInfo.UserName.ToLower());
                foreach (string username in usernames)
                {
                    if (reportsByName.ContainsKey(username))
                    {
                        var errors = reportsByName[username].RapportQuestions.SelectMany(x => x.RapportTestCases).Select(x => x.Error);
                        StringBuilder errorBuilder = new StringBuilder();
                        foreach (var error in errors)
                        {
                            var parts = error.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                            errorBuilder.Append(parts[0]);
                            if (parts.Length > 1)
                            {
                                errorBuilder.Append(",");
                                errorBuilder.Append(parts[1]);
                            }
                        }

                        string errorMsg = errorBuilder.ToString(0, errorBuilder.Length > 100 ? 100 : errorBuilder.Length);
                        //IEnumerable<IEnumerable<string>> r = reportsByName[username].RapportQuestions.Select(x => x.RapportTestCases.Select(w => w.Error)).ToList();

                        writer.WriteLine(
                            $"{username},{reportsByName[username].RapportQuestions[0].CompileAndExecuteInfo.Result},{errorMsg},{reportsByName[username].RapportQuestions[0].MethodeSourceCode}");
                    }
                    else
                    {
                        writer.WriteLine(username);
                    }
                }
            }


            Console.WriteLine("finished!");
            Console.ReadLine();
        }

        public string[] LoadTestMethodsCode(string testsFileSrcPath)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(testsFileSrcPath));
            var diagnostics = syntaxTree.GetDiagnostics(syntaxTree.GetRoot());
            if (diagnostics.Any())
            {
                throw new ArgumentException($"Compile error in {testsFileSrcPath}");
            }

            string[] testMethodesCode = new string[_questionNamesTest.Length];
            for (int i = 0; i < testMethodesCode.Length; i++)
            {
                string testMethodeCode = GetMethod(syntaxTree, _questionNamesTest[i]);
                testMethodesCode[i] = testMethodeCode;
            }
            return testMethodesCode;
        }

        public TestRapport GetTestRapport(string csCodeFilePath)
        {
            string csCode = File.ReadAllText(csCodeFilePath);

            TestRapport rapport = new TestRapport
            {
                CsCode = csCode,
                StudentInfo = ExtractStudentInfo(csCode, csCodeFilePath, Prefix),
                RapportQuestions =
                    ExecuteTests(csCode)
                        .ToList()
            };
            return rapport;
        }

        public IEnumerable<RapportQuestion> ExecuteTests(string csSourceCode)
        {
            var rapportQuestions = new List<RapportQuestion>();

            for (int questIdx = 0; questIdx < _questionNames.Length; questIdx++)
            {
                if (questionNumber.HasValue)
                {
                    if (questIdx + 1 != questionNumber)
                    {
                        continue;
                    }
                }

                RapportQuestion rapportQuestion = new RapportQuestion
                {
                    Nr = questIdx + 1,
                };
                rapportQuestions.Add(rapportQuestion);

                var syntaxTree = CSharpSyntaxTree.ParseText(csSourceCode);
                var diagnostics = syntaxTree.GetDiagnostics(syntaxTree.GetRoot());
                if (diagnostics.Any(x => x.Severity == DiagnosticSeverity.Error))
                {
                    rapportQuestion.Diagnostics = diagnostics.ToList();
                    rapportQuestion.CompileAndExecuteInfo = new CompilerUtil.CompileAndExecuteInfo()
                    {
                        Result = CompilerUtil.CompileAndExecuteInfo.eStatus.CompileError,
                        CompilerDiagnostics = diagnostics.ToList()
                    };
                    return rapportQuestions;
                }

                string testMethodeCode = _testMethodeCode[questIdx];
                string methodeToTestNameCode = GetMethod(syntaxTree, _questionNames[questIdx]);

                rapportQuestion.MethodeSourceCode = methodeToTestNameCode;

                List<string> additionSourceFiles = new List<string>();
                if (_loadExtraCode != null && _loadExtraCode[questIdx] != String.Empty)
                {
                    additionSourceFiles.Add(_loadExtraCode[questIdx]);
                }

                CompilerUtil compilerUtil = new CompilerUtil();

                string codeToExecute = testMethodeCode + methodeToTestNameCode;

                CompilerUtil.CompileAndExecuteInfo compileAndExecuteInfo =
                    compilerUtil.CompileAndExcuteMethod(
                        codeToExecute,
                        _questionNamesTest[questIdx],
                        references,
                        additionSourceFiles);

                rapportQuestion.NrAsserts = CountOccurances(testMethodeCode, "Assert");
                rapportQuestion.CompileAndExecuteInfo = compileAndExecuteInfo;

                switch (compileAndExecuteInfo.Result)
                {
                    case CompilerUtil.CompileAndExecuteInfo.eStatus.CompileError:
                        break;
                    case CompilerUtil.CompileAndExecuteInfo.eStatus.Incorrect:
                        if (rapportQuestion.NrAsserts > 0)
                        {
                            for (int i = 0; i < rapportQuestion.NrAsserts; i++)
                            {
                                string testMethodeCodeOneAssert = RemoveAllAssertsExcept(i, testMethodeCode);

                                codeToExecute = testMethodeCodeOneAssert + methodeToTestNameCode;
                                compileAndExecuteInfo = compilerUtil.CompileAndExcuteMethod(codeToExecute, _questionNamesTest[questIdx], references, additionSourceFiles);

                                if (compileAndExecuteInfo.Result != CompilerUtil.CompileAndExecuteInfo.eStatus.Correct)
                                {
                                    rapportQuestion.RapportTestCases.Add(new RapportTestCase()
                                    {
                                        Code = methodeToTestNameCode,
                                        Error = compileAndExecuteInfo.Message,
                                        TestCode = testMethodeCodeOneAssert
                                    });
                                }
                            }
                        }
                        break;
                    case CompilerUtil.CompileAndExecuteInfo.eStatus.Correct:
                        break;
                    case CompilerUtil.CompileAndExecuteInfo.eStatus.InfinitLoop:
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return rapportQuestions;
        }


        private static StudentInfo ExtractStudentInfo(string csSourceCode, string fileName, string prefix)
        {
            StudentInfo info = new StudentInfo();

            try
            {
                string userName = GetUsernNameFromFile(fileName, prefix);
                info.UserName = userName;
            }
            catch (Exception ex)
            {
                info.UserName = "NA";
            }

            string[] lines = csSourceCode.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            string[] startWiths = { "Voornaam", "Achternaam", "StudentNr", "Klas", "Datum" };
            string[] mapToProperty = { "FirstName", "LastName", "StudentNr", "Klas", "Datum" };

            foreach (var line in lines)
            {
                string l = line.Trim();
                if (!l.StartsWith(@"//"))
                    continue;

                for (int index = 0; index < startWiths.Length; index++)
                {
                    var startWith = startWiths[index];
                    if (l.StartsWith(@"//" + startWith))
                    {
                        string value = line.Substring(line.IndexOf(":") + 1).Trim();
                        info.GetType().GetProperty(mapToProperty[index]).SetValue(info, value);
                        break;
                    }
                }
            }

            return info;
        }

        private static string GetUsernNameFromFile(string fileName, string prefix)
        {
            var afterPrefix = fileName.Substring(fileName.IndexOf(prefix) + prefix.Length);
            string userName = afterPrefix.Substring(0, afterPrefix.IndexOf("_"));
            return userName;
        }

        public static string RemoveAllAssertsExcept(int nr, string sourceCode)
        {
            string[] lines = sourceCode.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            StringBuilder sb = new StringBuilder();
            int assertIndex = 0;
            foreach (var line in lines)
            {
                if (line.Contains("Assert"))
                {
                    if (nr == assertIndex)
                    {
                        sb.AppendLine(line);
                    }
                    assertIndex++;
                }
                else
                {
                    sb.AppendLine(line);
                }
            }
            return sb.ToString();
        }

        public static int CountOccurances(string source, string substring)
        {
            int count = 0, n = 0;

            if (substring != "")
            {
                while ((n = source.IndexOf(substring, n, StringComparison.InvariantCultureIgnoreCase)) != -1)
                {
                    n += substring.Length;
                    ++count;
                }
            }
            return count;
        }


        static string GetMethod(SyntaxTree syntaxTree, string methodName)
        {
            var root = syntaxTree.GetRoot();
            var method = root
                             .DescendantNodes()
                             .OfType<MethodDeclarationSyntax>()
                             .FirstOrDefault(md => md.Identifier.ValueText.Equals(methodName));

            if (method == null)
            {
                return "";
            }
            return method.ToString();
        }
    }
}