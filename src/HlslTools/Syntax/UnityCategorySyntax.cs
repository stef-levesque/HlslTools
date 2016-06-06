using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnityCategorySyntax : SyntaxNode
    {
        public readonly SyntaxToken CategoryKeyword;
        public readonly SyntaxToken OpenBraceToken;
        public readonly List<SyntaxNode> Statements;
        public readonly SyntaxToken CloseBraceToken;

        public UnityCategorySyntax(SyntaxToken categoryKeyword, SyntaxToken openBraceToken, List<SyntaxNode> statements, SyntaxToken closeBraceToken)
            : base(SyntaxKind.UnityCategory)
        {
            RegisterChildNode(out CategoryKeyword, categoryKeyword);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNodes(out Statements, statements);
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