namespace HlslTools.Syntax
{
    public sealed class UnityShaderPropertyAttributeSyntax : SyntaxNode
    {
        public readonly SyntaxToken OpenBracketToken;
        public readonly SyntaxToken Name;
        public readonly SyntaxToken CloseBracketToken;

        public UnityShaderPropertyAttributeSyntax(SyntaxToken openBracketToken, SyntaxToken name, SyntaxToken closeBracketToken)
            : base(SyntaxKind.UnityShaderPropertyAttribute)
        {
            RegisterChildNode(out OpenBracketToken, openBracketToken);
            RegisterChildNode(out Name, name);
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