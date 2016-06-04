using System;
using System.Collections.Generic;
using HlslTools.Syntax;

namespace HlslTools.Parser
{
    internal partial class HlslParser
    {
        private UnityShaderSyntax ParseUnityShader()
        {
            var shaderKeyword = Match(SyntaxKind.UnityShaderKeyword);
            var nameToken = Match(SyntaxKind.StringLiteralToken);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            UnityShaderPropertiesSyntax properties = null;
            if (Current.Kind == SyntaxKind.UnityPropertiesKeyword)
                properties = ParseUnityShaderProperties();

            var subShaders = new List<UnitySubShaderSyntax>();
            while (Current.Kind == SyntaxKind.UnitySubShaderKeyword)
                subShaders.Add(ParseUnitySubShader());

            var stateProperties = new List<UnityStatePropertySyntax>();
            var shouldContinue = true;
            while (shouldContinue && Current.Kind != SyntaxKind.CloseBraceToken)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnityFallbackKeyword:
                        stateProperties.Add(ParseUnityFallback());
                        break;

                    default:
                        shouldContinue = false;
                        break;
                }
            }

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityShaderSyntax(
                shaderKeyword, 
                nameToken, 
                openBraceToken, 
                properties, 
                subShaders, 
                stateProperties,
                closeBraceToken);
        }

        private UnityShaderPropertiesSyntax ParseUnityShaderProperties()
        {
            var propertiesKeyword = Match(SyntaxKind.UnityPropertiesKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            var properties = new List<UnityShaderPropertySyntax>();
            while (Current.Kind == SyntaxKind.IdentifierToken)
                properties.Add(ParseUnityShaderProperty());

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityShaderPropertiesSyntax(propertiesKeyword, openBraceToken, properties, closeBraceToken);
        }

        private UnityShaderPropertySyntax ParseUnityShaderProperty()
        {
            var nameToken = Match(SyntaxKind.IdentifierToken);
            var openParenToken = Match(SyntaxKind.OpenParenToken);
            var displayNameToken = Match(SyntaxKind.StringLiteralToken);
            var commaToken = Match(SyntaxKind.CommaToken);
            var propertyType = ParseUnityShaderPropertyType();
            var closeParenToken = Match(SyntaxKind.CloseParenToken);
            var equalsToken = Match(SyntaxKind.EqualsToken);
            var defaultValue = ParseUnityShaderPropertyDefaultValue(propertyType);

            return new UnityShaderPropertySyntax(
                nameToken,
                openParenToken,
                displayNameToken,
                commaToken,
                propertyType,
                closeParenToken,
                equalsToken,
                defaultValue);
        }

        private UnityShaderPropertyTypeSyntax ParseUnityShaderPropertyType()
        {
            switch (Current.Kind)
            {
                case SyntaxKind.UnityRangeKeyword:
                    return ParseUnityShaderPropertyRangeType();
                default:
                    return ParseUnityShaderPropertySimpleType();
            }
        }

        private UnityShaderPropertyRangeTypeSyntax ParseUnityShaderPropertyRangeType()
        {
            var rangeKeyword = Match(SyntaxKind.UnityRangeKeyword);
            var openParenToken = Match(SyntaxKind.OpenParenToken);
            var minValueToken = MatchNumericLiteral();
            var commaToken = Match(SyntaxKind.CommaToken);
            var maxValueToken = MatchNumericLiteral();

            var closeParenToken = Match(SyntaxKind.CloseParenToken);

            return new UnityShaderPropertyRangeTypeSyntax(
                rangeKeyword,
                openParenToken,
                minValueToken,
                commaToken,
                maxValueToken,
                closeParenToken);
        }

        private SyntaxToken MatchNumericLiteral()
        {
            switch (Current.Kind)
            {
                case SyntaxKind.IntegerLiteralToken:
                case SyntaxKind.FloatLiteralToken:
                    return NextToken();
                default:
                    return Match(SyntaxKind.IntegerLiteralToken);
            }
        }

        private UnityShaderPropertySimpleTypeSyntax ParseUnityShaderPropertySimpleType()
        {
            SyntaxToken typeKeyword;
            switch (Current.Kind)
            {
                case SyntaxKind.UnityFloatKeyword:
                case SyntaxKind.UnityIntKeyword:
                case SyntaxKind.UnityColorKeyword:
                case SyntaxKind.UnityVectorKeyword:
                case SyntaxKind.Unity2DKeyword:
                case SyntaxKind.Unity3DKeyword:
                case SyntaxKind.UnityCubeKeyword:
                    typeKeyword = NextToken();
                    break;
                default:
                    typeKeyword = Match(SyntaxKind.UnityIntKeyword);
                    break;
            }

            return new UnityShaderPropertySimpleTypeSyntax(typeKeyword);
        }

        private UnityShaderPropertyDefaultValueSyntax ParseUnityShaderPropertyDefaultValue(UnityShaderPropertyTypeSyntax propertyType)
        {
            switch (propertyType.TypeKind)
            {
                case SyntaxKind.UnityRangeKeyword:
                case SyntaxKind.UnityFloatKeyword:
                case SyntaxKind.UnityIntKeyword:
                    return ParseUnityShaderPropertyNumericDefaultValue(propertyType.TypeKind);
                case SyntaxKind.UnityColorKeyword:
                case SyntaxKind.UnityVectorKeyword:
                    return ParseUnityShaderPropertyVectorDefaultValue();
                case SyntaxKind.Unity2DKeyword:
                case SyntaxKind.Unity3DKeyword:
                case SyntaxKind.UnityCubeKeyword:
                    return ParseUnityShaderPropertyTextureDefaultValue();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private UnityShaderPropertyNumericDefaultValueSyntax ParseUnityShaderPropertyNumericDefaultValue(SyntaxKind propertyType)
        {
            SyntaxToken numberToken;
            switch (propertyType)
            {
                case SyntaxKind.UnityIntKeyword:
                    numberToken = Match(SyntaxKind.IntegerLiteralToken);
                    break;
                case SyntaxKind.UnityFloatKeyword:
                    numberToken = Match(SyntaxKind.FloatLiteralToken);
                    break;
                case SyntaxKind.UnityRangeKeyword:
                    numberToken = MatchNumericLiteral();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new UnityShaderPropertyNumericDefaultValueSyntax(numberToken);
        }

        private UnityShaderPropertyVectorDefaultValueSyntax ParseUnityShaderPropertyVectorDefaultValue()
        {
            var openParenToken = Match(SyntaxKind.OpenParenToken);
            var xToken = MatchNumericLiteral();
            var firstCommaToken = Match(SyntaxKind.CommaToken);
            var yToken = MatchNumericLiteral();
            var secondCommaToken = Match(SyntaxKind.CommaToken);
            var zToken = MatchNumericLiteral();
            var thirdCommaToken = Match(SyntaxKind.CommaToken);
            var wToken = MatchNumericLiteral();
            var closeParenToken = Match(SyntaxKind.CloseParenToken);

            return new UnityShaderPropertyVectorDefaultValueSyntax(
                openParenToken,
                xToken,
                firstCommaToken,
                yToken,
                secondCommaToken,
                zToken,
                thirdCommaToken,
                wToken,
                closeParenToken);
        }

        private UnityShaderPropertyTextureDefaultValueSyntax ParseUnityShaderPropertyTextureDefaultValue()
        {
            var defaultTextureToken = Match(SyntaxKind.StringLiteralToken);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);
            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityShaderPropertyTextureDefaultValueSyntax(
                defaultTextureToken,
                openBraceToken,
                closeBraceToken);
        }

        private UnitySubShaderSyntax ParseUnitySubShader()
        {
            var subShaderKeyword = Match(SyntaxKind.UnitySubShaderKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            UnityShaderTagsSyntax tags = null;
            var stateProperties = new List<UnityStatePropertySyntax>();
            var passes = new List<UnityPassSyntax>();

            var shouldContinue = true;
            while (shouldContinue && Current.Kind != SyntaxKind.CloseBraceToken)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnityTagsKeyword:
                        tags = ParseUnityShaderTags();
                        break;

                    case SyntaxKind.UnityPassKeyword:
                        passes.Add(ParseUnityPass());
                        break;

                    default:
                        shouldContinue = TryParseStateProperty(stateProperties);
                        break;
                }
            }

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnitySubShaderSyntax(subShaderKeyword, openBraceToken, tags, stateProperties, passes, closeBraceToken);
        }

        private UnityShaderTagsSyntax ParseUnityShaderTags()
        {
            var tagsKeyword = Match(SyntaxKind.UnityTagsKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            var tags = new List<UnityShaderTagSyntax>();
            while (Current.Kind == SyntaxKind.StringLiteralToken)
                tags.Add(ParseUnityShaderTag());

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityShaderTagsSyntax(tagsKeyword, openBraceToken, tags, closeBraceToken);
        }

        private UnityShaderTagSyntax ParseUnityShaderTag()
        {
            var nameToken = Match(SyntaxKind.StringLiteralToken);
            var equalsToken = Match(SyntaxKind.EqualsToken);
            var valueToken = Match(SyntaxKind.StringLiteralToken);

            return new UnityShaderTagSyntax(nameToken, equalsToken, valueToken);
        }

        private UnityPassSyntax ParseUnityPass()
        {
            var passKeyword = Match(SyntaxKind.UnityPassKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            UnityShaderTagsSyntax tags = null;
            var stateProperties = new List<UnityStatePropertySyntax>();

            var shouldContinue = true;
            while (shouldContinue && Current.Kind != SyntaxKind.CloseBraceToken)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnityTagsKeyword:
                        tags = ParseUnityShaderTags();
                        break;

                    default:
                        shouldContinue = TryParseStateProperty(stateProperties);
                        break;
                }
            }

            var cgProgram = ParseUnityCgProgram();

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityPassSyntax(passKeyword, openBraceToken, tags, stateProperties, cgProgram, closeBraceToken);
        }

        private UnityCgProgramSyntax ParseUnityCgProgram()
        {
            var cgProgramKeyword = Match(SyntaxKind.UnityCgProgramKeyword);

            _mode = LexerMode.UnityCgProgramSyntax;

            var saveTerm = _termState;
            _termState |= TerminatorState.IsPossibleGlobalDeclarationStartOrStop;

            var declarations = new List<SyntaxNode>();
            ParseTopLevelDeclarations(declarations, SyntaxKind.UnityEndCgKeyword);

            _termState = saveTerm;

            _mode = LexerMode.UnitySyntax;

            var endCgKeyword = Match(SyntaxKind.UnityEndCgKeyword);

            return new UnityCgProgramSyntax(cgProgramKeyword, declarations, endCgKeyword);
        }

        private bool TryParseStateProperty(List<UnityStatePropertySyntax> stateProperties)
        {
            switch (Current.Kind)
            {
                case SyntaxKind.UnityCullKeyword:
                    stateProperties.Add(ParseUnityCull());
                    return true;
                case SyntaxKind.UnityZWriteKeyword:
                    stateProperties.Add(ParseUnityZWrite());
                    return true;
                case SyntaxKind.UnityZTestKeyword:
                    stateProperties.Add(ParseUnityZTest());
                    return true;
                case SyntaxKind.UnityBlendKeyword:
                    stateProperties.Add(ParseUnityBlend());
                    return true;
                case SyntaxKind.UnityColorMaskKeyword:
                    stateProperties.Add(ParseUnityColorMask());
                    return true;

                default:
                    return false;
            }
        }

        private UnityStatePropertySyntax ParseUnityCull()
        {
            var keyword = Match(SyntaxKind.UnityCullKeyword);
            var value = MatchOneOf(SyntaxKind.UnityBackKeyword, SyntaxKind.UnityFrontKeyword, SyntaxKind.UnityOffKeyword);

            return new UnityStatePropertySyntax(keyword, new UnityStatePropertySimpleValueSyntax(value));
        }

        private UnityStatePropertySyntax ParseUnityZWrite()
        {
            var keyword = Match(SyntaxKind.UnityZWriteKeyword);
            var value = MatchOneOf(SyntaxKind.UnityOnKeyword, SyntaxKind.UnityOffKeyword);

            return new UnityStatePropertySyntax(keyword, new UnityStatePropertySimpleValueSyntax(value));
        }

        private UnityStatePropertySyntax ParseUnityZTest()
        {
            var keyword = Match(SyntaxKind.UnityZTestKeyword);
            var value = MatchOneOf(
                SyntaxKind.UnityLEqualKeyword,
                SyntaxKind.UnityLessKeyword,
                SyntaxKind.UnityGreaterKeyword,
                SyntaxKind.UnityGEqualKeyword,
                SyntaxKind.UnityEqualKeyword,
                SyntaxKind.UnityNotEqualKeyword,
                SyntaxKind.UnityAlwaysKeyword);

            return new UnityStatePropertySyntax(keyword, new UnityStatePropertySimpleValueSyntax(value));
        }

        private UnityStatePropertySyntax ParseUnityFallback()
        {
            var keyword = Match(SyntaxKind.UnityFallbackKeyword);
            var value = MatchOneOf(SyntaxKind.UnityOffKeyword, SyntaxKind.StringLiteralToken);

            return new UnityStatePropertySyntax(keyword, new UnityStatePropertySimpleValueSyntax(value));
        }

        private UnityStatePropertySyntax ParseUnityBlend()
        {
            var keyword = Match(SyntaxKind.UnityBlendKeyword);

            UnityStatePropertyValueSyntax value;
            if (Current.Kind.IsUnityBlendFactor())
            {
                value = new UnityStatePropertyBlendValueSyntax(
                    MatchUnityBlendFactor(),
                    MatchUnityBlendFactor());
            }
            else
            {
                value = new UnityStatePropertySimpleValueSyntax(Match(SyntaxKind.UnityOffKeyword));
            }

            return new UnityStatePropertySyntax(keyword, value);
        }

        private SyntaxToken MatchUnityBlendFactor()
        {
            return MatchOneOf(
                SyntaxKind.UnityOneKeyword,
                SyntaxKind.UnityZeroKeyword,
                SyntaxKind.UnitySrcColorKeyword,
                SyntaxKind.UnitySrcAlphaKeyword,
                SyntaxKind.UnityDstColorKeyword,
                SyntaxKind.UnityDstAlphaKeyword,
                SyntaxKind.UnityOneMinusSrcColorKeyword,
                SyntaxKind.UnityOneMinusSrcAlphaKeyword,
                SyntaxKind.UnityOneMinusDstColorKeyword,
                SyntaxKind.UnityOneMinusDstAlphaKeyword);
        }

        private UnityStatePropertySyntax ParseUnityColorMask()
        {
            var keyword = Match(SyntaxKind.UnityColorMaskKeyword);
            var value = MatchOneOf(
                SyntaxKind.UnityRgbKeyword,
                SyntaxKind.UnityAKeyword,
                SyntaxKind.IntegerLiteralToken);

            return new UnityStatePropertySyntax(keyword, new UnityStatePropertySimpleValueSyntax(value));
        }
    }
}