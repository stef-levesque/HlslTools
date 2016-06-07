namespace HlslTools.Syntax
{
    public sealed class UnityCommandMaterialSpecularSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken SpecularKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandMaterialSpecularSyntax(SyntaxToken specularKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandMaterialSpecular)
        {
            RegisterChildNode(out SpecularKeyword, specularKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandMaterialSpecular(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandMaterialSpecular(this);
        }
    }
}