using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public class UnityCgProgramSyntax : SyntaxNode
    {
        public readonly SyntaxToken CgProgramKeyword;
        public readonly List<SyntaxNode> Declarations;
        public readonly SyntaxToken EndCgKeyword;

        public UnityCgProgramSyntax(SyntaxToken cgProgramKeyword, List<SyntaxNode> declarations, SyntaxToken endCgKeyword)
            : base(SyntaxKind.UnityCgProgram)
        {
            RegisterChildNode(out CgProgramKeyword, cgProgramKeyword);
            RegisterChildNodes(out Declarations, declarations);
            RegisterChildNode(out EndCgKeyword, endCgKeyword);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCgProgram(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCgProgram(this);
        }
    }
}