namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyBlendColorAlphaSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken BlendKeyword;
        public readonly SyntaxToken SrcFactorToken;
        public readonly SyntaxToken DstFactorToken;
        public readonly SyntaxToken CommaToken;
        public readonly SyntaxToken SrcFactorAToken;
        public readonly SyntaxToken DstFactorAToken;

        public UnityStatePropertyBlendColorAlphaSyntax(SyntaxToken blendKeyword, SyntaxToken srcFactorToken, SyntaxToken dstFactorToken, SyntaxToken commaToken, SyntaxToken srcFactorAToken, SyntaxToken dstFactorAToken)
            : base(SyntaxKind.UnityStatePropertyBlendColorAlpha)
        {
            RegisterChildNode(out BlendKeyword, blendKeyword);
            RegisterChildNode(out SrcFactorToken, srcFactorToken);
            RegisterChildNode(out DstFactorToken, dstFactorToken);
            RegisterChildNode(out CommaToken, commaToken);
            RegisterChildNode(out SrcFactorAToken, srcFactorAToken);
            RegisterChildNode(out DstFactorAToken, dstFactorAToken);
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