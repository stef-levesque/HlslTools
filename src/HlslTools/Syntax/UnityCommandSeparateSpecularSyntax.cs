namespace HlslTools.Syntax
{
    public sealed class UnityCommandSeparateSpecularSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken SeparateSpecularKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandSeparateSpecularSyntax(SyntaxToken separateSpecularKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandSeparateSpecular)
        {
            RegisterChildNode(out SeparateSpecularKeyword, separateSpecularKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandSeparateSpecular(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandSeparateSpecular(this);
        }
    }
}