namespace HlslTools.Syntax
{

    public sealed class UnityCommandSetTextureCombineMultiplyAlphaValueSyntax : BaseUnityCommandSetTextureCombineValueSyntax
    {
        public readonly UnityCommandSetTextureCombineSourceSyntax Source1;
        public readonly SyntaxToken AsteriskToken;
        public readonly UnityCommandSetTextureCombineSourceSyntax Source2;
        public readonly SyntaxToken PlusToken;
        public readonly UnityCommandSetTextureCombineSourceSyntax Source3;

        public UnityCommandSetTextureCombineMultiplyAlphaValueSyntax(UnityCommandSetTextureCombineSourceSyntax source1, SyntaxToken asteriskToken, UnityCommandSetTextureCombineSourceSyntax source2, SyntaxToken plusToken, UnityCommandSetTextureCombineSourceSyntax source3)
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