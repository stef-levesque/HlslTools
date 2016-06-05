namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyColorMaskSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken ColorMaskKeyword;
        public readonly UnityStatePropertyValueSyntax Mask;

        public UnityStatePropertyColorMaskSyntax(SyntaxToken colorMaskKeyword, UnityStatePropertyValueSyntax mask)
            : base(SyntaxKind.UnityStatePropertyColorMask)
        {
            RegisterChildNode(out ColorMaskKeyword, colorMaskKeyword);
            RegisterChildNode(out Mask, mask);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyColorMask(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyColorMask(this);
        }
    }
}