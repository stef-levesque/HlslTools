namespace HlslTools.Syntax
{
    public sealed class UnityCommandStencilFailSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken FailKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandStencilFailSyntax(SyntaxToken failKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandStencilFail)
        {
            RegisterChildNode(out FailKeyword, failKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandStencilFail(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandStencilFail(this);
        }
    }
}