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
            var properties = ParseUnityShaderProperties();

            var subShaders = new List<UnitySubShaderSyntax>();
            while (Current.Kind == SyntaxKind.UnitySubShaderKeyword)
                subShaders.Add(ParseUnitySubShader());

            var states = new List<UnityStateSyntax>();
            var shouldContinue = true;
            while (shouldContinue && Current.Kind != SyntaxKind.CloseBraceToken)
            {
                switch (Current.Kind)
                {
                    case SyntaxKind.UnityFallbackKeyword:
                        states.Add(ParseUnityFallback());
                        break;

                    default:
                        shouldContinue = false;
                        break;
                }
            }

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityShaderSyntax(shaderKeyword, nameToken, openBraceToken, properties, subShaders, states, closeBraceToken);
        }

        private UnityShaderPropertiesSyntax ParseUnityShaderProperties()
        {
            // TODO
            return null;
        }

        private UnitySubShaderSyntax ParseUnitySubShader()
        {
            var subShaderKeyword = Match(SyntaxKind.UnitySubShaderKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            var passes = new List<UnityPassSyntax>();
            while (Current.Kind == SyntaxKind.UnityPassKeyword)
                passes.Add(ParseUnityPass());

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnitySubShaderSyntax(subShaderKeyword, openBraceToken, passes, closeBraceToken);
        }

        private UnityPassSyntax ParseUnityPass()
        {
            var passKeyword = Match(SyntaxKind.UnityPassKeyword);
            var openBraceToken = Match(SyntaxKind.OpenBraceToken);

            var cgProgram = ParseUnityCgProgram();

            var closeBraceToken = Match(SyntaxKind.CloseBraceToken);

            return new UnityPassSyntax(passKeyword, openBraceToken, cgProgram, closeBraceToken);
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

        private UnityFallbackSyntax ParseUnityFallback()
        {
            var fallbackKeyword = Match(SyntaxKind.UnityFallbackKeyword);
            var value = (Current.Kind == SyntaxKind.StringLiteralToken)
                ? Match(SyntaxKind.StringLiteralToken)
                : Match(SyntaxKind.UnityOffKeyword);
            return new UnityFallbackSyntax(fallbackKeyword, value);
        }
    }
}