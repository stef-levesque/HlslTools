namespace HlslTools.Syntax
{
    public class UnityCompilationUnitSyntax : SyntaxNode
    {
        public readonly UnityShaderSyntax Shader;
        public readonly SyntaxToken EndOfFileToken;

        public UnityCompilationUnitSyntax(UnityShaderSyntax shader, SyntaxToken endOfFileToken)
            : base(SyntaxKind.UnityCompilationUnit)
        {
            RegisterChildNode(out Shader, shader);
            RegisterChildNode(out EndOfFileToken, endOfFileToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCompilationUnit(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCompilationUnit(this);
        }
    }
}