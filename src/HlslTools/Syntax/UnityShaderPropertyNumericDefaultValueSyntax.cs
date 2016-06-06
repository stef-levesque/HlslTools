namespace HlslTools.Syntax
{
    public sealed class UnityShaderPropertyNumericDefaultValueSyntax : UnityShaderPropertyDefaultValueSyntax
    {
        public readonly ExpressionSyntax Number;

        public UnityShaderPropertyNumericDefaultValueSyntax(ExpressionSyntax number)
            : base(SyntaxKind.UnityShaderPropertyNumericDefaultValue)
        {
            RegisterChildNode(out Number, number);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityShaderPropertyNumericDefaultValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityShaderPropertyNumericDefaultValue(this);
        }
    }
}