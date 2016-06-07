namespace HlslTools.Syntax
{
    public sealed class UnityCommandStencilPassSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken PassKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandStencilPassSyntax(SyntaxToken passKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandStencilPass)
        {
            RegisterChildNode(out PassKeyword, passKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandStencilPass(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandStencilPass(this);
        }
    }
}