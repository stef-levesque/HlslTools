namespace HlslTools.Syntax
{
    public sealed class UnityCommandZWriteSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken ZWriteKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandZWriteSyntax(SyntaxToken zWriteKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandZWrite)
        {
            RegisterChildNode(out ZWriteKeyword, zWriteKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandZWrite(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandZWrite(this);
        }
    }
}