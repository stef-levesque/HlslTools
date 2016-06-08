using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnityEnumNameExpressionSyntax : ExpressionSyntax
    {
        public readonly List<SyntaxToken> NameTokens;

        public UnityEnumNameExpressionSyntax(List<SyntaxToken> nameTokens)
            : base(SyntaxKind.UnityEnumNameExpression)
        {
            RegisterChildNodes(out NameTokens, nameTokens);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityEnumNameExpression(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityEnumNameExpression(this);
        }
    }
}