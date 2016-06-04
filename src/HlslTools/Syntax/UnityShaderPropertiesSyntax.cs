using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnityShaderPropertiesSyntax : SyntaxNode
    {
        public readonly SyntaxToken PropertiesKeyword;
        public readonly SyntaxToken OpenBraceToken;
        public readonly List<UnityShaderPropertySyntax> Properties;
        public readonly SyntaxToken CloseBraceToken;

        public UnityShaderPropertiesSyntax(SyntaxToken propertiesKeyword, SyntaxToken openBraceToken, List<UnityShaderPropertySyntax> properties, SyntaxToken closeBraceToken)
            : base(SyntaxKind.UnityShaderProperties)
        {
            RegisterChildNode(out PropertiesKeyword, propertiesKeyword);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNodes(out Properties, properties);
            RegisterChildNode(out CloseBraceToken, closeBraceToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityShaderProperties(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityShaderProperties(this);
        }
    }
}