namespace HlslTools.Syntax
{
    public class UnityShaderPropertySyntax : SyntaxNode
    {
        public readonly IdentifierNameSyntax Name;

        // TODO

        public UnityShaderPropertySyntax(IdentifierNameSyntax name)
            : base(SyntaxKind.UnityShaderProperty)
        {
            RegisterChildNode(out Name, name);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityShaderProperty(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityShaderProperty(this);
        }
    }
}