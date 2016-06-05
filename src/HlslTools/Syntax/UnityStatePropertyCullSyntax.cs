namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyCullSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken CullKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyCullSyntax(SyntaxToken cullKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyCull)
        {
            RegisterChildNode(out CullKeyword, cullKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyCull(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyCull(this);
        }
    }
}