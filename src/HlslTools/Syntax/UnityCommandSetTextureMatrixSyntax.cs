namespace HlslTools.Syntax
{
    public sealed class UnityCommandSetTextureMatrixSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken MatrixKeyword;
        public readonly UnityCommandVariableValueSyntax Value;

        public UnityCommandSetTextureMatrixSyntax(SyntaxToken matrixKeyword, UnityCommandVariableValueSyntax value)
            : base(SyntaxKind.UnityCommandSetTextureMatrix)
        {
            RegisterChildNode(out MatrixKeyword, matrixKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandSetTextureMatrix(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandSetTextureMatrix(this);
        }
    }
}