using System.Collections.Immutable;
using System.Text;
using HlslTools.Diagnostics;

namespace HlslTools.Syntax
{
    public sealed class EmptyExpandedMacroTrivia : LocatedNode
    {
        private readonly MacroReference _macroReference;

        internal EmptyExpandedMacroTrivia(MacroReference macroReference)
            : base(SyntaxKind.EmptyExpandedMacroTrivia, GetMacroReferenceText(macroReference), macroReference.Span, ImmutableArray<Diagnostic>.Empty)
        {
            _macroReference = macroReference;
            SourceRange = macroReference.SourceRange;
            FullSourceRange = macroReference.FullSourceRange;
        }

        private static string GetMacroReferenceText(MacroReference macroReference)
        {
            var sb = new StringBuilder();
            macroReference.WriteTo(sb, false, false, false, false);
            return sb.ToString();
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitEmptyExpandedMacroTrivia(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitEmptyExpandedMacroTrivia(this);
        }

        protected internal override void WriteTo(StringBuilder sb, bool leading, bool trailing, bool includeNonRootFile, bool ignoreMacroReferences)
        {
            _macroReference.WriteTo(sb, leading, trailing, includeNonRootFile, ignoreMacroReferences);
        }
    }
}