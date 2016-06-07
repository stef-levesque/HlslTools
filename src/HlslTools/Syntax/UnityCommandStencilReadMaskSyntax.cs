namespace HlslTools.Syntax
{
    public sealed class UnityCommandStencilReadMaskSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken ReadMaskKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandStencilReadMaskSyntax(SyntaxToken readMaskKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandStencilReadMask)
        {
            RegisterChildNode(out ReadMaskKeyword, readMaskKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandStencilReadMask(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandStencilReadMask(this);
        }
    }
}