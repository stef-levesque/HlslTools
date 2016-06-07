namespace HlslTools.Syntax
{
    public sealed class UnityCommandNameSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken NameKeyword;
        public readonly SyntaxToken NameToken;

        public UnityCommandNameSyntax(SyntaxToken nameKeyword, SyntaxToken nameToken)
            : base(SyntaxKind.UnityCommandName)
        {
            RegisterChildNode(out NameKeyword, nameKeyword);
            RegisterChildNode(out NameToken, nameToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandName(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandName(this);
        }
    }
}