namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyStencilZFailSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken ZFailKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyStencilZFailSyntax(SyntaxToken zFailKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyStencilZFail)
        {
            RegisterChildNode(out ZFailKeyword, zFailKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyStencilZFail(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyStencilZFail(this);
        }
    }
}