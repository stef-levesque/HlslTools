namespace HlslTools.Syntax
{
    public sealed class UnityShaderPropertyRangeTypeSyntax : UnityShaderPropertyTypeSyntax
    {
        public readonly SyntaxToken RangeKeyword;
        public readonly SyntaxToken OpenParenToken;
        public readonly ExpressionSyntax MinValue;
        public readonly SyntaxToken CommaToken;
        public readonly ExpressionSyntax MaxValue;
        public readonly SyntaxToken CloseParenToken;

        public override SyntaxKind TypeKind => SyntaxKind.UnityRangeKeyword;

        public UnityShaderPropertyRangeTypeSyntax(SyntaxToken rangeKeyword, SyntaxToken openParenToken, ExpressionSyntax minValue, SyntaxToken commaToken, ExpressionSyntax maxValue, SyntaxToken closeParenToken)
            : base(SyntaxKind.UnityShaderPropertyRangeType)
        {
            RegisterChildNode(out RangeKeyword, rangeKeyword);
            RegisterChildNode(out OpenParenToken, openParenToken);
            RegisterChildNode(out MinValue, minValue);
            RegisterChildNode(out CommaToken, commaToken);
            RegisterChildNode(out MaxValue, maxValue);
            RegisterChildNode(out CloseParenToken, closeParenToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityShaderPropertyRangeType(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityShaderPropertyRangeType(this);
        }
    }
}