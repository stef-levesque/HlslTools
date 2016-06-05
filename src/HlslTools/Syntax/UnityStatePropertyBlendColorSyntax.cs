namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyBlendColorSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken BlendKeyword;
        public readonly UnityStatePropertyValueSyntax SrcFactor;
        public readonly UnityStatePropertyValueSyntax DstFactor;

        public UnityStatePropertyBlendColorSyntax(SyntaxToken blendKeyword, UnityStatePropertyValueSyntax srcFactor, UnityStatePropertyValueSyntax dstFactor)
            : base(SyntaxKind.UnityStatePropertyBlendColor)
        {
            RegisterChildNode(out BlendKeyword, blendKeyword);
            RegisterChildNode(out SrcFactor, srcFactor);
            RegisterChildNode(out DstFactor, dstFactor);
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