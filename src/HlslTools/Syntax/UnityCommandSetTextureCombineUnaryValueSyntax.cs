namespace HlslTools.Syntax
{
    public sealed class UnityCommandSetTextureCombineUnaryValueSyntax : BaseUnityCommandSetTextureCombineValueSyntax
    {
        public readonly UnityCommandSetTextureCombineSourceSyntax Source;

        public UnityCommandSetTextureCombineUnaryValueSyntax(UnityCommandSetTextureCombineSourceSyntax source)
            : base(SyntaxKind.UnityCommandSetTextureCombineUnaryValue)
        {
            RegisterChildNode(out Source, source);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandSetTextureCombineUnaryValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandSetTextureCombineUnaryValue(this);
        }
    }
}