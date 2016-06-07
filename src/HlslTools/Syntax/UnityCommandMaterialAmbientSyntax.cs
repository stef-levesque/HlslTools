namespace HlslTools.Syntax
{
    public sealed class UnityCommandMaterialAmbientSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken AmbientKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandMaterialAmbientSyntax(SyntaxToken ambientKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandMaterialAmbient)
        {
            RegisterChildNode(out AmbientKeyword, ambientKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandMaterialAmbient(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandMaterialAmbient(this);
        }
    }
}