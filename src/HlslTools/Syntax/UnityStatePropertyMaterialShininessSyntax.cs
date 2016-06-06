namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyMaterialShininessSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken ShininessKeyword;
        public readonly UnityStatePropertyValueSyntax Value;

        public UnityStatePropertyMaterialShininessSyntax(SyntaxToken shininessKeyword, UnityStatePropertyValueSyntax value)
            : base(SyntaxKind.UnityStatePropertyMaterialShininess)
        {
            RegisterChildNode(out ShininessKeyword, shininessKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyMaterialShininess(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyMaterialShininess(this);
        }
    }
}