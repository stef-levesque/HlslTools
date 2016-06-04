namespace HlslTools.Syntax
{
    public sealed class UnityShaderPropertyNumericDefaultValueSyntax : UnityShaderPropertyDefaultValueSyntax
    {
        public readonly SyntaxToken NumberToken;

        public UnityShaderPropertyNumericDefaultValueSyntax(SyntaxToken numberToken)
            : base(SyntaxKind.UnityShaderPropertyNumericDefaultValue)
        {
            RegisterChildNode(out NumberToken, numberToken);
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