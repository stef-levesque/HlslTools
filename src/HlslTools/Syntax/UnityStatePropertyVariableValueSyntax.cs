namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyVariableValueSyntax : UnityStatePropertyValueSyntax
    {
        public readonly SyntaxToken OpenBracketToken;
        public readonly SyntaxToken Identifier;
        public readonly SyntaxToken CloseBracketToken;

        public UnityStatePropertyVariableValueSyntax(SyntaxToken openBracketToken, SyntaxToken identifier, SyntaxToken closeBracketToken)
            : base(SyntaxKind.UnityStatePropertyVariableValue)
        {
            RegisterChildNode(out OpenBracketToken, openBracketToken);
            RegisterChildNode(out Identifier, identifier);
            RegisterChildNode(out CloseBracketToken, closeBracketToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyVariableValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyVariableValue(this);
        }
    }
}