namespace HlslTools.Syntax
{
    public sealed class UnityShaderPropertyAttributeSyntax : SyntaxNode
    {
        public readonly SyntaxToken OpenBracketToken;
        public readonly SyntaxToken Name;
        public readonly AttributeArgumentListSyntax ArgumentList;
        public readonly SyntaxToken CloseBracketToken;

        public UnityShaderPropertyAttributeSyntax(SyntaxToken openBracketToken, SyntaxToken name, AttributeArgumentListSyntax argumentList, SyntaxToken closeBracketToken)
            : base(SyntaxKind.UnityShaderPropertyAttribute)
        {
            RegisterChildNode(out OpenBracketToken, openBracketToken);
            RegisterChildNode(out Name, name);
            RegisterChildNode(out ArgumentList, argumentList);
            RegisterChildNode(out CloseBracketToken, closeBracketToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityShaderPropertyAttribute(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityShaderPropertyAttribute(this);
        }
    }
}