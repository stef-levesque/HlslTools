namespace HlslTools.Syntax
{
    public sealed class UnityCommandBlendColorAlphaSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken BlendKeyword;
        public readonly UnityCommandValueSyntax SrcFactor;
        public readonly UnityCommandValueSyntax DstFactor;
        public readonly SyntaxToken CommaToken;
        public readonly UnityCommandValueSyntax SrcFactorA;
        public readonly UnityCommandValueSyntax DstFactorA;

        public UnityCommandBlendColorAlphaSyntax(SyntaxToken blendKeyword, UnityCommandValueSyntax srcFactor, UnityCommandValueSyntax dstFactor, SyntaxToken commaToken, UnityCommandValueSyntax srcFactorA, UnityCommandValueSyntax dstFactorA)
            : base(SyntaxKind.UnityCommandBlendColorAlpha)
        {
            RegisterChildNode(out BlendKeyword, blendKeyword);
            RegisterChildNode(out SrcFactor, srcFactor);
            RegisterChildNode(out DstFactor, dstFactor);
            RegisterChildNode(out CommaToken, commaToken);
            RegisterChildNode(out SrcFactorA, srcFactorA);
            RegisterChildNode(out DstFactorA, dstFactorA);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandBlendColorAlpha(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandBlendColorAlpha(this);
        }
    }
}