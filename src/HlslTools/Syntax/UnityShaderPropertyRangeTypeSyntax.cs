using System;

namespace HlslTools.Syntax
{
    public sealed class UnityShaderPropertyRangeTypeSyntax : UnityShaderPropertyTypeSyntax
    {
        public readonly SyntaxToken RangeKeyword;
        public readonly SyntaxToken OpenParenToken;
        public readonly SyntaxToken MinValueToken;
        public readonly SyntaxToken CommaToken;
        public readonly SyntaxToken MaxValueToken;
        public readonly SyntaxToken CloseParenToken;

        public override SyntaxKind TypeKind => SyntaxKind.UnityRangeKeyword;

        public UnityShaderPropertyRangeTypeSyntax(SyntaxToken rangeKeyword, SyntaxToken openParenToken, SyntaxToken minValueToken, SyntaxToken commaToken, SyntaxToken maxValueToken, SyntaxToken closeParenToken)
            : base(SyntaxKind.UnityShaderPropertyRangeType)
        {
            RegisterChildNode(out RangeKeyword, rangeKeyword);
            RegisterChildNode(out OpenParenToken, openParenToken);
            RegisterChildNode(out MinValueToken, minValueToken);
            RegisterChildNode(out CommaToken, commaToken);
            RegisterChildNode(out MaxValueToken, maxValueToken);
            RegisterChildNode(out CloseParenToken, closeParenToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityShaderPropertyRangeType(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityShaderPropertyRangeType(this);
        }
    }
}