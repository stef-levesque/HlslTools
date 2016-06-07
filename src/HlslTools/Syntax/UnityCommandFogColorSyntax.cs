namespace HlslTools.Syntax
{
    public sealed class UnityCommandFogColorSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken ColorKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandFogColorSyntax(SyntaxToken colorKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandFogColor)
        {
            RegisterChildNode(out ColorKeyword, colorKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandFogColor(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandFogColor(this);
        }
    }
}
