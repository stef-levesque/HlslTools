namespace HlslTools.Syntax
{
    public sealed class UnityGrabPassSyntax : BaseUnityPassSyntax
    {
        public readonly SyntaxToken GrabPassKeyword;
        public readonly SyntaxToken OpenBraceToken;
        public readonly SyntaxToken CloseBraceToken;

        public UnityGrabPassSyntax(SyntaxToken grabPassKeyword, SyntaxToken openBraceToken, SyntaxToken closeBraceToken)
            : base(SyntaxKind.UnityGrabPass)
        {
            RegisterChildNode(out GrabPassKeyword, grabPassKeyword);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNode(out CloseBraceToken, closeBraceToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityGrabPass(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityGrabPass(this);
        }
    }
}