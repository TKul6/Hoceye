using System;
using System.Collections.Generic;
using System.Linq;
using HocEye.Core;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;

namespace Hoceye.Extension
{
    public class HoconQuickInfoSource : IQuickInfoSource
    {
        private HoconQuickInfoSourceProvider _provider;

        private ITextBuffer _buffer;

        private bool _isDisposing;

        private HoconRetriever _hoconRetriever;



        public HoconQuickInfoSource(HoconQuickInfoSourceProvider provider, ITextBuffer buffer)
        {
            _provider = provider;
            _buffer = buffer;

            var hoconContent = buffer.CurrentSnapshot.GetText();

            _hoconRetriever = new HoconRetriever(hoconContent, new PathConstructor());

        }

        public void Dispose()
        {
            if (!_isDisposing)
            {
                GC.SuppressFinalize(this);
                _isDisposing = true;
            }
        }

        public void AugmentQuickInfoSession(IQuickInfoSession session, IList<object> quickInfoContent, out ITrackingSpan applicableToSpan)
        {

            SnapshotPoint? subjectTriggerPoint = session.GetTriggerPoint(_buffer.CurrentSnapshot);

            if (!subjectTriggerPoint.HasValue)
            {
                applicableToSpan = null;
                return;
            }

            var snapshotLine = subjectTriggerPoint.Value.GetContainingLine().LineNumber;


            var triggerPoint = subjectTriggerPoint.Value;


            var lines = _buffer.CurrentSnapshot.Lines.Take(snapshotLine + 1).Select(line => line.GetText()).Reverse().GetEnumerator();

            var hocon = _hoconRetriever.GetHoconObject(lines, subjectTriggerPoint.Value.Position);


            if (!String.IsNullOrEmpty(hocon))
            {
                quickInfoContent.Add(hocon);

                applicableToSpan = subjectTriggerPoint.Value.Snapshot.CreateTrackingSpan(triggerPoint.Position, 5,
                    SpanTrackingMode.EdgeInclusive);
            }
            else
            {
                applicableToSpan = null;
            }
        }
    }
}
