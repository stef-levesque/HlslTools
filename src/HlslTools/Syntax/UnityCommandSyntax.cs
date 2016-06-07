namespace HlslTools.Syntax
{
    public abstract class UnityCommandSyntax : SyntaxNode
    {
        protected UnityCommandSyntax(SyntaxKind kind)
            : base(kind)
        {
        }
    }
}