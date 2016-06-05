namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyNameSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken NameKeyword;
        public readonly SyntaxToken NameToken;

        public UnityStatePropertyNameSyntax(SyntaxToken nameKeyword, SyntaxToken nameToken)
            : base(SyntaxKind.UnityStatePropertyName)
        {
            RegisterChildNode(out NameKeyword, nameKeyword);
            RegisterChildNode(out NameToken, nameToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyName(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyName(this);
        }
    }
}