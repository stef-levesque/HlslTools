namespace HlslTools.Syntax
{
    public sealed class UnityCommandConstantColorValueSyntax : UnityCommandValueSyntax
    {
        public readonly UnityVectorSyntax Vector;

        public UnityCommandConstantColorValueSyntax(UnityVectorSyntax vector)
            : base(SyntaxKind.UnityCommandConstantColorValue)
        {
            RegisterChildNode(out Vector, vector);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandConstantColorValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandConstantColorValue(this);
        }
    }
}