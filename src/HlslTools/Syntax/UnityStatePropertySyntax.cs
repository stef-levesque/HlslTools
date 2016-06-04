namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertySyntax : SyntaxNode
    {
        public readonly SyntaxToken PropertyNameKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertySyntax(SyntaxToken propertyNameKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStateProperty)
        {
            RegisterChildNode(out PropertyNameKeyword, propertyNameKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStateProperty(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStateProperty(this);
        }
    }
}