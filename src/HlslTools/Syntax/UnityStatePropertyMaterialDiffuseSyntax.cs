namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyMaterialDiffuseSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken DiffuseKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyMaterialDiffuseSyntax(SyntaxToken diffuseKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyMaterialDiffuse)
        {
            RegisterChildNode(out DiffuseKeyword, diffuseKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyMaterialDiffuse(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyMaterialDiffuse(this);
        }
    }
}