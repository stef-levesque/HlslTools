namespace HlslTools.Syntax
{
    public sealed class UnityCommandOffsetSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken OffsetKeyword;
        public readonly UnityCommandValueSyntax Factor;
        public readonly SyntaxToken CommaToken;
        public readonly UnityCommandValueSyntax Units;

        public UnityCommandOffsetSyntax(SyntaxToken offsetKeyword, UnityCommandValueSyntax factor, SyntaxToken commaToken, UnityCommandValueSyntax units)
            : base(SyntaxKind.UnityCommandOffset)
        {
            RegisterChildNode(out OffsetKeyword, offsetKeyword);
            RegisterChildNode(out Factor, factor);
            RegisterChildNode(out CommaToken, commaToken);
            RegisterChildNode(out Units, units);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandOffset(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandOffset(this);
        }
    }
}