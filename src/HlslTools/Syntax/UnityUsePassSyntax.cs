namespace HlslTools.Syntax
{
    public sealed class UnityUsePassSyntax : BaseUnityPassSyntax
    {
        public readonly SyntaxToken UsePassKeyword;
        public readonly SyntaxToken PassName;

        public UnityUsePassSyntax(SyntaxToken usePassKeyword, SyntaxToken passName)
            : base(SyntaxKind.UnityUsePass)
        {
            RegisterChildNode(out UsePassKeyword, usePassKeyword);
            RegisterChildNode(out PassName, passName);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityUsePass(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityUsePass(this);
        }
    }
}