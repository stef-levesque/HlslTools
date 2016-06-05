namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyBlendColorAlphaSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken BlendKeyword;
        public readonly UnityStatePropertyValueSyntax SrcFactor;
        public readonly UnityStatePropertyValueSyntax DstFactor;
        public readonly SyntaxToken CommaToken;
        public readonly UnityStatePropertyValueSyntax SrcFactorA;
        public readonly UnityStatePropertyValueSyntax DstFactorA;

        public UnityStatePropertyBlendColorAlphaSyntax(SyntaxToken blendKeyword, UnityStatePropertyValueSyntax srcFactor, UnityStatePropertyValueSyntax dstFactor, SyntaxToken commaToken, UnityStatePropertyValueSyntax srcFactorA, UnityStatePropertyValueSyntax dstFactorA)
            : base(SyntaxKind.UnityStatePropertyBlendColorAlpha)
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
            visitor.VisitUnityStatePropertyBlendColorAlpha(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyBlendColorAlpha(this);
        }
    }
}