namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyStencilPassSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken PassKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyStencilPassSyntax(SyntaxToken passKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyStencilPass)
        {
            RegisterChildNode(out PassKeyword, passKeyword);
            RegisterChildNode(out Value, value);
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