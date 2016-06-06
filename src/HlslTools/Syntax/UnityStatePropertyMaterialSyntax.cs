using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnityStatePropertyMaterialSyntax : UnityStatePropertySyntax
    {
        public readonly SyntaxToken MaterialKeyword;
        public readonly SyntaxToken OpenBraceToken;
        public readonly List<UnityStatePropertySyntax> StateProperties;
        public readonly SyntaxToken CloseBraceToken;

        public UnityStatePropertyMaterialSyntax(SyntaxToken materialKeyword, SyntaxToken openBraceToken, List<UnityStatePropertySyntax> stateProperties, SyntaxToken closeBraceToken)
            : base(SyntaxKind.UnityStatePropertyMaterial)
        {
            RegisterChildNode(out MaterialKeyword, materialKeyword);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNodes(out StateProperties, stateProperties);
            RegisterChildNode(out CloseBraceToken, closeBraceToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityStatePropertyMaterial(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityStatePropertyMaterial(this);
        }
    }
}