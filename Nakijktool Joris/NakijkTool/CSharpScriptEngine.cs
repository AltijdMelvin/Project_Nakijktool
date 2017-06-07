using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace NakijkTool
{
    public class CSharpScriptEngine
    {
        private static ScriptState<object> scriptState = null;

        public static IEnumerable<string> Execute(string code, IEnumerable<MetadataReference> references, string testMethodName)
        {
            //ScriptOptions scriptOptions = ScriptOptions.Default;

            MetadataReference[] r = new MetadataReference[]
            {
                            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                            MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                            MetadataReference.CreateFromFile(typeof(NUnit.Framework.Assert).Assembly.Location)
            };


            ScriptOptions scriptOptions = ScriptOptions.Default;

            //Add reference to mscorlib
            var mscorlib = typeof(System.Object).Assembly;
            var systemCore = typeof(System.Linq.Enumerable).Assembly;
            var nunitLib = typeof(NUnit.Framework.Assert).Assembly;
            scriptOptions = scriptOptions.AddReferences(mscorlib, systemCore, nunitLib);
            //Add namespaces
            //scriptOptions = scriptOptions.WithImports("System");
            //scriptOptions = scriptOptions.WithImports("System.Linq");
            //scriptOptions = scriptOptions.WithImports("System.Collections.Generic");
            //scriptOptions = scriptOptions.WithImports("NUnit");
            //scriptOptions = scriptOptions.WithImports("NUnit.Framework");

            exceptions.Clear();

            var script = CSharpScript.Create(code, scriptOptions);
            var diags = script.Compile();

            var wwx = script.ContinueWith($"new QuestionAndTest().{testMethodName}()", scriptOptions);

            //scriptState = CSharpScript.RunAsync(code, scriptOptions).Result;
            //scriptState = scriptState.ContinueWithAsync($"new QuestionAndTest().{testMethodName}()", scriptOptions, CatchException).Result;

            return exceptions.Select(x => x.Message).ToList().AsEnumerable();

            //if (scriptState.ReturnValue != null && !string.IsNullOrEmpty(scriptState.ReturnValue.ToString()))
            //    return scriptState.ReturnValue;
            //return null;
        }

        static List<Exception> exceptions = new List<Exception>();
        private static bool CatchException(Exception exception)
        {
            exceptions.Add(exception);
            return true;
        }
    }
}
