namespace HlslTools.Syntax
{
    public sealed class UnityCommandStencilWriteMaskSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken WriteMaskKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandStencilWriteMaskSyntax(SyntaxToken writeMaskKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandStencilWriteMask)
        {
            RegisterChildNode(out WriteMaskKeyword, writeMaskKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandStencilWriteMask(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandStencilWriteMask(this);
        }
    }
}