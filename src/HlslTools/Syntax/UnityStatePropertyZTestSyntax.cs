namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyZTestSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken ZTestKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyZTestSyntax(SyntaxToken zTestKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyZTest)
        {
            RegisterChildNode(out ZTestKeyword, zTestKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyZTest(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyZTest(this);
        }
    }
}