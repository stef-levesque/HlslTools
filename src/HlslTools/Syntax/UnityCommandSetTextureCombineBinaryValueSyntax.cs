namespace HlslTools.Syntax
{
    public sealed class UnityCommandSetTextureCombineBinaryValueSyntax : BaseUnityCommandSetTextureCombineValueSyntax
    {
        public readonly UnityCommandSetTextureCombineSourceSyntax Source1;
        public readonly SyntaxToken OperatorToken;
        public readonly UnityCommandSetTextureCombineSourceSyntax Source2;

        public UnityCommandSetTextureCombineBinaryValueSyntax(UnityCommandSetTextureCombineSourceSyntax source1, SyntaxToken operatorToken, UnityCommandSetTextureCombineSourceSyntax source2)
            : base(SyntaxKind.UnityCommandSetTextureCombineBinaryValue)
        {
            RegisterChildNode(out Source1, source1);
            RegisterChildNode(out OperatorToken, operatorToken);
            RegisterChildNode(out Source2, source2);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandSetTextureCombineBinaryValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandSetTextureCombineBinaryValue(this);
        }
    }
}