namespace HlslTools.Syntax
{
    public sealed class UnityShaderTagSyntax : SyntaxNode
    {
        public readonly SyntaxToken NameToken;
        public readonly SyntaxToken EqualsToken;
        public readonly SyntaxToken ValueToken;

        public UnityShaderTagSyntax(SyntaxToken nameToken, SyntaxToken equalsToken, SyntaxToken valueToken)
            : base(SyntaxKind.UnityShaderTag)
        {
            RegisterChildNode(out NameToken, nameToken);
            RegisterChildNode(out EqualsToken, equalsToken);
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityShaderTag(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityShaderTag(this);
        }
    }
}