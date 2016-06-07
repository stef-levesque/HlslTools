using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnityCommandFogSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken FogKeyword;
        public readonly SyntaxToken OpenBraceToken;
        public readonly List<UnityCommandSyntax> Commands;
        public readonly SyntaxToken CloseBraceToken;

        public UnityCommandFogSyntax(SyntaxToken fogKeyword, SyntaxToken openBraceToken, List<UnityCommandSyntax> commands, SyntaxToken closeBraceToken)
            : base (SyntaxKind.UnityCommandFog)
        {
            RegisterChildNode(out FogKeyword, fogKeyword);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNodes(out Commands, commands);
            RegisterChildNode(out CloseBraceToken, closeBraceToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandFog(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandFog(this);
        }
    }
}