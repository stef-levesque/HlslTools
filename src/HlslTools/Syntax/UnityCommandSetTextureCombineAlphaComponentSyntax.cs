namespace HlslTools.Syntax
{
    public sealed class UnityCommandSetTextureCombineAlphaComponentSyntax : SyntaxNode
    {
        public readonly SyntaxToken CommaToken;
        public readonly BaseUnityCommandSetTextureCombineValueSyntax Value;

        public UnityCommandSetTextureCombineAlphaComponentSyntax(SyntaxToken commaToken, BaseUnityCommandSetTextureCombineValueSyntax value)
            : base(SyntaxKind.UnityCommandSetTextureCombineAlphaComponent)
        {
            RegisterChildNode(out CommaToken, commaToken);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandSetTextureCombineAlphaComponent(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandSetTextureCombineAlphaComponent(this);
        }
    }
}