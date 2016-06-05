namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyStencilFailSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken FailKeyword;
        public readonly SyntaxToken ValueToken;

        public UnityStatePropertyStencilFailSyntax(SyntaxToken failKeyword, SyntaxToken valueToken)
            : base(SyntaxKind.UnityStatePropertyStencilFail)
        {
            RegisterChildNode(out FailKeyword, failKeyword);
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyStencilFail(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyStencilFail(this);
        }
    }
}