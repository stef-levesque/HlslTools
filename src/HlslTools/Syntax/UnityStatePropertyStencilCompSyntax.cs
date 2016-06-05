namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyStencilCompSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken CompKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyStencilCompSyntax(SyntaxToken compKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyStencilComp)
        {
            RegisterChildNode(out CompKeyword, compKeyword);
            RegisterChildNode(out Value, value);
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