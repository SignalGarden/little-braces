using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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
        public ILineTransformSource Create(IWpfTextView textView)
        {
            return new BraceLineTransformSource();
        }
    }

    public class BraceLineTransformSource : ILineTransformSource
    {
        /// <summary>
        /// Scale factor of brace lines. Expose for another extension to change it if needed.
        /// </summary>
        public static double? BraceLineScale { get; set; }
        public static Regex BraceMatchExpression = new Regex(@"^\s*(\{|\};?|\s)*\s*$");

        public LineTransform GetLineTransform(ITextViewLine line, double yPosition, ViewRelativePosition placement)
        {
            if (BraceMatchExpression.IsMatch(line.Extent.GetText()))
                return new LineTransform(BraceLineScale ?? .45);
            return line.DefaultLineTransform;
        }
    }
}