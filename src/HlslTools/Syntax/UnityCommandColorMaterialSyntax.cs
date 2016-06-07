namespace HlslTools.Syntax
{
    public sealed class UnityCommandColorMaterialSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken ColorMaterialKeyword;
        public readonly SyntaxToken Value;

        public UnityCommandColorMaterialSyntax(SyntaxToken colorMaterialKeyword, SyntaxToken value)
            : base(SyntaxKind.UnityCommandColorMaterial)
        {
            RegisterChildNode(out ColorMaterialKeyword, colorMaterialKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandColorMaterial(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandColorMaterial(this);
        }
    }
}