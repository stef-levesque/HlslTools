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

            UnityCgIncludeSyntax cgInclude = null;
            if (Current.Kind == SyntaxKind.UnityCgIncludeKeyword)
                cgInclude = ParseUnityCgInclude();

            var statements = new List<SyntaxNode>();
            while (Current.Kind == SyntaxKind.UnitySubShaderKeyword || Current.Kind == SyntaxKind.UnityCategoryKeyword)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnitySubShaderKeyword:
                        statements.Add(ParseUnitySubShader());
                        break;
                    case SyntaxKind.UnityCategoryKeyword:
                        statements.Add(ParseUnityCategory());
                        break;
                }
            }

            var stateProperties = new List<UnityStatePropertySyntax>();
            var shouldContinue = true;
            while (shouldContinue && Current.Kind != SyntaxKind.CloseBraceToken)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnityFallbackKeyword:
                        stateProperties.Add(ParseUnityFallback());
                        break;
                    case SyntaxKind.UnityCustomEditorKeyword:
                        stateProperties.Add(ParseUnityCustomEditor());
                        break;
                    case SyntaxKind.UnityDependencyKeyword:
                        stateProperties.Add(ParseUnityDependency());
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
                cgInclude,
                statements, 
                stateProperties,
                closeBraceToken);
        }

        private UnityShaderPropertiesSyntax ParseUnityShaderProperties()
        {
            var propertiesKeyword = Match(SyntaxKind.UnityPropertiesKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            var properties = new List<UnityShaderPropertySyntax>();
            while (Current.Kind == SyntaxKind.IdentifierToken || Current.Kind == SyntaxKind.OpenBracketToken)
                properties.Add(ParseUnityShaderProperty());

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityShaderPropertiesSyntax(propertiesKeyword, openBraceToken, properties, closeBraceToken);
        }

        private UnityShaderPropertySyntax ParseUnityShaderProperty()
        {
            var attributes = new List<UnityShaderPropertyAttributeSyntax>();
            while (Current.Kind == SyntaxKind.OpenBracketToken)
                attributes.Add(ParseUnityShaderPropertyAttribute());

            var nameToken = Match(SyntaxKind.IdentifierToken);
            var openParenToken = Match(SyntaxKind.OpenParenToken);
            var displayNameToken = Match(SyntaxKind.StringLiteralToken);
            var commaToken = Match(SyntaxKind.CommaToken);
            var propertyType = ParseUnityShaderPropertyType();
            var closeParenToken = Match(SyntaxKind.CloseParenToken);
            var equalsToken = Match(SyntaxKind.EqualsToken);
            var defaultValue = ParseUnityShaderPropertyDefaultValue(propertyType);

            return new UnityShaderPropertySyntax(
                attributes,
                nameToken,
                openParenToken,
                displayNameToken,
                commaToken,
                propertyType,
                closeParenToken,
                equalsToken,
                defaultValue);
        }

        private UnityShaderPropertyAttributeSyntax ParseUnityShaderPropertyAttribute()
        {
            var openBracketToken = Match(SyntaxKind.OpenBracketToken);
            var name = Match(SyntaxKind.IdentifierToken);
            var arguments = ParseAttributeArgumentList();
            var closeBracketToken = Match(SyntaxKind.CloseBracketToken);

            return new UnityShaderPropertyAttributeSyntax(
                openBracketToken,
                name,
                arguments,
                closeBracketToken);
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
            var minValue = ParseUnityPossiblyNegativeNumericLiteralExpression();
            var commaToken = Match(SyntaxKind.CommaToken);
            var maxValue = ParseUnityPossiblyNegativeNumericLiteralExpression();

            var closeParenToken = Match(SyntaxKind.CloseParenToken);

            return new UnityShaderPropertyRangeTypeSyntax(
                rangeKeyword,
                openParenToken,
                minValue,
                commaToken,
                maxValue,
                closeParenToken);
        }

        private ExpressionSyntax ParseUnityPossiblyNegativeNumericLiteralExpression()
        {
            if (Current.Kind == SyntaxKind.MinusToken)
            {
                var operatorToken = NextToken();
                var literalToken = MatchOneOf(SyntaxKind.IntegerLiteralToken, SyntaxKind.FloatLiteralToken);
                var operand = new LiteralExpressionSyntax(
                    SyntaxFacts.GetLiteralExpression(literalToken.Kind),
                    literalToken);
                return new PrefixUnaryExpressionSyntax(
                    SyntaxFacts.GetPrefixUnaryExpression(operatorToken.Kind),
                    operatorToken, operand);
            }
            else
            {
                var literalToken = MatchOneOf(SyntaxKind.IntegerLiteralToken, SyntaxKind.FloatLiteralToken);
                return new LiteralExpressionSyntax(
                    SyntaxFacts.GetLiteralExpression(literalToken.Kind),
                    literalToken);
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
            var number = ParseUnityPossiblyNegativeNumericLiteralExpression();
            return new UnityShaderPropertyNumericDefaultValueSyntax(number);
        }

        private UnityShaderPropertyVectorDefaultValueSyntax ParseUnityShaderPropertyVectorDefaultValue()
        {
            var openParenToken = Match(SyntaxKind.OpenParenToken);
            var x = ParseUnityPossiblyNegativeNumericLiteralExpression();
            var firstCommaToken = Match(SyntaxKind.CommaToken);
            var y = ParseUnityPossiblyNegativeNumericLiteralExpression();
            var secondCommaToken = Match(SyntaxKind.CommaToken);
            var z = ParseUnityPossiblyNegativeNumericLiteralExpression();
            var thirdCommaToken = Match(SyntaxKind.CommaToken);
            var w = ParseUnityPossiblyNegativeNumericLiteralExpression();
            var closeParenToken = Match(SyntaxKind.CloseParenToken);

            return new UnityShaderPropertyVectorDefaultValueSyntax(
                openParenToken,
                x,
                firstCommaToken,
                y,
                secondCommaToken,
                z,
                thirdCommaToken,
                w,
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

        private UnityCategorySyntax ParseUnityCategory()
        {
            var categoryKeyword = Match(SyntaxKind.UnityCategoryKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            var statements = new List<SyntaxNode>();
            var shouldContinue = true;
            while (shouldContinue && Current.Kind != SyntaxKind.CloseBraceToken)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnityTagsKeyword:
                        statements.Add(ParseUnityShaderTags());
                        break;
                    case SyntaxKind.UnitySubShaderKeyword:
                        statements.Add(ParseUnitySubShader());
                        break;

                    default:
                        shouldContinue = TryParseStateProperty(statements);
                        break;
                }
            }

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityCategorySyntax(
                categoryKeyword,
                openBraceToken,
                statements,
                closeBraceToken);
        }

        private UnitySubShaderSyntax ParseUnitySubShader()
        {
            var subShaderKeyword = Match(SyntaxKind.UnitySubShaderKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            var statements = new List<SyntaxNode>();
            var shouldContinue = true;
            while (shouldContinue && Current.Kind != SyntaxKind.CloseBraceToken)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnityTagsKeyword:
                        statements.Add(ParseUnityShaderTags());
                        break;
                    case SyntaxKind.UnityPassKeyword:
                        statements.Add(ParseUnityPass());
                        break;
                    case SyntaxKind.UnityUsePassKeyword:
                        statements.Add(ParseUnityUsePass());
                        break;
                    case SyntaxKind.UnityGrabPassKeyword:
                        statements.Add(ParseUnityGrabPass());
                        break;
                    case SyntaxKind.UnityCgProgramKeyword:
                        statements.Add(ParseUnityCgProgram());
                        break;
                    case SyntaxKind.UnityCgIncludeKeyword:
                        statements.Add(ParseUnityCgInclude());
                        break;

                    default:
                        shouldContinue = TryParseStateProperty(statements);
                        break;
                }
            }

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnitySubShaderSyntax(
                subShaderKeyword, 
                openBraceToken,
                statements, 
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

            var statements = new List<SyntaxNode>();
            var shouldContinue = true;
            while (shouldContinue && Current.Kind != SyntaxKind.CloseBraceToken)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnityTagsKeyword:
                        statements.Add(ParseUnityShaderTags());
                        break;
                    case SyntaxKind.UnityNameKeyword:
                        statements.Add(ParseUnityName());
                        break;

                    default:
                        shouldContinue = TryParseStateProperty(statements);
                        break;
                }
            }

            UnityCgProgramSyntax cgProgram = null;
            if (Current.Kind == SyntaxKind.UnityCgProgramKeyword)
                cgProgram = ParseUnityCgProgram();

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityPassSyntax(
                passKeyword, 
                openBraceToken, 
                statements,
                cgProgram, 
                closeBraceToken);
        }

        private UnityGrabPassSyntax ParseUnityGrabPass()
        {
            var grabPassKeyword = Match(SyntaxKind.UnityGrabPassKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            var statements = new List<SyntaxNode>();
            var shouldContinue = true;
            while (shouldContinue && Current.Kind != SyntaxKind.CloseBraceToken)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnityTagsKeyword:
                        statements.Add(ParseUnityShaderTags());
                        break;
                    case SyntaxKind.UnityNameKeyword:
                        statements.Add(ParseUnityName());
                        break;

                    default:
                        shouldContinue = TryParseStateProperty(statements);
                        break;
                }
            }

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityGrabPassSyntax(
                grabPassKeyword,
                openBraceToken,
                statements,
                closeBraceToken);
        }

        private UnityUsePassSyntax ParseUnityUsePass()
        {
            var usePassKeyword = Match(SyntaxKind.UnityUsePassKeyword);
            var passName = Match(SyntaxKind.StringLiteralToken);

            return new UnityUsePassSyntax(
                usePassKeyword,
                passName);
        }

        private UnityCgProgramSyntax ParseUnityCgProgram()
        {
            var cgProgramKeyword = Match(SyntaxKind.UnityCgProgramKeyword);

            List<SyntaxNode> declarations;
            SyntaxToken endCgKeyword;

            ParseUnityCgProgramOrInclude(out declarations, out endCgKeyword);

            return new UnityCgProgramSyntax(cgProgramKeyword, declarations, endCgKeyword);
        }

        private UnityCgIncludeSyntax ParseUnityCgInclude()
        {
            var cgIncludeKeyword = Match(SyntaxKind.UnityCgIncludeKeyword);

            List<SyntaxNode> declarations;
            SyntaxToken endCgKeyword;

            ParseUnityCgProgramOrInclude(out declarations, out endCgKeyword);

            return new UnityCgIncludeSyntax(cgIncludeKeyword, declarations, endCgKeyword);
        }

        private void ParseUnityCgProgramOrInclude(out List<SyntaxNode> declarations, out SyntaxToken endCgKeyword)
        {
            _mode = LexerMode.UnityCgProgramSyntax;

            var saveTerm = _termState;
            _termState |= TerminatorState.IsPossibleGlobalDeclarationStartOrStop;

            declarations = new List<SyntaxNode>();
            ParseTopLevelDeclarations(declarations, SyntaxKind.UnityEndCgKeyword);

            _termState = saveTerm;

            _mode = LexerMode.UnitySyntax;

            endCgKeyword = Match(SyntaxKind.UnityEndCgKeyword);
        }

        private bool TryParseStateProperty(List<SyntaxNode> stateProperties)
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
                case SyntaxKind.UnityOffsetKeyword:
                    stateProperties.Add(ParseUnityOffset());
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
                case SyntaxKind.UnityStencilKeyword:
                    stateProperties.Add(ParseUnityStencil());
                    return true;
                case SyntaxKind.UnityMaterialKeyword:
                    stateProperties.Add(ParseUnityMaterial());
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

        private UnityStatePropertySyntax ParseUnityCustomEditor()
        {
            var keyword = Match(SyntaxKind.UnityCustomEditorKeyword);
            var value = Match(SyntaxKind.StringLiteralToken);

            return new UnityStatePropertyCustomEditorSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityDependency()
        {
            var keyword = Match(SyntaxKind.UnityDependencyKeyword);
            var name = Match(SyntaxKind.StringLiteralToken);
            var equalsToken = Match(SyntaxKind.EqualsToken);
            var dependentShaderToken = Match(SyntaxKind.StringLiteralToken);

            return new UnityStatePropertyDependencySyntax(keyword, name, equalsToken, dependentShaderToken);
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
            var value = ParseUnityStatePropertyValue(SyntaxKind.IdentifierToken);

            return new UnityStatePropertyZWriteSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityZTest()
        {
            var keyword = Match(SyntaxKind.UnityZTestKeyword);
            var value = ParseUnityStatePropertyValue(SyntaxKind.IdentifierToken);

            return new UnityStatePropertyZTestSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityBlend()
        {
            var keyword = Match(SyntaxKind.UnityBlendKeyword);

            if (Current.Kind == SyntaxKind.IdentifierToken && Current.Text == "Off")
            {
                return new UnityStatePropertyBlendOffSyntax(keyword, NextToken());
            }

            var srcFactor = ParseUnityStatePropertyValue(SyntaxKind.IdentifierToken);
            var dstFactor = ParseUnityStatePropertyValue(SyntaxKind.IdentifierToken);

            if (Current.Kind == SyntaxKind.CommaToken)
            {
                var commaToken = Match(SyntaxKind.CommaToken);
                var srcFactorA = ParseUnityStatePropertyValue(SyntaxKind.IdentifierToken);
                var dstFactorA = ParseUnityStatePropertyValue(SyntaxKind.IdentifierToken);

                return new UnityStatePropertyBlendColorAlphaSyntax(
                    keyword,
                    srcFactor,
                    dstFactor,
                    commaToken,
                    srcFactorA,
                    dstFactorA);
            }

            return new UnityStatePropertyBlendColorSyntax(
                keyword,
                srcFactor,
                dstFactor);
        }

        private UnityStatePropertySyntax ParseUnityColorMask()
        {
            var keyword = Match(SyntaxKind.UnityColorMaskKeyword);
            var maskToken = ParseUnityStatePropertyValue(
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

        private UnityStatePropertySyntax ParseUnityOffset()
        {
            var keyword = Match(SyntaxKind.UnityOffsetKeyword);
            var factor = ParseUnityStatePropertyValue(SyntaxKind.FloatLiteralToken, SyntaxKind.IntegerLiteralToken);
            var commaToken = Match(SyntaxKind.CommaToken);
            var units = ParseUnityStatePropertyValue(SyntaxKind.FloatLiteralToken, SyntaxKind.IntegerLiteralToken);

            return new UnityStatePropertyOffsetSyntax(keyword, factor, commaToken, units);
        }

        private UnityStatePropertySyntax ParseUnityStencil()
        {
            var keyword = Match(SyntaxKind.UnityStencilKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            var stateProperties = new List<UnityStatePropertySyntax>();
            var shouldContinue = true;
            while (shouldContinue && Current.Kind != SyntaxKind.CloseBraceToken)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnityRefKeyword:
                        stateProperties.Add(ParseUnityStencilRef());
                        break;
                    case SyntaxKind.UnityReadMaskKeyword:
                        stateProperties.Add(ParseUnityStencilReadMask());
                        break;
                    case SyntaxKind.UnityWriteMaskKeyword:
                        stateProperties.Add(ParseUnityStencilWriteMask());
                        break;
                    case SyntaxKind.UnityCompKeyword:
                        stateProperties.Add(ParseUnityStencilComp());
                        break;
                    case SyntaxKind.UnityPassKeyword:
                        stateProperties.Add(ParseUnityStencilPass());
                        break;
                    case SyntaxKind.UnityFailKeyword:
                        stateProperties.Add(ParseUnityStencilFail());
                        break;
                    case SyntaxKind.UnityZFailKeyword:
                        stateProperties.Add(ParseUnityStencilZFail());
                        break;

                    default:
                        shouldContinue = false;
                        break;
                }
            }

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityStatePropertyStencilSyntax(
                keyword, 
                openBraceToken, 
                stateProperties, 
                closeBraceToken);
        }

        private UnityStatePropertySyntax ParseUnityStencilRef()
        {
            var keyword = Match(SyntaxKind.UnityRefKeyword);
            var value = ParseUnityStatePropertyValue(SyntaxKind.IntegerLiteralToken);

            return new UnityStatePropertyStencilRefSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityStencilReadMask()
        {
            var keyword = Match(SyntaxKind.UnityReadMaskKeyword);
            var value = ParseUnityStatePropertyValue(SyntaxKind.IntegerLiteralToken);

            return new UnityStatePropertyStencilReadMaskSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityStencilWriteMask()
        {
            var keyword = Match(SyntaxKind.UnityWriteMaskKeyword);
            var value = ParseUnityStatePropertyValue(SyntaxKind.IntegerLiteralToken);

            return new UnityStatePropertyStencilWriteMaskSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityStencilComp()
        {
            var keyword = Match(SyntaxKind.UnityCompKeyword);
            var value = ParseUnityStatePropertyValue(SyntaxKind.IdentifierToken);

            return new UnityStatePropertyStencilCompSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityStencilPass()
        {
            var keyword = Match(SyntaxKind.UnityPassKeyword);
            var value = ParseUnityStatePropertyValue(SyntaxKind.IdentifierToken);

            return new UnityStatePropertyStencilPassSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityStencilFail()
        {
            var keyword = Match(SyntaxKind.UnityFailKeyword);
            var value = ParseUnityStatePropertyValue(SyntaxKind.IdentifierToken);

            return new UnityStatePropertyStencilFailSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityStencilZFail()
        {
            var keyword = Match(SyntaxKind.UnityZFailKeyword);
            var value = ParseUnityStatePropertyValue(SyntaxKind.IdentifierToken);

            return new UnityStatePropertyStencilZFailSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityMaterial()
        {
            var keyword = Match(SyntaxKind.UnityMaterialKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            var stateProperties = new List<UnityStatePropertySyntax>();
            var shouldContinue = true;
            while (shouldContinue && Current.Kind != SyntaxKind.CloseBraceToken)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnityDiffuseKeyword:
                        stateProperties.Add(ParseUnityMaterialDiffuse());
                        break;
                    case SyntaxKind.UnityAmbientKeyword:
                        stateProperties.Add(ParseUnityMaterialAmbient());
                        break;
                    case SyntaxKind.UnityShininessKeyword:
                        stateProperties.Add(ParseUnityMaterialShininess());
                        break;
                    case SyntaxKind.UnitySpecularKeyword:
                        stateProperties.Add(ParseUnityMaterialSpecular());
                        break;
                    case SyntaxKind.UnityEmissionKeyword:
                        stateProperties.Add(ParseUnityMaterialEmission());
                        break;

                    default:
                        shouldContinue = false;
                        break;
                }
            }

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityStatePropertyMaterialSyntax(
                keyword,
                openBraceToken,
                stateProperties,
                closeBraceToken);
        }

        private UnityStatePropertyValueSyntax ParseUnityStatePropertyColorValue()
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

            var openParenToken = Match(SyntaxKind.OpenParenToken);
            var r = ParseUnityPossiblyNegativeNumericLiteralExpression();
            var firstCommaToken = Match(SyntaxKind.CommaToken);
            var g = ParseUnityPossiblyNegativeNumericLiteralExpression();
            var secondCommaToken = Match(SyntaxKind.CommaToken);
            var b = ParseUnityPossiblyNegativeNumericLiteralExpression();
            var thirdCommaToken = Match(SyntaxKind.CommaToken);
            var a = ParseUnityPossiblyNegativeNumericLiteralExpression();
            var closeParenToken = Match(SyntaxKind.CloseParenToken);

            return new UnityStatePropertyConstantColorValueSyntax(
                openParenToken,
                r,
                firstCommaToken,
                g,
                secondCommaToken,
                b,
                thirdCommaToken,
                a,
                closeParenToken);
        }

        private UnityStatePropertySyntax ParseUnityMaterialDiffuse()
        {
            var keyword = Match(SyntaxKind.UnityDiffuseKeyword);
            var value = ParseUnityStatePropertyColorValue();

            return new UnityStatePropertyMaterialDiffuseSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityMaterialAmbient()
        {
            var keyword = Match(SyntaxKind.UnityAmbientKeyword);
            var value = ParseUnityStatePropertyColorValue();

            return new UnityStatePropertyMaterialAmbientSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityMaterialShininess()
        {
            var keyword = Match(SyntaxKind.UnityShininessKeyword);
            var value = ParseUnityStatePropertyValue(SyntaxKind.FloatLiteralToken, SyntaxKind.IntegerLiteralToken);

            return new UnityStatePropertyMaterialShininessSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityMaterialSpecular()
        {
            var keyword = Match(SyntaxKind.UnitySpecularKeyword);
            var value = ParseUnityStatePropertyColorValue();

            return new UnityStatePropertyMaterialSpecularSyntax(keyword, value);
        }

        private UnityStatePropertySyntax ParseUnityMaterialEmission()
        {
            var keyword = Match(SyntaxKind.UnityEmissionKeyword);
            var value = ParseUnityStatePropertyColorValue();

            return new UnityStatePropertyMaterialEmissionSyntax(keyword, value);
        }
    }
}