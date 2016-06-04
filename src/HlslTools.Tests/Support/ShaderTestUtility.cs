using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using HlslTools.Syntax;
using NUnit.Framework;

namespace HlslTools.Tests.Support
{
    public static class ShaderTestUtility
    {
        public static IEnumerable<TestCaseData> FindTestShaders(string rootFolder)
        {
            return FindTestShaders(rootFolder, ".hlsl", ".fx", ".vsh", ".psh");
        }

        private static IEnumerable<TestCaseData> FindTestShaders(string rootFolder, params string[] extensions)
        {
            return Directory.GetFiles(rootFolder, "*.*", SearchOption.AllDirectories)
                .Where(x =>
                {
                    var ext = Path.GetExtension(x).ToLower();
                    return extensions.Contains(ext);
                })
                .Select(x => new TestCaseData(x));
        }

        internal static IEnumerable<TestCaseData> GetTestShaders()
        {
            return FindTestShaders("Shaders");
        }

        internal static IEnumerable<TestCaseData> GetUnityTestShaders()
        {
            return FindTestShaders("Shaders", ".shader");
        }

        public static void CheckForParseErrors(SyntaxTree syntaxTree)
        {
            foreach (var diagnostic in syntaxTree.GetDiagnostics())
                Debug.WriteLine(diagnostic.ToString());
            Assert.That(syntaxTree.GetDiagnostics().Count(), Is.EqualTo(0));
        }
    }
}