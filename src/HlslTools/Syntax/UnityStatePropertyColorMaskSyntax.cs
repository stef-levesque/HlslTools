namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyColorMaskSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken ColorMaskKeyword;
        public readonly SyntaxToken MaskToken;

        public UnityStatePropertyColorMaskSyntax(SyntaxToken colorMaskKeyword, SyntaxToken maskToken)
            : base(SyntaxKind.UnityStatePropertyColorMask)
        {
            RegisterChildNode(out ColorMaskKeyword, colorMaskKeyword);
            RegisterChildNode(out MaskToken, maskToken);
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