namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyMaterialSpecularSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken SpecularKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyMaterialSpecularSyntax(SyntaxToken specularKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyMaterialSpecular)
        {
            RegisterChildNode(out SpecularKeyword, specularKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyMaterialSpecular(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyMaterialSpecular(this);
        }
    }
}