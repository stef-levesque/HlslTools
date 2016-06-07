namespace HlslTools.Syntax
{
    public sealed class UnityCommandCullSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken CullKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandCullSyntax(SyntaxToken cullKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandCull)
        {
            RegisterChildNode(out CullKeyword, cullKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandCull(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandCull(this);
        }
    }
}