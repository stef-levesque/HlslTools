namespace HlslTools.Syntax
{
    public sealed class UnityCommandSetTextureCombineSourceSyntax : SyntaxNode
    {
        public readonly SyntaxToken SourceToken;
        public readonly SyntaxToken AlphaKeyword;

        public UnityCommandSetTextureCombineSourceSyntax(SyntaxToken sourceToken, SyntaxToken alphaKeyword)
            : base(SyntaxKind.UnityCommandSetTextureCombineSource)
        {
            RegisterChildNode(out SourceToken, sourceToken);
            RegisterChildNode(out AlphaKeyword, alphaKeyword);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandSetTextureCombineSource(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandSetTextureCombineSource(this);
        }
    }
}