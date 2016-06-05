using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnityShaderSyntax : SyntaxNode
    {
        public readonly SyntaxToken ShaderKeyword;
        public readonly SyntaxToken NameToken;
        public readonly SyntaxToken OpenBraceToken;
        public readonly UnityShaderPropertiesSyntax Properties;
        public readonly UnityCgIncludeSyntax CgInclude;
        public readonly List<SyntaxNode> Statements;
        public readonly List<UnityStatePropertySyntax> StateProperties;
        public readonly SyntaxToken CloseBraceToken;

        public UnityShaderSyntax(SyntaxToken shaderKeyword, SyntaxToken nameToken, SyntaxToken openBraceToken, UnityShaderPropertiesSyntax properties, UnityCgIncludeSyntax cgInclude, List<SyntaxNode> statements, List<UnityStatePropertySyntax> stateProperties, SyntaxToken closeBraceToken)
            : base(SyntaxKind.UnityShader)
        {
            RegisterChildNode(out ShaderKeyword, shaderKeyword);
            RegisterChildNode(out NameToken, nameToken);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNode(out Properties, properties);
            RegisterChildNode(out CgInclude, cgInclude);
            RegisterChildNodes(out Statements, statements);
            RegisterChildNodes(out StateProperties, stateProperties);
            RegisterChildNode(out CloseBraceToken, closeBraceToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityShader(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityShader(this);
        }
    }
}