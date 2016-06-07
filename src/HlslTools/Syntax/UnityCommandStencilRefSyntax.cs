namespace HlslTools.Syntax
{
    public sealed class UnityCommandStencilRefSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken RefKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandStencilRefSyntax(SyntaxToken refKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandStencilRef)
        {
            RegisterChildNode(out RefKeyword, refKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandStencilRef(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandStencilRef(this);
        }
    }
}