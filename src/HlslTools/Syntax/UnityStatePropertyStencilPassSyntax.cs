namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyStencilPassSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken PassKeyword;
        public readonly SyntaxToken ValueToken;

        public UnityStatePropertyStencilPassSyntax(SyntaxToken passKeyword, SyntaxToken valueToken)
            : base(SyntaxKind.UnityStatePropertyStencilPass)
        {
            RegisterChildNode(out PassKeyword, passKeyword);
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyStencilPass(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyStencilPass(this);
        }
    }
}