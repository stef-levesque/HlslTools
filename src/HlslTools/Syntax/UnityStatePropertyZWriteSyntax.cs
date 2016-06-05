namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyZWriteSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken ZWriteKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyZWriteSyntax(SyntaxToken zWriteKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyZWrite)
        {
            RegisterChildNode(out ZWriteKeyword, zWriteKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyZWrite(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyZWrite(this);
        }
    }
}