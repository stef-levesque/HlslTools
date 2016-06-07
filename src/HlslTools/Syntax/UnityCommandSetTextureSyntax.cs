using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnityCommandSetTextureSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken SetTextureKeyword;
        public readonly UnityCommandVariableValueSyntax TextureName;
        public readonly SyntaxToken OpenBraceToken;
        public readonly List<UnityCommandSyntax> Commands;
        public readonly SyntaxToken CloseBraceToken;

        public UnityCommandSetTextureSyntax(SyntaxToken setTextureKeyword, UnityCommandVariableValueSyntax textureName, SyntaxToken openBraceToken, List<UnityCommandSyntax> commands, SyntaxToken closeBraceToken)
            : base(SyntaxKind.UnityCommandSetTexture)
        {
            RegisterChildNode(out SetTextureKeyword, setTextureKeyword);
            RegisterChildNode(out TextureName, textureName);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNodes(out Commands, commands);
            RegisterChildNode(out CloseBraceToken, closeBraceToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandSetTexture(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandSetTexture(this);
        }
    }
}