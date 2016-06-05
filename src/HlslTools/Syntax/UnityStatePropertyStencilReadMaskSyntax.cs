namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyStencilReadMaskSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken ReadMaskKeyword;
        public readonly SyntaxToken ValueToken;

        public UnityStatePropertyStencilReadMaskSyntax(SyntaxToken readMaskKeyword, SyntaxToken valueToken)
            : base(SyntaxKind.UnityStatePropertyStencilReadMask)
        {
            RegisterChildNode(out ReadMaskKeyword, readMaskKeyword);
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyStencilReadMask(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyStencilReadMask(this);
        }
    }
}