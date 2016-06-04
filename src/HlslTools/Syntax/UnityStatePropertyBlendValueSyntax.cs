namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyBlendValueSyntax : UnityStatePropertyValueSyntax
    {
        public readonly SyntaxToken SrcFactorToken;
        public readonly SyntaxToken DstFactorToken;

        public UnityStatePropertyBlendValueSyntax(SyntaxToken srcFactorToken, SyntaxToken dstFactorToken)
            : base(SyntaxKind.UnityStatePropertyBlendValue)
        {
            RegisterChildNode(out SrcFactorToken, srcFactorToken);
            RegisterChildNode(out DstFactorToken, dstFactorToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyBlendValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyBlendValue(this);
        }
    }
}