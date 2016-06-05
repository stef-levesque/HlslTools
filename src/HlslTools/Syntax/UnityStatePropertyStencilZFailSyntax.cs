namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyStencilZFailSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken ZFailKeyword;
        public readonly SyntaxToken ValueToken;

        public UnityStatePropertyStencilZFailSyntax(SyntaxToken zFailKeyword, SyntaxToken valueToken)
            : base(SyntaxKind.UnityStatePropertyStencilZFail)
        {
            RegisterChildNode(out ZFailKeyword, zFailKeyword);
            RegisterChildNode(out ValueToken, valueToken);
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