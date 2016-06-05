namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyStencilWriteMaskSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken WriteMaskKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyStencilWriteMaskSyntax(SyntaxToken writeMaskKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyStencilWriteMask)
        {
            RegisterChildNode(out WriteMaskKeyword, writeMaskKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyStencilWriteMask(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyStencilWriteMask(this);
        }
    }
}