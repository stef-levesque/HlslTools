namespace HlslTools.Syntax
{
    public sealed class UnityCommandStencilZFailSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken ZFailKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandStencilZFailSyntax(SyntaxToken zFailKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandStencilZFail)
        {
            RegisterChildNode(out ZFailKeyword, zFailKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandStencilZFail(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandStencilZFail(this);
        }
    }
}