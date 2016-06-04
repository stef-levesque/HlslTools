namespace HlslTools.Syntax
{
    public sealed class UnityShaderPropertySimpleTypeSyntax : UnityShaderPropertyTypeSyntax
    {
        public readonly SyntaxToken TypeKeyword;

        public override SyntaxKind TypeKind => TypeKeyword.Kind;

        public UnityShaderPropertySimpleTypeSyntax(SyntaxToken typeKeyword)
            : base(SyntaxKind.UnityShaderPropertySimpleType)
        {
            RegisterChildNode(out TypeKeyword, typeKeyword);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityShaderPropertySimpleType(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityShaderPropertySimpleType(this);
        }
    }
}