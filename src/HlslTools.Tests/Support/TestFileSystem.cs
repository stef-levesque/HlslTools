using System.IO;
using System.Linq;
using HlslTools.Text;

namespace HlslTools.Tests.Support
{
    public sealed class TestFileSystem : IIncludeFileSystem
    {
        private readonly string[] _includePaths;

        public TestFileSystem(string parentFile, params string[] additionalIncludePaths)
        {
            _includePaths = new[] { Path.GetDirectoryName(parentFile) }
                .Union(additionalIncludePaths)
                .ToArray();
        }

        public SourceText GetInclude(string path)
        {
            foreach (var includePath in _includePaths)
            {
                var fullPath = Path.Combine(includePath, path);
                if (File.Exists(fullPath))
                    return new StringText(File.ReadAllText(fullPath), path);
            }
            return null;
        }
    }
}