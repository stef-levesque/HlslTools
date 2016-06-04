using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnityShaderTagsSyntax : SyntaxNode
    {
        public readonly SyntaxToken TagsKeyword;
        public readonly SyntaxToken OpenBraceToken;
        public readonly List<UnityShaderTagSyntax> Tags;
        public readonly SyntaxToken CloseBraceToken;

        public UnityShaderTagsSyntax(SyntaxToken tagsKeyword, SyntaxToken openBraceToken, List<UnityShaderTagSyntax> tags, SyntaxToken closeBraceToken)
            : base(SyntaxKind.UnityShaderTags)
        {
            RegisterChildNode(out TagsKeyword, tagsKeyword);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNodes(out Tags, tags);
            RegisterChildNode(out CloseBraceToken, closeBraceToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityShaderTags(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityShaderTags(this);
        }
    }
}