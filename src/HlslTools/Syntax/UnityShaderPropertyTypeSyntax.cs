namespace HlslTools.Syntax
{
    public abstract class UnityShaderPropertyTypeSyntax : SyntaxNode
    {
        public abstract SyntaxKind TypeKind { get; }

        protected UnityShaderPropertyTypeSyntax(SyntaxKind kind)
            : base(kind)
        {
        }
    }
}