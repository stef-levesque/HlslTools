namespace HlslTools.Syntax
{
    public sealed class UnityCommandVariableValueSyntax : UnityCommandValueSyntax
    {
        public readonly SyntaxToken OpenBracketToken;
        public readonly SyntaxToken Identifier;
        public readonly SyntaxToken CloseBracketToken;

        public UnityCommandVariableValueSyntax(SyntaxToken openBracketToken, SyntaxToken identifier, SyntaxToken closeBracketToken)
            : base(SyntaxKind.UnityCommandVariableValue)
        {
            RegisterChildNode(out OpenBracketToken, openBracketToken);
            RegisterChildNode(out Identifier, identifier);
            RegisterChildNode(out CloseBracketToken, closeBracketToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandVariableValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandVariableValue(this);
        }
    }
}