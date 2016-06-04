using System;
using System.IO;
using HlslTools.Text;

namespace HlslTools.Tests.Support
{
    public sealed class TestUnityFileSystem : IIncludeFileSystem
    {
        private readonly string _parentDirectory;

        public TestUnityFileSystem(string parentFile)
        {
            _parentDirectory = Path.GetDirectoryName(parentFile);
        }

        public SourceText GetInclude(string path)
        {
            var fullPath = Path.Combine(_parentDirectory, path);
            if (!File.Exists(fullPath))
            {
                // Try to find file in CGIncludes folder.
                var attemptedCgIncludePath = Path.Combine("Shaders/Unity/CGIncludes", path);

                if (!File.Exists(attemptedCgIncludePath))
                    throw new Exception($"Could not find file at either {fullPath} or {attemptedCgIncludePath}.");

                fullPath = attemptedCgIncludePath;
            }

            return new StringText(File.ReadAllText(fullPath), path);
        }
    }
}