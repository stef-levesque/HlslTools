namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyBlendColorSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken BlendKeyword;
        public readonly SyntaxToken SrcFactorToken;
        public readonly SyntaxToken DstFactorToken;

        public UnityStatePropertyBlendColorSyntax(SyntaxToken blendKeyword, SyntaxToken srcFactorToken, SyntaxToken dstFactorToken)
            : base(SyntaxKind.UnityStatePropertyBlendColor)
        {
            RegisterChildNode(out BlendKeyword, blendKeyword);
            RegisterChildNode(out SrcFactorToken, srcFactorToken);
            RegisterChildNode(out DstFactorToken, dstFactorToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyBlendColor(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyBlendColor(this);
        }
    }
}