namespace HlslTools.Syntax
{
    public sealed class UnityCommandSetTextureCombineSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken CombineKeyword;
        public readonly BaseUnityCommandSetTextureCombineValueSyntax Value;
        public readonly SyntaxToken Modifier;

        public UnityCommandSetTextureCombineSyntax(SyntaxToken combineKeyword, BaseUnityCommandSetTextureCombineValueSyntax value, SyntaxToken modifier)
            : base(SyntaxKind.UnityCommandSetTextureCombine)
        {
            RegisterChildNode(out CombineKeyword, combineKeyword);
            RegisterChildNode(out Value, value);
            RegisterChildNode(out Modifier, modifier);
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