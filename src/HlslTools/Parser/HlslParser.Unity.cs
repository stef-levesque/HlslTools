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

            var stateProperties = new List<UnityCommandSyntax>();
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
            var vector = ParseUnityVector();

            return new UnityShaderPropertyVectorDefaultValueSyntax(vector);
        }

        private BaseUnityVectorSyntax ParseUnityVector()
        {
            var openParenToken = Match(SyntaxKind.OpenParenToken);
            var x = ParseUnityPossiblyNegativeNumericLiteralExpression();
            var firstCommaToken = Match(SyntaxKind.CommaToken);
            var y = ParseUnityPossiblyNegativeNumericLiteralExpression();
            var secondCommaToken = Match(SyntaxKind.CommaToken);
            var z = ParseUnityPossiblyNegativeNumericLiteralExpression();

            if (Current.Kind == SyntaxKind.CloseParenToken)
                return new UnityVector3Syntax(
                    openParenToken,
                    x,
                    firstCommaToken,
                    y,
                    secondCommaToken,
                    z,
                    NextToken());

            var thirdCommaToken = Match(SyntaxKind.CommaToken);
            var w = ParseUnityPossiblyNegativeNumericLiteralExpression();
            var closeParenToken = Match(SyntaxKind.CloseParenToken);

            return new UnityVector4Syntax(
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

            var options = new List<SyntaxToken>();
            while (Current.Kind != SyntaxKind.CloseBraceToken)
                options.Add(Match(SyntaxKind.IdentifierToken));

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityShaderPropertyTextureDefaultValueSyntax(
                defaultTextureToken,
                openBraceToken,
                options,
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

            _lexer.ResetDirectiveStack();

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
                case SyntaxKind.UnityFogKeyword:
                    stateProperties.Add(ParseUnityFog());
                    return true;
                case SyntaxKind.UnitySeparateSpecularKeyword:
                    stateProperties.Add(ParseUnitySeparateSpecular());
                    return true;
                case SyntaxKind.UnitySetTextureKeyword:
                    stateProperties.Add(ParseUnitySetTexture());
                    return true;
                case SyntaxKind.UnityAlphaTestKeyword:
                    stateProperties.Add(ParseUnityAlphaTest());
                    return true;
                case SyntaxKind.UnityAlphaToMaskKeyword:
                    stateProperties.Add(ParseUnityAlphaToMask());
                    return true;
                case SyntaxKind.UnityColorMaterialKeyword:
                    stateProperties.Add(ParseUnityColorMaterial());
                    return true;
                case SyntaxKind.UnityBindChannelsKeyword:
                    stateProperties.Add(ParseUnityBindChannels());
                    return true;

                default:
                    return false;
            }
        }

        private UnityCommandVariableValueSyntax ParseUnityCommandVariableValue()
        {
            var openBracketToken = NextToken();
            var identifier = Match(SyntaxKind.IdentifierToken);
            var closeBracketToken = Match(SyntaxKind.CloseBracketToken);

            return new UnityCommandVariableValueSyntax(
                openBracketToken,
                identifier,
                closeBracketToken);
        }

        private UnityCommandValueSyntax ParseUnityCommandValue(SyntaxKind preferred, params SyntaxKind[] otherOptions)
        {
            if (Current.Kind == SyntaxKind.OpenBracketToken)
                return ParseUnityCommandVariableValue();

            var valueToken = MatchOneOf(preferred, otherOptions);
            return new UnityCommandConstantValueSyntax(valueToken);
        }

        private UnityCommandSyntax ParseUnityFallback()
        {
            var keyword = Match(SyntaxKind.UnityFallbackKeyword);
            var value = MatchOneOf(SyntaxKind.IdentifierToken, SyntaxKind.StringLiteralToken);

            return new UnityCommandFallbackSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityCustomEditor()
        {
            var keyword = Match(SyntaxKind.UnityCustomEditorKeyword);
            var value = Match(SyntaxKind.StringLiteralToken);

            return new UnityCommandCustomEditorSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityDependency()
        {
            var keyword = Match(SyntaxKind.UnityDependencyKeyword);
            var name = Match(SyntaxKind.StringLiteralToken);
            var equalsToken = Match(SyntaxKind.EqualsToken);
            var dependentShaderToken = Match(SyntaxKind.StringLiteralToken);

            return new UnityCommandDependencySyntax(keyword, name, equalsToken, dependentShaderToken);
        }

        private UnityCommandSyntax ParseUnityCull()
        {
            var keyword = Match(SyntaxKind.UnityCullKeyword);
            var value = ParseUnityCommandValue(SyntaxKind.IdentifierToken);

            return new UnityCommandCullSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityZWrite()
        {
            var keyword = Match(SyntaxKind.UnityZWriteKeyword);
            var value = ParseUnityCommandValue(SyntaxKind.IdentifierToken);

            return new UnityCommandZWriteSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityZTest()
        {
            var keyword = Match(SyntaxKind.UnityZTestKeyword);
            var value = ParseUnityCommandValue(SyntaxKind.IdentifierToken);

            return new UnityCommandZTestSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityBlend()
        {
            var keyword = Match(SyntaxKind.UnityBlendKeyword);

            if (Current.Kind == SyntaxKind.IdentifierToken && string.Equals(Current.Text, "Off", StringComparison.OrdinalIgnoreCase))
            {
                return new UnityCommandBlendOffSyntax(keyword, NextToken());
            }

            var srcFactor = ParseUnityCommandValue(SyntaxKind.IdentifierToken);
            var dstFactor = ParseUnityCommandValue(SyntaxKind.IdentifierToken);

            if (Current.Kind == SyntaxKind.CommaToken)
            {
                var commaToken = Match(SyntaxKind.CommaToken);
                var srcFactorA = ParseUnityCommandValue(SyntaxKind.IdentifierToken);
                var dstFactorA = ParseUnityCommandValue(SyntaxKind.IdentifierToken);

                return new UnityCommandBlendColorAlphaSyntax(
                    keyword,
                    srcFactor,
                    dstFactor,
                    commaToken,
                    srcFactorA,
                    dstFactorA);
            }

            return new UnityCommandBlendColorSyntax(
                keyword,
                srcFactor,
                dstFactor);
        }

        private UnityCommandSyntax ParseUnityColorMask()
        {
            var keyword = Match(SyntaxKind.UnityColorMaskKeyword);
            var maskToken = ParseUnityCommandValue(
                SyntaxKind.IdentifierToken,
                SyntaxKind.IntegerLiteralToken);

            return new UnityCommandColorMaskSyntax(keyword, maskToken);
        }

        private UnityCommandSyntax ParseUnityLod()
        {
            var keyword = Match(SyntaxKind.UnityLodKeyword);
            var value = ParseUnityCommandValue(SyntaxKind.IntegerLiteralToken);

            return new UnityCommandLodSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityName()
        {
            var keyword = Match(SyntaxKind.UnityNameKeyword);
            var value = Match(SyntaxKind.StringLiteralToken);

            return new UnityCommandNameSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityLighting()
        {
            var keyword = Match(SyntaxKind.UnityLightingKeyword);
            var value = Match(SyntaxKind.IdentifierToken);

            return new UnityCommandLightingSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityOffset()
        {
            var keyword = Match(SyntaxKind.UnityOffsetKeyword);
            var factor = ParseUnityCommandValue(SyntaxKind.FloatLiteralToken, SyntaxKind.IntegerLiteralToken);
            var commaToken = Match(SyntaxKind.CommaToken);
            var units = ParseUnityCommandValue(SyntaxKind.FloatLiteralToken, SyntaxKind.IntegerLiteralToken);

            return new UnityCommandOffsetSyntax(keyword, factor, commaToken, units);
        }

        private UnityCommandSyntax ParseUnityStencil()
        {
            var keyword = Match(SyntaxKind.UnityStencilKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            var stateProperties = new List<UnityCommandSyntax>();
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
                    case SyntaxKind.UnityCompBackKeyword:
                    case SyntaxKind.UnityCompFrontKeyword:
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

            return new UnityCommandStencilSyntax(
                keyword, 
                openBraceToken, 
                stateProperties, 
                closeBraceToken);
        }

        private UnityCommandSyntax ParseUnityStencilRef()
        {
            var keyword = Match(SyntaxKind.UnityRefKeyword);
            var value = ParseUnityCommandValue(SyntaxKind.IntegerLiteralToken);

            return new UnityCommandStencilRefSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityStencilReadMask()
        {
            var keyword = Match(SyntaxKind.UnityReadMaskKeyword);
            var value = ParseUnityCommandValue(SyntaxKind.IntegerLiteralToken);

            return new UnityCommandStencilReadMaskSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityStencilWriteMask()
        {
            var keyword = Match(SyntaxKind.UnityWriteMaskKeyword);
            var value = ParseUnityCommandValue(SyntaxKind.IntegerLiteralToken);

            return new UnityCommandStencilWriteMaskSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityStencilComp()
        {
            var keyword = NextToken();
            var value = ParseUnityCommandValue(SyntaxKind.IdentifierToken);

            return new UnityCommandStencilCompSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityStencilPass()
        {
            var keyword = Match(SyntaxKind.UnityPassKeyword);
            var value = ParseUnityCommandValue(SyntaxKind.IdentifierToken);

            return new UnityCommandStencilPassSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityStencilFail()
        {
            var keyword = Match(SyntaxKind.UnityFailKeyword);
            var value = ParseUnityCommandValue(SyntaxKind.IdentifierToken);

            return new UnityCommandStencilFailSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityStencilZFail()
        {
            var keyword = Match(SyntaxKind.UnityZFailKeyword);
            var value = ParseUnityCommandValue(SyntaxKind.IdentifierToken);

            return new UnityCommandStencilZFailSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityMaterial()
        {
            var keyword = Match(SyntaxKind.UnityMaterialKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            var stateProperties = new List<UnityCommandSyntax>();
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

            return new UnityCommandMaterialSyntax(
                keyword,
                openBraceToken,
                stateProperties,
                closeBraceToken);
        }

        private UnityCommandValueSyntax ParseUnityCommandColorValue()
        {
            if (Current.Kind == SyntaxKind.OpenBracketToken)
                return ParseUnityCommandVariableValue();

            var vector = ParseUnityVector();
            return new UnityCommandConstantColorValueSyntax(vector);
        }

        private UnityCommandSyntax ParseUnityMaterialDiffuse()
        {
            var keyword = Match(SyntaxKind.UnityDiffuseKeyword);
            var value = ParseUnityCommandColorValue();

            return new UnityCommandMaterialDiffuseSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityMaterialAmbient()
        {
            var keyword = Match(SyntaxKind.UnityAmbientKeyword);
            var value = ParseUnityCommandColorValue();

            return new UnityCommandMaterialAmbientSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityMaterialShininess()
        {
            var keyword = Match(SyntaxKind.UnityShininessKeyword);
            var value = ParseUnityCommandValue(SyntaxKind.FloatLiteralToken, SyntaxKind.IntegerLiteralToken);

            return new UnityCommandMaterialShininessSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityMaterialSpecular()
        {
            var keyword = Match(SyntaxKind.UnitySpecularKeyword);
            var value = ParseUnityCommandColorValue();

            return new UnityCommandMaterialSpecularSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityMaterialEmission()
        {
            var keyword = Match(SyntaxKind.UnityEmissionKeyword);
            var value = ParseUnityCommandColorValue();

            return new UnityCommandMaterialEmissionSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityFog()
        {
            var keyword = Match(SyntaxKind.UnityFogKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            var commands = new List<UnityCommandSyntax>();
            var shouldContinue = true;
            while (shouldContinue && Current.Kind != SyntaxKind.CloseBraceToken)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnityModeKeyword:
                        commands.Add(ParseUnityFogMode());
                        break;
                    case SyntaxKind.UnityColorKeyword:
                        commands.Add(ParseUnityFogColor());
                        break;
                    case SyntaxKind.UnityDensityKeyword:
                        commands.Add(ParseUnityFogDensity());
                        break;
                    case SyntaxKind.UnityRangeKeyword:
                        commands.Add(ParseUnityFogRange());
                        break;

                    default:
                        shouldContinue = false;
                        break;
                }
            }

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityCommandFogSyntax(
                keyword,
                openBraceToken,
                commands,
                closeBraceToken);
        }

        private UnityCommandSyntax ParseUnityFogMode()
        {
            var keyword = Match(SyntaxKind.UnityModeKeyword);
            var value = ParseUnityCommandValue(SyntaxKind.IdentifierToken);

            return new UnityCommandFogModeSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityFogColor()
        {
            var keyword = Match(SyntaxKind.UnityColorKeyword);
            var value = ParseUnityCommandColorValue();

            return new UnityCommandFogColorSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityFogDensity()
        {
            var keyword = Match(SyntaxKind.UnityDensityKeyword);
            var value = ParseUnityCommandValue(SyntaxKind.FloatLiteralToken, SyntaxKind.IntegerLiteralToken);

            return new UnityCommandFogDensitySyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityFogRange()
        {
            var keyword = Match(SyntaxKind.UnityRangeKeyword);
            var near = ParseUnityCommandValue(SyntaxKind.FloatLiteralToken, SyntaxKind.IntegerLiteralToken);
            var commaToken = Match(SyntaxKind.CommaToken);
            var far = ParseUnityCommandValue(SyntaxKind.FloatLiteralToken, SyntaxKind.IntegerLiteralToken);

            return new UnityCommandFogRangeSyntax(keyword, near, commaToken, far);
        }

        private UnityCommandSyntax ParseUnitySeparateSpecular()
        {
            var keyword = Match(SyntaxKind.UnitySeparateSpecularKeyword);
            var value = ParseUnityCommandValue(SyntaxKind.IdentifierToken);

            return new UnityCommandSeparateSpecularSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnitySetTexture()
        {
            var keyword = Match(SyntaxKind.UnitySetTextureKeyword);
            var textureName = ParseUnityCommandVariableValue();
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            var commands = new List<UnityCommandSyntax>();
            var shouldContinue = true;
            while (shouldContinue && Current.Kind != SyntaxKind.CloseBraceToken)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnityConstantColorKeyword:
                        commands.Add(ParseUnitySetTextureConstantColor());
                        break;
                    case SyntaxKind.UnityMatrixKeyword:
                        commands.Add(ParseUnitySetTextureMatrix());
                        break;
                    case SyntaxKind.UnityCombineKeyword:
                        commands.Add(ParseUnitySetTextureCombine());
                        break;

                    default:
                        shouldContinue = false;
                        break;
                }
            }

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityCommandSetTextureSyntax(
                keyword,
                textureName,
                openBraceToken,
                commands,
                closeBraceToken);
        }

        private UnityCommandSyntax ParseUnitySetTextureConstantColor()
        {
            var keyword = Match(SyntaxKind.UnityConstantColorKeyword);
            var value = ParseUnityCommandColorValue();

            return new UnityCommandSetTextureConstantColorSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnitySetTextureMatrix()
        {
            var keyword = Match(SyntaxKind.UnityMatrixKeyword);
            var value = ParseUnityCommandVariableValue();

            return new UnityCommandSetTextureMatrixSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnitySetTextureCombine()
        {
            var keyword = Match(SyntaxKind.UnityCombineKeyword);
            var value = ParseUnitySetTextureCombineValue();

            SyntaxToken modifierToken = null;
            if (Current.Kind == SyntaxKind.UnityDoubleKeyword || Current.Kind == SyntaxKind.UnityQuadKeyword)
                modifierToken = NextToken();

            return new UnityCommandSetTextureCombineSyntax(keyword, value, modifierToken);
        }

        private BaseUnityCommandSetTextureCombineValueSyntax ParseUnitySetTextureCombineValue()
        {
            var source1 = Match(SyntaxKind.IdentifierToken);

            switch (Current.Kind)
            {
                case SyntaxKind.UnityLerpKeyword:
                    return ParseUnitySetTextureCombineLerpValue(source1);

                default:
                    var operatorToken = MatchOneOf(SyntaxKind.AsteriskToken, SyntaxKind.PlusToken, SyntaxKind.MinusToken);
                    var source2 = Match(SyntaxKind.IdentifierToken);

                    if (operatorToken.Kind == SyntaxKind.AsteriskToken && Current.Kind == SyntaxKind.PlusToken)
                        return ParseUnitySetTextureCombineMultiplyAlphaValue(source1, operatorToken, source2);

                    return new UnityCommandSetTextureCombineBinaryValueSyntax(source1, operatorToken, source2);
            }
        }

        private BaseUnityCommandSetTextureCombineValueSyntax ParseUnitySetTextureCombineLerpValue(SyntaxToken source1)
        {
            var lerpKeyword = Match(SyntaxKind.UnityLerpKeyword);
            var openParenToken = Match(SyntaxKind.OpenParenToken);
            var source2 = Match(SyntaxKind.IdentifierToken);
            var closeParenToken = Match(SyntaxKind.CloseParenToken);
            var source3 = Match(SyntaxKind.IdentifierToken);

            return new UnityCommandSetTextureCombineLerpValueSyntax(
                source1,
                lerpKeyword,
                openParenToken,
                source2,
                closeParenToken,
                source3);
        }

        private BaseUnityCommandSetTextureCombineValueSyntax ParseUnitySetTextureCombineMultiplyAlphaValue(SyntaxToken source1, SyntaxToken operatorToken, SyntaxToken source2)
        {
            var plusToken = Match(SyntaxKind.PlusToken);
            var source3 = Match(SyntaxKind.IdentifierToken);

            return new UnityCommandSetTextureCombineMultiplyAlphaValueSyntax(
                source1,
                operatorToken,
                source2,
                plusToken,
                source3);
        }

        private UnityCommandSyntax ParseUnityAlphaTest()
        {
            var keyword = Match(SyntaxKind.UnityAlphaTestKeyword);
            var identifier = Match(SyntaxKind.IdentifierToken);

            if (string.Equals(identifier.Text, "Off", StringComparison.OrdinalIgnoreCase))
                return new UnityCommandAlphaTestOffSyntax(keyword, identifier);

            var alphaValue = ParseUnityCommandValue(SyntaxKind.FloatLiteralToken, SyntaxKind.IntegerLiteralToken);
            return new UnityCommandAlphaTestComparisonSyntax(keyword, identifier, alphaValue);
        }

        private UnityCommandSyntax ParseUnityAlphaToMask()
        {
            var keyword = Match(SyntaxKind.UnityAlphaToMaskKeyword);
            var value = ParseUnityCommandValue(SyntaxKind.IdentifierToken);

            return new UnityCommandAlphaToMaskSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityColorMaterial()
        {
            var keyword = Match(SyntaxKind.UnityColorMaterialKeyword);
            var value = Match(SyntaxKind.IdentifierToken);

            return new UnityCommandColorMaterialSyntax(keyword, value);
        }

        private UnityCommandSyntax ParseUnityBindChannels()
        {
            var keyword = Match(SyntaxKind.UnityBindChannelsKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            var commands = new List<UnityCommandSyntax>();
            while (Current.Kind == SyntaxKind.UnityBindKeyword)
                commands.Add(ParseUnityBind());

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityCommandBindChannelsSyntax(
                keyword,
                openBraceToken,
                commands,
                closeBraceToken);
        }

        private UnityCommandSyntax ParseUnityBind()
        {
            var keyword = Match(SyntaxKind.UnityBindKeyword);
            var sourceToken = Match(SyntaxKind.StringLiteralToken);
            var commaToken = Match(SyntaxKind.CommaToken);
            var targetToken = MatchOneOf(SyntaxKind.IdentifierToken, SyntaxKind.UnityColorKeyword);
            if (targetToken.Kind == SyntaxKind.UnityColorKeyword)
                targetToken = targetToken.WithKind(SyntaxKind.IdentifierToken);

            return new UnityCommandBindChannelsBindSyntax(keyword, sourceToken, commaToken, targetToken);
        }
    }
}