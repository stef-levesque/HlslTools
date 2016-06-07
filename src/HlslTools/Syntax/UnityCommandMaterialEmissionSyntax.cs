namespace HlslTools.Syntax
{
    public sealed class UnityCommandMaterialEmissionSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken EmissionKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandMaterialEmissionSyntax(SyntaxToken emissionKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandMaterialEmission)
        {
            RegisterChildNode(out EmissionKeyword, emissionKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandMaterialEmission(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandMaterialEmission(this);
        }
    }
}