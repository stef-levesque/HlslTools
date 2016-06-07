namespace HlslTools.Syntax
{
    public sealed class UnityCommandStencilCompSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken CompKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandStencilCompSyntax(SyntaxToken compKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandStencilComp)
        {
            RegisterChildNode(out CompKeyword, compKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandStencilComp(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandStencilComp(this);
        }
    }
}