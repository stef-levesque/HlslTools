namespace HlslTools.Syntax
{
    public sealed class UnityCommandLodSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken LodKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandLodSyntax(SyntaxToken lodKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandLod)
        {
            RegisterChildNode(out LodKeyword, lodKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandLod(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandLod(this);
        }
    }
}