namespace HlslTools.Syntax
{
    public sealed class UnityCommandMaterialDiffuseSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken DiffuseKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandMaterialDiffuseSyntax(SyntaxToken diffuseKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandMaterialDiffuse)
        {
            RegisterChildNode(out DiffuseKeyword, diffuseKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandMaterialDiffuse(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandMaterialDiffuse(this);
        }
    }
}