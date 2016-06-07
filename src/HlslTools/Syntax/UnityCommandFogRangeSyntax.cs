namespace HlslTools.Syntax
{
    public sealed class UnityCommandFogRangeSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken RangeKeyword;
        public readonly UnityCommandValueSyntax NearValue;
        public readonly SyntaxToken CommaToken;
        public readonly UnityCommandValueSyntax FarValue;

        public UnityCommandFogRangeSyntax(SyntaxToken rangeKeyword, UnityCommandValueSyntax nearValue, SyntaxToken commaToken, UnityCommandValueSyntax farValue)
            : base(SyntaxKind.UnityCommandFogRange)
        {
            RegisterChildNode(out RangeKeyword, rangeKeyword);
            RegisterChildNode(out NearValue, nearValue);
            RegisterChildNode(out CommaToken, commaToken);
            RegisterChildNode(out FarValue, farValue);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandFogRange(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandFogRange(this);
        }
    }
}
