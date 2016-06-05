namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyStencilFailSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken FailKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyStencilFailSyntax(SyntaxToken failKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyStencilFail)
        {
            RegisterChildNode(out FailKeyword, failKeyword);
            RegisterChildNode(out Value, value);
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