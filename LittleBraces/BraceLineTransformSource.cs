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
        /// Factor to scale lines to. 1.0 = 100%
        /// </summary>
        public static double BraceLineScale { get; set; } = .45;
        /// <summary>
        /// Expression used to decide if lines should be squished.
        /// </summary>
        public static Regex BraceMatchExpression { get; set; } = new Regex(@"^\s*(\{|\};?|\s)*\s*$");
        public LineTransform GetLineTransform(ITextViewLine line, double yPosition, ViewRelativePosition placement) => BraceMatchExpression.IsMatch(line.Extent.GetText()) ? new LineTransform(BraceLineScale) : line.DefaultLineTransform;
    }
}