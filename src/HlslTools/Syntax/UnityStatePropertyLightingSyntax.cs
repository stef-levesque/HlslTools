namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyLightingSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken LightingKeyword;
        public readonly SyntaxToken ValueToken;

        public UnityStatePropertyLightingSyntax(SyntaxToken lightingKeyword, SyntaxToken valueToken)
            : base(SyntaxKind.UnityStatePropertyLighting)
        {
            RegisterChildNode(out LightingKeyword, lightingKeyword);
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyLighting(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyLighting(this);
        }
    }
}