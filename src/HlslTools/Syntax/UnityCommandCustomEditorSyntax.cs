namespace HlslTools.Syntax
{
    public sealed class UnityCommandCustomEditorSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken CustomEditorKeyword;
        public readonly SyntaxToken ValueToken;

        public UnityCommandCustomEditorSyntax(SyntaxToken customEditorKeyword, SyntaxToken valueToken)
            : base(SyntaxKind.UnityCommandCustomEditor)
        {
            RegisterChildNode(out CustomEditorKeyword, customEditorKeyword);
            RegisterChildNode(out ValueToken, valueToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandCustomEditor(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandCustomEditor(this);
        }
    }
}