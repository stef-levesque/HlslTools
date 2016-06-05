using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnityPassSyntax : BaseUnityPassSyntax
    {
        public readonly SyntaxToken PassKeyword;
        public readonly SyntaxToken OpenBraceToken;
        public readonly UnityStatePropertySyntax Name;
        public readonly List<SyntaxNode> Statements;
        public readonly UnityCgProgramSyntax CgProgram;
        public readonly SyntaxToken CloseBraceToken;

        public UnityPassSyntax(SyntaxToken passKeyword, SyntaxToken openBraceToken, UnityStatePropertySyntax name, List<SyntaxNode> statements, UnityCgProgramSyntax cgProgram, SyntaxToken closeBraceToken)
            : base(SyntaxKind.UnityPass)
        {
            RegisterChildNode(out PassKeyword, passKeyword);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNode(out Name, name);
            RegisterChildNodes(out Statements, statements);
            RegisterChildNode(out CgProgram, cgProgram);
            RegisterChildNode(out CloseBraceToken, closeBraceToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityPass(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityPass(this);
        }
    }
}