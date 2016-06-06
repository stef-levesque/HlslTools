using System.IO;
using HlslTools.Syntax;
using HlslTools.Tests.Support;
using HlslTools.Text;
using NUnit.Framework;

namespace HlslTools.Tests.Parser
{
    [TestFixture]
    public class RoundtrippingTests
    {
        [TestCaseSource(typeof(ShaderTestUtility), nameof(ShaderTestUtility.GetTestShaders))]
        public void CanBuildSyntaxTree(string testFile)
        {
            var sourceCode = File.ReadAllText(testFile);

            // Build syntax tree.
            var syntaxTree = SyntaxFactory.ParseSyntaxTree(SourceText.From(sourceCode), fileSystem: new TestFileSystem(testFile));

            ShaderTestUtility.CheckForParseErrors(syntaxTree);

            // Check roundtripping.
            var roundtrippedText = syntaxTree.Root.ToFullString();
            Assert.That(roundtrippedText, Is.EqualTo(sourceCode));
        }

        [TestCaseSource(typeof(ShaderTestUtility), nameof(ShaderTestUtility.GetUnityTestShaders))]
        public void CanBuildUnitySyntaxTree(string testFile)
        {
            var sourceCode = File.ReadAllText(testFile);

            // Build syntax tree.
            var syntaxTree = SyntaxFactory.ParseUnitySyntaxTree(
                SourceText.From(sourceCode), 
                fileSystem: new TestFileSystem(testFile, "Shaders/Unity/CGIncludes"));

            ShaderTestUtility.CheckForParseErrors(syntaxTree);

            // Check roundtripping.
            var roundtrippedText = syntaxTree.Root.ToFullString();
            Assert.That(roundtrippedText, Is.EqualTo(sourceCode));
        }
    }
}