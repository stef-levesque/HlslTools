namespace HlslTools.Syntax
{
    public sealed class UnityCommandSetTextureCombineSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken CombineKeyword;
        public readonly BaseUnityCommandSetTextureCombineValueSyntax Value;
        public readonly SyntaxToken Modifier;
        public readonly UnityCommandSetTextureCombineAlphaComponentSyntax AlphaComponent;

        public UnityCommandSetTextureCombineSyntax(SyntaxToken combineKeyword, BaseUnityCommandSetTextureCombineValueSyntax value, SyntaxToken modifier, UnityCommandSetTextureCombineAlphaComponentSyntax alphaComponent)
            : base(SyntaxKind.UnityCommandSetTextureCombine)
        {
            RegisterChildNode(out CombineKeyword, combineKeyword);
            RegisterChildNode(out Value, value);
            RegisterChildNode(out Modifier, modifier);
            RegisterChildNode(out AlphaComponent, alphaComponent);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandSetTextureCombine(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandSetTextureCombine(this);
        }
    }
}