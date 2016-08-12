using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace Hoceye.Extension
{
    public class HoconQuickInfoControllerProvider
    {
        public IIntellisenseController TryCreateIntellisenseController(ITextView textView, IList<ITextBuffer> subjectBuffers)
        {
            return new HoconQuickInfoController(textView, subjectBuffers, this);
        }

        [Import]
        internal IQuickInfoBroker QuickInfoBroker { get; set; }
    }
}