namespace HlslTools.Syntax
{
    public sealed class UnityCommandMaterialShininessSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken ShininessKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandMaterialShininessSyntax(SyntaxToken shininessKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandMaterialShininess)
        {
            RegisterChildNode(out ShininessKeyword, shininessKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandMaterialShininess(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandMaterialShininess(this);
        }
    }
}