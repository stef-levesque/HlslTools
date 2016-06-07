namespace HlslTools.Syntax
{
    public sealed class UnityShaderPropertyVectorDefaultValueSyntax : UnityShaderPropertyDefaultValueSyntax
    {
        public readonly BaseUnityVectorSyntax Vector;

        public UnityShaderPropertyVectorDefaultValueSyntax(BaseUnityVectorSyntax vector)
            : base (SyntaxKind.UnityShaderPropertyVectorDefaultValue)
        {
            RegisterChildNode(out Vector, vector);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityShaderPropertyVectorDefaultValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityShaderPropertyVectorDefaultValue(this);
        }
    }
}