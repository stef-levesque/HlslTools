using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnityCategorySyntax : SyntaxNode
    {
        public readonly SyntaxToken CategoryKeyword;
        public readonly SyntaxToken OpenBraceToken;
        public readonly UnityShaderTagsSyntax Tags;
        public readonly List<UnityStatePropertySyntax> StateProperties;
        public readonly List<UnitySubShaderSyntax> SubShaders;
        public readonly SyntaxToken CloseBraceToken;

        public UnityCategorySyntax(SyntaxToken categoryKeyword, SyntaxToken openBraceToken, UnityShaderTagsSyntax tags, List<UnityStatePropertySyntax> stateProperties, List<UnitySubShaderSyntax> subShaders, SyntaxToken closeBraceToken)
            : base(SyntaxKind.UnityCategory)
        {
            RegisterChildNode(out CategoryKeyword, categoryKeyword);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNode(out Tags, tags);
            RegisterChildNodes(out StateProperties, stateProperties);
            RegisterChildNodes(out SubShaders, subShaders);
            RegisterChildNode(out CloseBraceToken, closeBraceToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCategory(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCategory(this);
        }
    }
}