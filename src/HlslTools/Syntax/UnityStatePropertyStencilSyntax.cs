using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyStencilSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken StencilKeyword;
        public readonly SyntaxToken OpenBraceToken;
        public readonly List<UnityStatePropertySyntax> StateProperties;
        public readonly SyntaxToken CloseBraceToken;

        public UnityStatePropertyStencilSyntax(SyntaxToken stencilKeyword, SyntaxToken openBraceToken, List<UnityStatePropertySyntax> stateProperties, SyntaxToken closeBraceToken)
            : base(SyntaxKind.UnityStatePropertyStencil)
        {
            RegisterChildNode(out StencilKeyword, stencilKeyword);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNodes(out StateProperties, stateProperties);
            RegisterChildNode(out CloseBraceToken, closeBraceToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyStencil(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyStencil(this);
        }
    }
}