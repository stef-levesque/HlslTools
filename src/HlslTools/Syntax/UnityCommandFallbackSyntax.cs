namespace HlslTools.Syntax
{
    public sealed class UnityCommandFallbackSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken FallbackKeyword;
        public readonly SyntaxToken ValueToken;

        public UnityCommandFallbackSyntax(SyntaxToken fallbackKeyword, SyntaxToken valueToken)
            : base(SyntaxKind.UnityCommandFallback)
        {
            RegisterChildNode(out FallbackKeyword, fallbackKeyword);
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandFallback(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandFallback(this);
        }
    }
}