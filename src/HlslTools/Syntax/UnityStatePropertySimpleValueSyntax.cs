namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertySimpleValueSyntax : UnityStatePropertyValueSyntax
    {
        public readonly SyntaxToken ValueToken;

        public UnityStatePropertySimpleValueSyntax(SyntaxToken valueToken)
            : base(SyntaxKind.UnityStatePropertySimpleValue)
        {
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertySimpleValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertySimpleValue(this);
        }
    }
}