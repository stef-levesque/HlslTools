namespace HlslTools.Syntax
{
    public sealed class UnityCommandSetTextureCombineLerpValueSyntax : BaseUnityCommandSetTextureCombineValueSyntax
    {
        public readonly SyntaxToken Source1;
        public readonly SyntaxToken LerpKeyword;
        public readonly SyntaxToken OpenParenToken;
        public readonly SyntaxToken Source2;
        public readonly SyntaxToken CloseParenToken;
        public readonly SyntaxToken Source3;

        public UnityCommandSetTextureCombineLerpValueSyntax(SyntaxToken source1, SyntaxToken lerpKeyword, SyntaxToken openParenToken, SyntaxToken source2, SyntaxToken closeParenToken, SyntaxToken source3)
            : base(SyntaxKind.UnityCommandSetTextureCombineLerpValue)
        {
            RegisterChildNode(out Source1, source1);
            RegisterChildNode(out LerpKeyword, lerpKeyword);
            RegisterChildNode(out OpenParenToken, openParenToken);
            RegisterChildNode(out Source2, source2);
            RegisterChildNode(out CloseParenToken, closeParenToken);
            RegisterChildNode(out Source3, source3);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandSetTextureCombineLerpValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandSetTextureCombineLerpValue(this);
        }
    }
}