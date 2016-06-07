namespace HlslTools.Syntax
{
    public sealed class UnityCommandSetTextureConstantColorSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken ConstantColorKeyword;
        public readonly UnityCommandValueSyntax Value;

        public UnityCommandSetTextureConstantColorSyntax(SyntaxToken constantColorKeyword, UnityCommandValueSyntax value)
            : base(SyntaxKind.UnityCommandSetTextureConstantColor)
        {
            RegisterChildNode(out ConstantColorKeyword, constantColorKeyword);
            RegisterChildNode(out Value, value);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandSetTextureConstantColor(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandSetTextureConstantColor(this);
        }
    }
}