namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyStencilCompSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken CompKeyword;
        public readonly SyntaxToken ValueToken;

        public UnityStatePropertyStencilCompSyntax(SyntaxToken compKeyword, SyntaxToken valueToken)
            : base(SyntaxKind.UnityStatePropertyStencilComp)
        {
            RegisterChildNode(out CompKeyword, compKeyword);
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyStencilComp(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyStencilComp(this);
        }
    }
}