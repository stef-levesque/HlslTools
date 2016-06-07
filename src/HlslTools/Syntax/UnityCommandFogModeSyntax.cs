namespace HlslTools.Syntax
{
    public sealed class UnityCommandFogModeSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken ModeKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandFogModeSyntax(SyntaxToken modeKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandFogMode)
        {
            RegisterChildNode(out ModeKeyword, modeKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandFogMode(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandFogMode(this);
        }
    }
}
