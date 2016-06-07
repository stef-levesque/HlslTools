namespace HlslTools.Syntax
{
    public sealed class UnityCommandFogDensitySyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken DensityKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandFogDensitySyntax(SyntaxToken densityKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandFogDensity)
        {
            RegisterChildNode(out DensityKeyword, densityKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandFogDensity(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandFogDensity(this);
        }
    }
}
