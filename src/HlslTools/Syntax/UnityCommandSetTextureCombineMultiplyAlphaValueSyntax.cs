namespace HlslTools.Syntax
{

    public sealed class UnityCommandSetTextureCombineMultiplyAlphaValueSyntax : BaseUnityCommandSetTextureCombineValueSyntax
    {
        public readonly SyntaxToken Source1;
        public readonly SyntaxToken AsteriskToken;
        public readonly SyntaxToken Source2;
        public readonly SyntaxToken PlusToken;
        public readonly SyntaxToken Source3;

        public UnityCommandSetTextureCombineMultiplyAlphaValueSyntax(SyntaxToken source1, SyntaxToken asteriskToken, SyntaxToken source2, SyntaxToken plusToken, SyntaxToken source3)
            : base(SyntaxKind.UnityCommandSetTextureCombineMultiplyAlphaValue)
        {
            RegisterChildNode(out Source1, source1);
            RegisterChildNode(out AsteriskToken, asteriskToken);
            RegisterChildNode(out Source2, source2);
            RegisterChildNode(out PlusToken, plusToken);
            RegisterChildNode(out Source3, source3);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandSetTextureCombineMultiplyAlphaValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandSetTextureCombineMultiplyAlphaValue(this);
        }
    }
}