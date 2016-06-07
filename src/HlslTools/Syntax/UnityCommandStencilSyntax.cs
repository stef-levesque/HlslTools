using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnityCommandStencilSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken StencilKeyword;
        public readonly SyntaxToken OpenBraceToken;
        public readonly List<UnityCommandSyntax> StateProperties;
        public readonly SyntaxToken CloseBraceToken;

        public UnityCommandStencilSyntax(SyntaxToken stencilKeyword, SyntaxToken openBraceToken, List<UnityCommandSyntax> stateProperties, SyntaxToken closeBraceToken)
            : base(SyntaxKind.UnityCommandStencil)
        {
            RegisterChildNode(out StencilKeyword, stencilKeyword);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNodes(out StateProperties, stateProperties);
            RegisterChildNode(out CloseBraceToken, closeBraceToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandStencil(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandStencil(this);
        }
    }
}