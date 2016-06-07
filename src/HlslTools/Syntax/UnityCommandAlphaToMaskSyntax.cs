namespace HlslTools.Syntax
{
    public sealed class UnityCommandAlphaToMaskSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken AlphaToMaskKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandAlphaToMaskSyntax(SyntaxToken alphaToMaskKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandAlphaToMask)
        {
            RegisterChildNode(out AlphaToMaskKeyword, alphaToMaskKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandAlphaToMask(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandAlphaToMask(this);
        }
    }
}