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
            var typeKeyword = MatchOneOf(
                SyntaxKind.UnityIntKeyword,
                SyntaxKind.UnityFloatKeyword,
                SyntaxKind.UnityColorKeyword,
                SyntaxKind.UnityVectorKeyword,
                SyntaxKind.Unity2DKeyword,
                SyntaxKind.Unity3DKeyword,
                SyntaxKind.UnityCubeKeyword,
                SyntaxKind.UnityAnyKeyword);

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
                case SyntaxKind.UnityAnyKeyword:
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
            UnityCgProgramSyntax cgProgram = null;
            var passes = new List<UnityPassSyntax>();

            var shouldContinue = true;
            while (shouldContinue && Current.Kind != SyntaxKind.CloseBraceToken)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnityTagsKeyword:
                        tags = ParseUnityShaderTags();
                        break;
                    case SyntaxKind.UnityCgProgramKeyword:
                        cgProgram = ParseUnityCgProgram();
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

            return new UnitySubShaderSyntax(
                subShaderKeyword, 
                openBraceToken, 
                tags, 
                stateProperties, 
                cgProgram, 
                passes, 
                closeBraceToken);
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

            UnityStatePropertySyntax name = null;
            UnityShaderTagsSyntax tags = null;
            var stateProperties = new List<UnityStatePropertySyntax>();
            UnityCgProgramSyntax cgProgram = null;

            var shouldContinue = true;
            while (shouldContinue && Current.Kind != SyntaxKind.CloseBraceToken)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnityTagsKeyword:
                        tags = ParseUnityShaderTags();
                        break;
                    case SyntaxKind.UnityCgProgramKeyword:
                        cgProgram = ParseUnityCgProgram();
                        break;
                    case SyntaxKind.UnityNameKeyword:
                        name = ParseUnityName();
                        break;

                    default:
                        shouldContinue = TryParseStateProperty(stateProperties);
                        break;
                }
            }

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityPassSyntax(
                passKeyword, 
                openBraceToken, 
                name, 
                tags, 
                stateProperties, 
                cgProgram, 
                closeBraceToken);
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
                case SyntaxKind.UnityLodKeyword:
                    stateProperties.Add(ParseUnityLod());
                    return true;
                case SyntaxKind.UnityLightingKeyword:
                    stateProperties.Add(ParseUnityLighting());
                    return true;

                default:
                    return false;
            }
        }

        private UnityStatePropertyValueSyntax ParseUnityStatePropertyValue(SyntaxKind preferred, params SyntaxKind[] otherOptions)
        {
            if (Current.Kind == SyntaxKind.OpenBracketToken)
            {
                var openBracketToken = NextToken();
                var identifier = Match(SyntaxKind.IdentifierToken);
                var closeBracketToken = Match(SyntaxKind.CloseBracketToken);

                return new UnityStatePropertyVariableValueSyntax(
                    openBracketToken,
                    identifier,
                    closeBracketToken);
            }

            var valueToken = MatchOneOf(preferred, otherOptions);
            return new UnityStatePropertyConstantValueSyntax(valueToken);
        }

        private UnityStatePropertySyntax ParseUnityFallback()
        {
            var keyword = Match(SyntaxKind.UnityFallbackKeyword);
            var value = MatchOneOf(SyntaxKind.IdentifierToken, SyntaxKind.StringLiteralToken);

            return new UnityStatePropertyFallbackSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityCull()
        {
            var keyword = Match(SyntaxKind.UnityCullKeyword);
            var value = ParseUnityStatePropertyValue(SyntaxKind.IdentifierToken);

            return new UnityStatePropertyCullSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityZWrite()
        {
            var keyword = Match(SyntaxKind.UnityZWriteKeyword);
            var value = Match(SyntaxKind.IdentifierToken);

            return new UnityStatePropertyZWriteSyntax(keyword, new UnityStatePropertyConstantValueSyntax(value));
        }

        private UnityStatePropertySyntax ParseUnityZTest()
        {
            var keyword = Match(SyntaxKind.UnityZTestKeyword);
            var value = Match(SyntaxKind.IdentifierToken);

            return new UnityStatePropertyZTestSyntax(keyword, new UnityStatePropertyConstantValueSyntax(value));
        }

        private UnityStatePropertySyntax ParseUnityBlend()
        {
            var keyword = Match(SyntaxKind.UnityBlendKeyword);

            if (Current.Kind == SyntaxKind.IdentifierToken && Current.Text == "Off")
            {
                return new UnityStatePropertyBlendOffSyntax(keyword, NextToken());
            }

            var srcFactorToken = Match(SyntaxKind.IdentifierToken);
            var dstFactorToken = Match(SyntaxKind.IdentifierToken);

            if (Current.Kind == SyntaxKind.CommaToken)
            {
                var commaToken = Match(SyntaxKind.CommaToken);
                var srcFactorAToken = Match(SyntaxKind.IdentifierToken);
                var dstFactorAToken = Match(SyntaxKind.IdentifierToken);

                return new UnityStatePropertyBlendColorAlphaSyntax(
                    keyword,
                    srcFactorToken,
                    dstFactorToken,
                    commaToken,
                    srcFactorAToken,
                    dstFactorAToken);
            }

            return new UnityStatePropertyBlendColorSyntax(
                keyword,
                srcFactorToken,
                dstFactorToken);
        }

        private UnityStatePropertySyntax ParseUnityColorMask()
        {
            var keyword = Match(SyntaxKind.UnityColorMaskKeyword);
            var maskToken = MatchOneOf(
                SyntaxKind.IdentifierToken,
                SyntaxKind.IntegerLiteralToken);

            return new UnityStatePropertyColorMaskSyntax(keyword, maskToken);
        }

        private UnityStatePropertySyntax ParseUnityLod()
        {
            var keyword = Match(SyntaxKind.UnityLodKeyword);
            var value = ParseUnityStatePropertyValue(SyntaxKind.IntegerLiteralToken);

            return new UnityStatePropertyLodSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityName()
        {
            var keyword = Match(SyntaxKind.UnityNameKeyword);
            var value = Match(SyntaxKind.StringLiteralToken);

            return new UnityStatePropertyNameSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityLighting()
        {
            var keyword = Match(SyntaxKind.UnityLightingKeyword);
            var value = Match(SyntaxKind.IdentifierToken);

            return new UnityStatePropertyLightingSyntax(keyword, value);
        }
    }
}