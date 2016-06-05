namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyStencilWriteMaskSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken WriteMaskKeyword;
        public readonly SyntaxToken ValueToken;

        public UnityStatePropertyStencilWriteMaskSyntax(SyntaxToken writeMaskKeyword, SyntaxToken valueToken)
            : base(SyntaxKind.UnityStatePropertyStencilWriteMask)
        {
            RegisterChildNode(out WriteMaskKeyword, writeMaskKeyword);
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyStencilWriteMask(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyStencilWriteMask(this);
        }
    }
}