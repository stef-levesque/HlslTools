namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyConstantColorValueSyntax : UnityStatePropertyValueSyntax
    {
        public readonly SyntaxToken OpenParenToken;
        public readonly SyntaxToken RToken;
        public readonly SyntaxToken FirstCommaToken;
        public readonly SyntaxToken GToken;
        public readonly SyntaxToken SecondCommaToken;
        public readonly SyntaxToken BToken;
        public readonly SyntaxToken ThirdCommaToken;
        public readonly SyntaxToken AToken;
        public readonly SyntaxToken CloseParenToken;

        public UnityStatePropertyConstantColorValueSyntax(SyntaxToken openParenToken, SyntaxToken rToken, SyntaxToken firstCommaToken, SyntaxToken gToken, SyntaxToken secondCommaToken, SyntaxToken bToken, SyntaxToken thirdCommaToken, SyntaxToken aToken, SyntaxToken closeParenToken)
            : base(SyntaxKind.UnityStatePropertyConstantColorValue)
        {
            RegisterChildNode(out OpenParenToken, openParenToken);
            RegisterChildNode(out RToken, rToken);
            RegisterChildNode(out FirstCommaToken, firstCommaToken);
            RegisterChildNode(out GToken, gToken);
            RegisterChildNode(out SecondCommaToken, secondCommaToken);
            RegisterChildNode(out BToken, bToken);
            RegisterChildNode(out ThirdCommaToken, thirdCommaToken);
            RegisterChildNode(out AToken, aToken);
            RegisterChildNode(out CloseParenToken, closeParenToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyConstantColorValue(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyConstantColorValue(this);
        }
    }
}