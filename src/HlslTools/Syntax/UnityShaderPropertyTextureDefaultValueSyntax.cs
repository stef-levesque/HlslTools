namespace HlslTools.Syntax
{
    public sealed class UnityShaderPropertyTextureDefaultValueSyntax : UnityShaderPropertyDefaultValueSyntax
    {
        public readonly SyntaxToken DefaultTextureToken;
        public readonly SyntaxToken OpenBraceToken;
        public readonly SyntaxToken CloseBraceToken;

        public UnityShaderPropertyTextureDefaultValueSyntax(SyntaxToken defaultTextureToken, SyntaxToken openBraceToken, SyntaxToken closeBraceToken)
            : base(SyntaxKind.UnityShaderPropertyTextureDefaultValue)
        {
            RegisterChildNode(out DefaultTextureToken, defaultTextureToken);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNode(out CloseBraceToken, closeBraceToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityShaderPropertyTextureDefaultValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityShaderPropertyTextureDefaultValue(this);
        }
    }
}