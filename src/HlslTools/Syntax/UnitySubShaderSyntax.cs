using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnitySubShaderSyntax : SyntaxNode
    {
        public readonly SyntaxToken SubShaderKeyword;
        public readonly SyntaxToken OpenBraceToken;
        public readonly UnityShaderTagsSyntax Tags;
        public readonly List<UnityStatePropertySyntax> StateProperties;
        public readonly UnityCgProgramSyntax CgProgram;
        public readonly List<UnityPassSyntax> Passes;
        public readonly SyntaxToken CloseBraceToken;

        public UnitySubShaderSyntax(SyntaxToken subShaderKeyword, SyntaxToken openBraceToken, UnityShaderTagsSyntax tags, List<UnityStatePropertySyntax> stateProperties, UnityCgProgramSyntax cgProgram, List<UnityPassSyntax> passes, SyntaxToken closeBraceToken)
            : base(SyntaxKind.UnitySubShader)
        {
            RegisterChildNode(out SubShaderKeyword, subShaderKeyword);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNode(out Tags, tags);
            RegisterChildNodes(out StateProperties, stateProperties);
            RegisterChildNode(out CgProgram, cgProgram);
            RegisterChildNodes(out Passes, passes);
            RegisterChildNode(out CloseBraceToken, closeBraceToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnitySubShader(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnitySubShader(this);
        }
    }
}