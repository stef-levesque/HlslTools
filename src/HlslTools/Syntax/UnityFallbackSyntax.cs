namespace HlslTools.Syntax
{
    public sealed class UnityFallbackSyntax : UnityStateSyntax
    {
        public readonly SyntaxToken FallbackKeyword;
        public readonly SyntaxToken Value;

        public UnityFallbackSyntax(SyntaxToken fallbackKeyword, SyntaxToken value)
            : base(SyntaxKind.UnityFallback)
        {
            RegisterChildNode(out FallbackKeyword, fallbackKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityFallback(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityFallback(this);
        }
    }
}