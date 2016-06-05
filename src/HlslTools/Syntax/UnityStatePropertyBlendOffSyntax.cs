namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyBlendOffSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken BlendKeyword;
        public readonly SyntaxToken OffIdentifier;

        public UnityStatePropertyBlendOffSyntax(SyntaxToken blendKeyword, SyntaxToken offIdentifier)
            : base(SyntaxKind.UnityStatePropertyBlendOff)
        {
            RegisterChildNode(out BlendKeyword, blendKeyword);
            RegisterChildNode(out OffIdentifier, offIdentifier);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyBlendOff(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyBlendOff(this);
        }
    }
}