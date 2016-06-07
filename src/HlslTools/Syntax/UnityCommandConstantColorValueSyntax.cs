namespace HlslTools.Syntax
{
    public sealed class UnityCommandConstantColorValueSyntax : UnityCommandValueSyntax
    {
        public readonly SyntaxToken OpenParenToken;
        public readonly ExpressionSyntax R;
        public readonly SyntaxToken FirstCommaToken;
        public readonly ExpressionSyntax G;
        public readonly SyntaxToken SecondCommaToken;
        public readonly ExpressionSyntax B;
        public readonly SyntaxToken ThirdCommaToken;
        public readonly ExpressionSyntax A;
        public readonly SyntaxToken CloseParenToken;

        public UnityCommandConstantColorValueSyntax(SyntaxToken openParenToken, ExpressionSyntax r, SyntaxToken firstCommaToken, ExpressionSyntax g, SyntaxToken secondCommaToken, ExpressionSyntax b, SyntaxToken thirdCommaToken, ExpressionSyntax a, SyntaxToken closeParenToken)
            : base(SyntaxKind.UnityCommandConstantColorValue)
        {
            RegisterChildNode(out OpenParenToken, openParenToken);
            RegisterChildNode(out R, r);
            RegisterChildNode(out FirstCommaToken, firstCommaToken);
            RegisterChildNode(out G, g);
            RegisterChildNode(out SecondCommaToken, secondCommaToken);
            RegisterChildNode(out B, b);
            RegisterChildNode(out ThirdCommaToken, thirdCommaToken);
            RegisterChildNode(out A, a);
            RegisterChildNode(out CloseParenToken, closeParenToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandConstantColorValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandConstantColorValue(this);
        }
    }
}