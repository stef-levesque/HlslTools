using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public class UnityShaderPropertiesSyntax : SyntaxNode
    {
        public readonly List<UnityShaderPropertySyntax> Properties;

        public UnityShaderPropertiesSyntax(List<UnityShaderPropertySyntax> properties)
            : base(SyntaxKind.UnityShaderProperties)
        {
            RegisterChildNodes(out Properties, properties);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityShaderProperties(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityShaderProperties(this);
        }
    }
}