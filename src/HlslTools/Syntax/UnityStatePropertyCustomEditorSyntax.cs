namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyCustomEditorSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken CustomEditorKeyword;
        public readonly SyntaxToken ValueToken;

        public UnityStatePropertyCustomEditorSyntax(SyntaxToken customEditorKeyword, SyntaxToken valueToken)
            : base(SyntaxKind.UnityStatePropertyCustomEditor)
        {
            RegisterChildNode(out CustomEditorKeyword, customEditorKeyword);
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyCustomEditor(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyCustomEditor(this);
        }
    }
}