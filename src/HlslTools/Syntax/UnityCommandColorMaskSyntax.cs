namespace HlslTools.Syntax
{
    public sealed class UnityCommandColorMaskSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken ColorMaskKeyword;
        public readonly UnityCommandValueSyntax Mask;

        public UnityCommandColorMaskSyntax(SyntaxToken colorMaskKeyword, UnityCommandValueSyntax mask)
            : base(SyntaxKind.UnityCommandColorMask)
        {
            RegisterChildNode(out ColorMaskKeyword, colorMaskKeyword);
            RegisterChildNode(out Mask, mask);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandColorMask(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandColorMask(this);
        }
    }
}