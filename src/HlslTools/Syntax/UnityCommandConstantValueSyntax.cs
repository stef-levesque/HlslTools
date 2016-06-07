namespace HlslTools.Syntax
{
    public sealed class UnityCommandConstantValueSyntax : UnityCommandValueSyntax
    {
        public readonly SyntaxToken ValueToken;

        public UnityCommandConstantValueSyntax(SyntaxToken valueToken)
            : base(SyntaxKind.UnityCommandConstantValue)
        {
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandConstantValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandConstantValue(this);
        }
    }
}