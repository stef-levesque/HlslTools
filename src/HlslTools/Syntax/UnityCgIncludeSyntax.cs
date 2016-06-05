using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnityCgIncludeSyntax : SyntaxNode
    {
        public readonly SyntaxToken CgIncludeKeyword;
        public readonly List<SyntaxNode> Declarations;
        public readonly SyntaxToken EndCgKeyword;

        public UnityCgIncludeSyntax(SyntaxToken cgIncludeKeyword, List<SyntaxNode> declarations, SyntaxToken endCgKeyword)
            : base(SyntaxKind.UnityCgInclude)
        {
            RegisterChildNode(out CgIncludeKeyword, cgIncludeKeyword);
            RegisterChildNodes(out Declarations, declarations);
            RegisterChildNode(out EndCgKeyword, endCgKeyword);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCgInclude(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCgInclude(this);
        }
    }
}