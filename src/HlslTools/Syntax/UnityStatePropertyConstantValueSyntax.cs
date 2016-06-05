namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyConstantValueSyntax : UnityStatePropertyValueSyntax
    {
        public readonly SyntaxToken ValueToken;

        public UnityStatePropertyConstantValueSyntax(SyntaxToken valueToken)
            : base(SyntaxKind.UnityStatePropertyConstantValue)
        {
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyConstantValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyConstantValue(this);
        }
    }
}