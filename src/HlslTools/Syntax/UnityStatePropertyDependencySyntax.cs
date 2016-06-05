namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyDependencySyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken DependencyKeyword;
        public readonly SyntaxToken NameToken;
        public readonly SyntaxToken EqualsToken;
        public readonly SyntaxToken DependentShaderToken;

        public UnityStatePropertyDependencySyntax(SyntaxToken dependencyKeyword, SyntaxToken nameToken, SyntaxToken equalsToken, SyntaxToken dependentShaderToken)
            : base(SyntaxKind.UnityStatePropertyDependency)
        {
            RegisterChildNode(out DependencyKeyword, dependencyKeyword);
            RegisterChildNode(out NameToken, nameToken);
            RegisterChildNode(out EqualsToken, equalsToken);
            RegisterChildNode(out DependentShaderToken, dependentShaderToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyDependency(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyDependency(this);
        }
    }
}