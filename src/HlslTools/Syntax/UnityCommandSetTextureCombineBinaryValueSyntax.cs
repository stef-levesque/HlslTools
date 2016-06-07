namespace HlslTools.Syntax
{
    public sealed class UnityCommandSetTextureCombineBinaryValueSyntax : BaseUnityCommandSetTextureCombineValueSyntax
    {
        public readonly SyntaxToken Source1;
        public readonly SyntaxToken OperatorToken;
        public readonly SyntaxToken Source2;

        public UnityCommandSetTextureCombineBinaryValueSyntax(SyntaxToken source1, SyntaxToken operatorToken, SyntaxToken source2)
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