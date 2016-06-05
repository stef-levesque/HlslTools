namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyStencilRefSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken RefKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyStencilRefSyntax(SyntaxToken refKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyStencilRef)
        {
            RegisterChildNode(out RefKeyword, refKeyword);
            RegisterChildNode(out Value, value);
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