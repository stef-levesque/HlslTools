namespace HlslTools.Syntax
{
    public sealed class UnityShaderPropertyVectorDefaultValueSyntax : UnityShaderPropertyDefaultValueSyntax
    {
        public readonly SyntaxToken OpenParenToken;
        public readonly SyntaxToken XToken;
        public readonly SyntaxToken FirstCommaToken;
        public readonly SyntaxToken YToken;
        public readonly SyntaxToken SecondCommaToken;
        public readonly SyntaxToken ZToken;
        public readonly SyntaxToken ThirdCommaToken;
        public readonly SyntaxToken WToken;
        public readonly SyntaxToken CloseParenToken;

        public UnityShaderPropertyVectorDefaultValueSyntax(SyntaxToken openParenToken, SyntaxToken xToken, SyntaxToken firstCommaToken, SyntaxToken yToken, SyntaxToken secondCommaToken, SyntaxToken zToken, SyntaxToken thirdCommaToken, SyntaxToken wToken, SyntaxToken closeParenToken)
            : base(SyntaxKind.UnityShaderPropertyVectorDefaultValue)
        {
            RegisterChildNode(out OpenParenToken, openParenToken);
            RegisterChildNode(out XToken, xToken);
            RegisterChildNode(out FirstCommaToken, firstCommaToken);
            RegisterChildNode(out YToken, yToken);
            RegisterChildNode(out SecondCommaToken, secondCommaToken);
            RegisterChildNode(out ZToken, zToken);
            RegisterChildNode(out ThirdCommaToken, thirdCommaToken);
            RegisterChildNode(out WToken, wToken);
            RegisterChildNode(out CloseParenToken, closeParenToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityShaderPropertyVectorDefaultValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityShaderPropertyVectorDefaultValue(this);
        }
    }
}