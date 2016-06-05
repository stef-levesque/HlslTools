namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyFallbackSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken FallbackKeyword;
        public readonly SyntaxToken ValueToken;

        public UnityStatePropertyFallbackSyntax(SyntaxToken fallbackKeyword, SyntaxToken valueToken)
            : base(SyntaxKind.UnityStatePropertyFallback)
        {
            RegisterChildNode(out FallbackKeyword, fallbackKeyword);
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyFallback(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyFallback(this);
        }
    }
}