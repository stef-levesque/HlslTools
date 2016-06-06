namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyMaterialAmbientSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken AmbientKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyMaterialAmbientSyntax(SyntaxToken ambientKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyMaterialAmbient)
        {
            RegisterChildNode(out AmbientKeyword, ambientKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyMaterialAmbient(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyMaterialAmbient(this);
        }
    }
}