using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.Utilities;

namespace BraceLineShrinker
{
    [Export(typeof(ILineTransformSourceProvider))]
    [ContentType("CSharp")]
    [ContentType("C/C++")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    public class BraceLineTransformSourceProvider : ILineTransformSourceProvider
    {
        public ILineTransformSource Create(IWpfTextView textView) => new BraceLineTransformSource();
    }

    public class BraceLineTransformSource : ILineTransformSource
    {
        /// <summary>
        /// Scale factor of brace lines. Expose for another extension to change it if needed.
        /// </summary>
        public static double BraceLineScale { get; set; } = .45;
        public static Regex BraceMatchExpression { get; set; } = new Regex(@"^\s*(\{|\};?|\s)*\s*$");

        public LineTransform GetLineTransform(ITextViewLine line, double yPosition, ViewRelativePosition placement)
        {
            return BraceMatchExpression.IsMatch(line.Extent.GetText()) ? new LineTransform(BraceLineScale) : line.DefaultLineTransform;
        }
    }
}