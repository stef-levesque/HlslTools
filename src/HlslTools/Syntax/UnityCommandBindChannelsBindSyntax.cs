namespace HlslTools.Syntax
{
    public sealed class UnityCommandBindChannelsBindSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken BindKeyword;
        public readonly SyntaxToken SourceToken;
        public readonly SyntaxToken CommaToken;
        public readonly SyntaxToken TargetToken;

        public UnityCommandBindChannelsBindSyntax(SyntaxToken bindKeyword, SyntaxToken sourceToken, SyntaxToken commaToken, SyntaxToken targetToken)
            : base(SyntaxKind.UnityCommandBindChannelsBind)
        {
            RegisterChildNode(out BindKeyword, bindKeyword);
            RegisterChildNode(out SourceToken, sourceToken);
            RegisterChildNode(out CommaToken, commaToken);
            RegisterChildNode(out TargetToken, targetToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandBindChannelsBind(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandBindChannelsBind(this);
        }
    }
}