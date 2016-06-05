namespace HlslTools.Syntax
{
    public sealed class UnityShaderPropertySyntax : SyntaxNode
    {
        public readonly UnityShaderPropertyAttributeSyntax Attribute;
        public readonly SyntaxToken NameToken;
        public readonly SyntaxToken OpenParenToken;
        public readonly SyntaxToken DisplayNameToken;
        public readonly SyntaxToken CommaToken;
        public readonly UnityShaderPropertyTypeSyntax PropertyType;
        public readonly SyntaxToken CloseParenToken;
        public readonly SyntaxToken EqualsToken;
        public readonly UnityShaderPropertyDefaultValueSyntax DefaultValue;

        public UnityShaderPropertySyntax(UnityShaderPropertyAttributeSyntax attribute, SyntaxToken nameToken, SyntaxToken openParenToken, SyntaxToken displayNameToken, SyntaxToken commaToken, UnityShaderPropertyTypeSyntax propertyType, SyntaxToken closeParenToken, SyntaxToken equalsToken, UnityShaderPropertyDefaultValueSyntax defaultValue)
            : base(SyntaxKind.UnityShaderProperty)
        {
            RegisterChildNode(out Attribute, attribute);
            RegisterChildNode(out NameToken, nameToken);
            RegisterChildNode(out OpenParenToken, openParenToken);
            RegisterChildNode(out DisplayNameToken, displayNameToken);
            RegisterChildNode(out CommaToken, commaToken);
            RegisterChildNode(out PropertyType, propertyType);
            RegisterChildNode(out CloseParenToken, closeParenToken);
            RegisterChildNode(out EqualsToken, equalsToken);
            RegisterChildNode(out DefaultValue, defaultValue);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityShaderProperty(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityShaderProperty(this);
        }
    }
}