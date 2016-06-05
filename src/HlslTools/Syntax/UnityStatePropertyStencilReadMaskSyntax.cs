namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyStencilReadMaskSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken ReadMaskKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyStencilReadMaskSyntax(SyntaxToken readMaskKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyStencilReadMask)
        {
            RegisterChildNode(out ReadMaskKeyword, readMaskKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyStencilReadMask(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyStencilReadMask(this);
        }
    }
}