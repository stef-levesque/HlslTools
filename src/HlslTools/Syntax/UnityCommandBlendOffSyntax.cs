namespace HlslTools.Syntax
{
    public sealed class UnityCommandBlendOffSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken BlendKeyword;
        public readonly SyntaxToken OffIdentifier;

        public UnityCommandBlendOffSyntax(SyntaxToken blendKeyword, SyntaxToken offIdentifier)
            : base(SyntaxKind.UnityCommandBlendOff)
        {
            RegisterChildNode(out BlendKeyword, blendKeyword);
            RegisterChildNode(out OffIdentifier, offIdentifier);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandBlendOff(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandBlendOff(this);
        }
    }
}