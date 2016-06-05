namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyOffsetSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken OffsetKeyword;
        public readonly UnityStatePropertyValueSyntax Factor;
        public readonly SyntaxToken CommaToken;
        public readonly UnityStatePropertyValueSyntax Units;

        public UnityStatePropertyOffsetSyntax(SyntaxToken offsetKeyword, UnityStatePropertyValueSyntax factor, SyntaxToken commaToken, UnityStatePropertyValueSyntax units)
            : base(SyntaxKind.UnityStatePropertyOffset)
        {
            RegisterChildNode(out OffsetKeyword, offsetKeyword);
            RegisterChildNode(out Factor, factor);
            RegisterChildNode(out CommaToken, commaToken);
            RegisterChildNode(out Units, units);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyOffset(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyOffset(this);
        }
    }
}