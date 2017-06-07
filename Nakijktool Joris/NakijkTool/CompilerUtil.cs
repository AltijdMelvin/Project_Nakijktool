using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace NakijkTool
{
    public class CompilerUtil
    {
        public class CompileAndExecuteInfo
        {
            public enum eStatus
            {
                Correct,
                Incorrect,
                CompileError,
                InfinitLoop,
                ExceptionDuringExecution
            }

            public eStatus Result { get; set; }
            public List<Diagnostic> CompilerDiagnostics { get; set; }
            public string Message { get; set; }

            public CompileAndExecuteInfo()
            {
                CompilerDiagnostics = new List<Diagnostic>();
            }
        }

        public CompileAndExecuteInfo CompileAndExcuteMethod(string methodeCode, string methodName, IEnumerable<MetadataReference> references, List<string> additionSourceFiles = null)
        {
            CompileAndExecuteInfo cr = new CompileAndExecuteInfo();

            string program = @"
                using System;
                using System.Collections.Generic;
                using System.Text;
                using NUnit.Framework;
                
                namespace Tentamens
                {
                    public class QuestionAndTest
                    {
                        {0}
                    }

                    public class TestUtils
                    {
                        public static string DisplayArrays<T>(IEnumerable<T> expected, IEnumerable<T> actual) => $""Expected: { string.Join("","", expected)} { Environment.NewLine} Actual: { string.Join("","", actual)}"";
                    }
                }";
            program = program.Replace("{0}", methodeCode);

            //CSharpScriptEngine.Execute(program, references, methodName);

            IList<SyntaxTree> syntaxTrees = additionSourceFiles
                ?.Select(sourceFile => CSharpSyntaxTree.ParseText(File.ReadAllText(sourceFile))).ToList()
                ?? new List<SyntaxTree>();
            syntaxTrees.Add(CSharpSyntaxTree.ParseText(program));

            string assemblyName = Path.GetRandomFileName();


            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary,
                optimizationLevel: OptimizationLevel.Debug
            );

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees,
                references,
                options: options);

            Assembly assembly = null;
            using (var ms = new MemoryStream())
            using (var pdbstream = new MemoryStream())
            {
                //string dllFilePath = @"C:\Dev\Werk\NakijkTool\NakijkTool\Test.dll";
                //EmitResult result = compilation.Emit(dllFilePath);
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    cr.CompilerDiagnostics = failures.ToList();
                    //foreach (Diagnostic diagnostic in failures)
                    //{
                    //    Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    //    cr.CompilerDiagnostics.Add
                    //}
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    assembly = Assembly.Load(ms.ToArray(), pdbstream.ToArray());
                }
            }

            Type type;
            object obj;
            try
            {
                //Assembly assembly = Assembly.LoadFile(dllFilePath);
                type = assembly.GetType("Tentamens.QuestionAndTest");
                obj = Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                cr.Result = CompileAndExecuteInfo.eStatus.CompileError;
                cr.Message = ex.Message;
                return cr;
            }



            try
            {
                Task task = Task.Run(() =>
                {
                    try
                    {
                        type.InvokeMember(methodName,
                            BindingFlags.Default | BindingFlags.InvokeMethod,
                            null,
                            obj,
                            new object[] {});
                    }
                    catch (TargetInvocationException ex)
                    {
                        cr.Result = CompileAndExecuteInfo.eStatus.ExceptionDuringExecution;
                        cr.Message = $"{ex.InnerException} {ex.Message}";
                        //Console.WriteLine(cr.Message);
                    }
                    catch (Exception ex)
                    {
                        cr.Result = CompileAndExecuteInfo.eStatus.ExceptionDuringExecution;
                        cr.Message = ex.Message;
                    }
                });

                try
                {
                    bool result = task.Wait(2000);

                    if (cr.Result == CompileAndExecuteInfo.eStatus.ExceptionDuringExecution)
                    {
                        return cr;
                    }

                    if (!result)
                    {
                        cr.Result = CompileAndExecuteInfo.eStatus.InfinitLoop;
                        cr.Message = "Infinit loop";
                        return cr;
                    }
                }
                catch (Exception ex)
                {
                    cr.Result = CompileAndExecuteInfo.eStatus.ExceptionDuringExecution;
                    cr.Message = ex.Message;
                }
                

                cr.Result = CompileAndExecuteInfo.eStatus.Correct;
                return cr;
            }
            catch (AggregateException ex)
            {
                cr.Result = CompileAndExecuteInfo.eStatus.Incorrect;
                cr.Message = ex.InnerExceptions[0].InnerException.Message;
                return cr;
            }
            catch (Exception ex)
            {
                //var st = new StackTrace(ex, true);
                //var frame = st.GetFrame(0);
                //var line = frame.GetFileLineNumber();

                cr.Result = CompileAndExecuteInfo.eStatus.Incorrect;
                cr.Message = ex.InnerException.Message;
                return cr;
            }
        }
    }
}
