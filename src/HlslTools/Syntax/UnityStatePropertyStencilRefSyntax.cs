namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyStencilRefSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken RefKeyword;
        public readonly SyntaxToken ValueToken;

        public UnityStatePropertyStencilRefSyntax(SyntaxToken refKeyword, SyntaxToken valueToken)
            : base(SyntaxKind.UnityStatePropertyStencilRef)
        {
            RegisterChildNode(out RefKeyword, refKeyword);
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyStencilRef(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyStencilRef(this);
        }
    }
}