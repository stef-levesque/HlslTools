namespace HlslTools.Syntax
{
    public sealed class UnityCommandAlphaTestComparisonSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken AlphaTestKeyword;
        public readonly SyntaxToken Comparison;
        public readonly UnityCommandValueSyntax AlphaValue;

        public UnityCommandAlphaTestComparisonSyntax(SyntaxToken alphaTestKeyword, SyntaxToken comparison, UnityCommandValueSyntax alphaValue)
            : base(SyntaxKind.UnityCommandAlphaTestComparison)
        {
            RegisterChildNode(out AlphaTestKeyword, alphaTestKeyword);
            RegisterChildNode(out Comparison, comparison);
            RegisterChildNode(out AlphaValue, alphaValue);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandAlphaTestComparison(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandAlphaTestComparison(this);
        }
    }
}