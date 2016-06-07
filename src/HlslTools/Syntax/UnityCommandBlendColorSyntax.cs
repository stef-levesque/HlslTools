namespace HlslTools.Syntax
{
    public sealed class UnityCommandBlendColorSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken BlendKeyword;
        public readonly UnityCommandValueSyntax SrcFactor;
        public readonly UnityCommandValueSyntax DstFactor;

        public UnityCommandBlendColorSyntax(SyntaxToken blendKeyword, UnityCommandValueSyntax srcFactor, UnityCommandValueSyntax dstFactor)
            : base(SyntaxKind.UnityCommandBlendColor)
        {
            RegisterChildNode(out BlendKeyword, blendKeyword);
            RegisterChildNode(out SrcFactor, srcFactor);
            RegisterChildNode(out DstFactor, dstFactor);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandBlendColor(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandBlendColor(this);
        }
    }
}