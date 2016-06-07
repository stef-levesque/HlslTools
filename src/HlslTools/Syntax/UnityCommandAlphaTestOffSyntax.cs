namespace HlslTools.Syntax
{
    public sealed class UnityCommandAlphaTestOffSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken AlphaTestKeyword;
        public readonly SyntaxToken OffToken;

        public UnityCommandAlphaTestOffSyntax(SyntaxToken alphaTestKeyword, SyntaxToken offToken)
            : base(SyntaxKind.UnityCommandAlphaTestOff)
        {
            RegisterChildNode(out AlphaTestKeyword, alphaTestKeyword);
            RegisterChildNode(out OffToken, offToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandAlphaTestOff(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandAlphaTestOff(this);
        }
    }
}