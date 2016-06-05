namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyLodSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken LodKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyLodSyntax(SyntaxToken lodKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyLod)
        {
            RegisterChildNode(out LodKeyword, lodKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyLod(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyLod(this);
        }
    }
}