namespace HlslTools.Syntax
{
    public sealed class UnityCommandLightingSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken LightingKeyword;
        public readonly SyntaxToken ValueToken;

        public UnityCommandLightingSyntax(SyntaxToken lightingKeyword, SyntaxToken valueToken)
            : base(SyntaxKind.UnityCommandLighting)
        {
            RegisterChildNode(out LightingKeyword, lightingKeyword);
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandLighting(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandLighting(this);
        }
    }
}