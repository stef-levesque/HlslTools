namespace HlslTools.Syntax
{
    public sealed class UnityCommandZTestSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken ZTestKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandZTestSyntax(SyntaxToken zTestKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandZTest)
        {
            RegisterChildNode(out ZTestKeyword, zTestKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandZTest(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandZTest(this);
        }
    }
}