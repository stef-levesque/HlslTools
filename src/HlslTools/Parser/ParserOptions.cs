using System.Collections.Generic;

namespace HlslTools.Parser
{
    public sealed class ParserOptions
    {
        public List<string> PreprocessorDefines { get; } = new List<string>();
        public List<string> AdditionalIncludeDirectories { get; } = new List<string>();
    }
}