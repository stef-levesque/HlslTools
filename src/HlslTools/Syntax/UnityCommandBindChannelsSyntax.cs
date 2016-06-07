using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnityCommandBindChannelsSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken BindChannelsKeyword;
        public readonly SyntaxToken OpenBraceToken;
        public readonly List<UnityCommandSyntax> Commands;
        public readonly SyntaxToken CloseBraceToken;

        public UnityCommandBindChannelsSyntax(SyntaxToken bindChannelsKeyword, SyntaxToken openBraceToken, List<UnityCommandSyntax> commands, SyntaxToken closeBraceToken)
            : base(SyntaxKind.UnityCommandBindChannels)
        {
            RegisterChildNode(out BindChannelsKeyword, bindChannelsKeyword);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNodes(out Commands, commands);
            RegisterChildNode(out CloseBraceToken, closeBraceToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandBindChannels(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandBindChannels(this);
        }
    }
}