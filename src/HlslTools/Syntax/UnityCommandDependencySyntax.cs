namespace HlslTools.Syntax
{
    public sealed class UnityCommandDependencySyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken DependencyKeyword;
        public readonly SyntaxToken NameToken;
        public readonly SyntaxToken EqualsToken;
        public readonly SyntaxToken DependentShaderToken;

        public UnityCommandDependencySyntax(SyntaxToken dependencyKeyword, SyntaxToken nameToken, SyntaxToken equalsToken, SyntaxToken dependentShaderToken)
            : base(SyntaxKind.UnityCommandDependency)
        {
            RegisterChildNode(out DependencyKeyword, dependencyKeyword);
            RegisterChildNode(out NameToken, nameToken);
            RegisterChildNode(out EqualsToken, equalsToken);
            RegisterChildNode(out DependentShaderToken, dependentShaderToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandDependency(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandDependency(this);
        }
    }
}