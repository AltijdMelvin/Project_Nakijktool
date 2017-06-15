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
    public interface ICodeLocator
    {
        string GetCode(SyntaxTree syntaxTree);
    }

    public class MethodCodeLocator : ICodeLocator
    {
        private readonly string _methodeName;

        public MethodCodeLocator(string methodeName)
        {
            _methodeName = methodeName;
        }

        public string GetCode(SyntaxTree syntaxTree)
        {
            var root = syntaxTree.GetRoot();
            var method = root
                             .DescendantNodes()
                             .OfType<MethodDeclarationSyntax>()
                             .FirstOrDefault(md => md.Identifier.ValueText.Equals(_methodeName));

            if (method == null)
            {
                return "";
            }
            return method.ToString();
        }
    }

    public class ClassCodeLocator : ICodeLocator
    {
        private readonly string _className;

        public ClassCodeLocator(string className)
        {
            _className = className;
        }

        public string GetCode(SyntaxTree syntaxTree)
        {
            var root = syntaxTree.GetRoot();
            var classCode = root
                             .DescendantNodes()
                             .OfType<ClassDeclarationSyntax>()
                             .FirstOrDefault(md => md.Identifier.ValueText.Equals(_className));

            if (classCode == null)
            {
                return "";
            }
            return classCode.ToString();
        }
    }

    public class CompositionCodeCompose : ICodeLocator
    {
        private IEnumerable<ICodeLocator> _locators;

        public CompositionCodeCompose(IEnumerable<ICodeLocator> locators)
        {
            _locators = locators;
        }

        public string GetCode(SyntaxTree syntaxTree)
        {
            string code = String.Join("\n", _locators.Select(x => x.GetCode(syntaxTree)));

            if (code == null)
            {
                return "";
            }
            return code;
        }

    }

    public class ClassesCodeLocator : ICodeLocator
    {
        private IEnumerable<ClassCodeLocator> _locators;

        public ClassesCodeLocator(IEnumerable<string> classNames )
        {
            _locators = classNames.Select(className => new ClassCodeLocator(className));
        }

        public string GetCode(SyntaxTree syntaxTree)
        {
            string classesCode = String.Join("\n",
                _locators.Select(x => x.GetCode(syntaxTree)).ToList());
            

            if (classesCode == null)
            {
                return "";
            }
            return classesCode;
        }

    }

    public class Program
    {
        int? questionNumber = 2;

        public int? QuestionNumber
        {
            get { return this.questionNumber; }
            set { this.questionNumber = value; }
        }
        

        const string examPrefixNameBeforeUserName = "Tentamen Programmeren 3_";
        //const string Prefix = "Tentamen Programmeren 3_";

        readonly string[] _questionNamesTest = { "TestVraag1", "TestVraag2", "TestVraag3", "TestVraag4A", "TestVraag5A" };
        private static readonly ClassesCodeLocator classesCodeLocator = new ClassesCodeLocator(new string[] { "Bus", "Lijn", "Halte" });
        readonly ICodeLocator[] _questionCodeLoactors = { new MethodCodeLocator("Vraag1"),
            classesCodeLocator,
            new CompositionCodeCompose(new ICodeLocator[]{classesCodeLocator, new MethodCodeLocator("Vraag3")})
            , /*new CompositionCodeCompose(new ICodeLocator[] {new ClassCodeLocator("HalteStack"), new ClassCodeLocator("Halte") })*/
            new ClassCodeLocator("HalteStack"), 
            new CompositionCodeCompose(new [] {new ClassCodeLocator("HalteLinked"),
                new ClassCodeLocator("LijnLinked")})};
        private readonly string[] _loadExtraCode = null; //{ "", "", "", "", @"C:\Dev\Werk\Programmeren\Programmeren2Tests2\Tentamens\StudentDatabase.cs" };
        private string[] _testMethodeCode;

        private const string directoryExamResults = @"C:\Users\Emiell\Documents\GitHub\Project_Nakijktool\Anonieme tentamens\prg3Anoniem";
        private const string TestsFileSrc = @"C:\Users\Emiell\Documents\GitHub\Project_Nakijktool\Anonieme tentamens\TentamenPrg3-1-2016-2017-AntwoordModel.txt";
        
        MetadataReference[] references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(NUnit.Framework.Assert).Assembly.Location)
        };

        public Program(string testfilesrc)
        {
            _testMethodeCode = LoadTestMethodsCode(testfilesrc); //laad testmethodes uit het nakijkblad
            //controleert of de vraag bestaat
            if (questionNumber.HasValue)
            {
                if (string.IsNullOrWhiteSpace(_testMethodeCode[questionNumber.Value-1]))
                {
                    throw new ArgumentException("Question not found");
                }
            }
            else if(_testMethodeCode.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException("Questions not found");
            }
        }

        static void Main(string[] args)
        {
            List<TestRapport> repports = new List<TestRapport>(); //maakt een lijst van testrapporten

            //haalt alle tentamens uit de ingevuld maplocatie (eindigend op .cs)
            var files = Directory.GetFiles(
                directoryExamResults,
                searchPattern: "*.cs");

            Program p = new Program(TestsFileSrc);

            //maakt van alle .cs files testrapporten
            foreach (var stundentCsFilePath in files/*.Skip(16).Take(5)*/)
            {
                Console.WriteLine($"Processing {stundentCsFilePath}");
                TestRapport testRapport = p.GetTestRapport(stundentCsFilePath);
                repports.Add(testRapport);
            }
            //ExcelWriter.CreateTestRapport(repports);
        }

        public void FileWriterReport(string[] files, List<TestRapport> repports, int questionNr)
        {
            string[] usernames = files.Select(f => GetUsernNameFromFile(f, examPrefixNameBeforeUserName)).ToArray();
            string vraag = @"C:\Users\Emiell\Documents\GitHub\Project_Nakijktool\Anonieme tentamens\Vraag" + Convert.ToString(questionNr) + ".txt";
            using (StreamWriter writer = File.CreateText(vraag))
            {
                var reportsByName = new Dictionary<string, TestRapport>();
                foreach (var rep in repports)
                {
                    if (rep.StudentInfo.UserName != "NA")
                        reportsByName.Add(rep.StudentInfo.UserName, rep);
                }


                foreach (string username in usernames)
                {
                    if (reportsByName.ContainsKey(username))
                    {
                        var testRapport = reportsByName[username];

                        var errors = testRapport.RapportQuestions
                            .SelectMany(x => x.RapportTestCases)
                            .Select(x => x.Error);

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

                        writer.WriteLine($"{testRapport.StudentInfo.UserName} \t ---Begin---------------------");

                        if (testRapport.RapportQuestions[0].CompileAndExecuteInfo.Result ==
                            CompilerUtil.CompileAndExecuteInfo.eStatus.CompileError)
                        {
                            writer.WriteLine("Compiler Errors:");
                            writer.WriteLine(string.Join("\n",
                                testRapport.RapportQuestions[0].CompileAndExecuteInfo.CompilerDiagnostics.Select(x => x.ToString())));
                            writer.WriteLine("##################");
                            writer.WriteLine();
                        }

                        if (testRapport.RapportQuestions[0].Exception != null)
                        {
                            writer.WriteLine("Runtime Errors:");
                            writer.WriteLine(testRapport.RapportQuestions[0].Exception.Message);
                            writer.WriteLine("-#-#-#-#-#-#-#-#");
                            writer.WriteLine();
                        }


                        writer.WriteLine(
                            $"{username},{testRapport.RapportQuestions[0].CompileAndExecuteInfo.Result},{errorMsg}");

                        if (testRapport.RapportQuestions[0].CompileAndExecuteInfo.Result ==
                            CompilerUtil.CompileAndExecuteInfo.eStatus.ExceptionDuringExecution)
                        {
                            writer.WriteLine($"Runtime exception: {testRapport.RapportQuestions[0].CompileAndExecuteInfo.Message}");
                        }

                        writer.WriteLine(testRapport.RapportQuestions[0].StudentSourceCode);

                        writer.WriteLine($"{testRapport} \t ---End---------------------");
                        writer.WriteLine();
                    }
                    else
                    {
                        writer.WriteLine(username);
                    }
                }
            }
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
                var questionNamesString = _questionNamesTest[i].Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
                
                StringBuilder questionCodes = new StringBuilder();
                foreach (var questionName in questionNamesString)
                {
                    string testMethodeCode = GetMethod(syntaxTree, questionName.Trim());
                    questionCodes.AppendLine(testMethodeCode);
                }
                testMethodesCode[i] = questionCodes.ToString();
            }
            return testMethodesCode;
        }

        public TestRapport GetTestRapport(string csCodeFilePath)
        {
            string csCode = File.ReadAllText(csCodeFilePath);

            TestRapport rapport = new TestRapport
            {
                CsCode = csCode,
                StudentInfo = ExtractStudentInfo(csCode, csCodeFilePath, examPrefixNameBeforeUserName), //haalt alle gegevens uit .cs file
                RapportQuestions =
                    ExecuteTests(csCode)
                        .ToList()
            };
            return rapport;
        }

        public IEnumerable<RapportQuestion> ExecuteTests(string csSourceCode)
        {
            var rapportQuestions = new List<RapportQuestion>();

            for (int questIdx = 0; questIdx < _questionCodeLoactors.Length; questIdx++)
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

                //code is correct
                string testMethodeCode = _testMethodeCode[questIdx];
                string studentCode = _questionCodeLoactors[questIdx].GetCode(syntaxTree);

                rapportQuestion.StudentSourceCode = studentCode;

                List<string> additionSourceFiles = new List<string>();
                if (_loadExtraCode != null && _loadExtraCode[questIdx] != String.Empty)
                {
                    additionSourceFiles.Add(_loadExtraCode[questIdx]);
                }

                CompilerUtil compilerUtil = new CompilerUtil();

                string codeToExecute = testMethodeCode + studentCode;

                CompilerUtil.CompileAndExecuteInfo compileAndExecuteInfo = null;
                try
                {
                    compileAndExecuteInfo = compilerUtil.CompileAndExcuteMethod(
                        codeToExecute,
                        _questionNamesTest[questIdx],
                        references,
                        additionSourceFiles);
                }
                catch (Exception ex)
                {
                    rapportQuestion.Exception = ex;
                }

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

                                codeToExecute = testMethodeCodeOneAssert + studentCode;
                                compileAndExecuteInfo = compilerUtil.CompileAndExcuteMethod(codeToExecute, _questionNamesTest[questIdx], references, additionSourceFiles);

                                if (compileAndExecuteInfo.Result != CompilerUtil.CompileAndExecuteInfo.eStatus.Correct)
                                {
                                    rapportQuestion.RapportTestCases.Add(new RapportTestCase()
                                    {
                                        Code = studentCode,
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
                    case CompilerUtil.CompileAndExecuteInfo.eStatus.ExceptionDuringExecution:
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            return rapportQuestions;
        }


        private static StudentInfo ExtractStudentInfo(string csSourceCode, string fileName, string prefix) //haalt informatie uit het .cs bestand/rapport
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
