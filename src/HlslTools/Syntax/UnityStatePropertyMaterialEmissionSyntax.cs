namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyMaterialEmissionSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken EmissionKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyMaterialEmissionSyntax(SyntaxToken emissionKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyMaterialEmission)
        {
            RegisterChildNode(out EmissionKeyword, emissionKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyMaterialEmission(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyMaterialEmission(this);
        }
    }
}