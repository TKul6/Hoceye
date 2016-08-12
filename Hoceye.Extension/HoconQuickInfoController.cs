using System.Collections.Generic;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace Hoceye.Extension
{
    public class HoconQuickInfoController : IIntellisenseController
    {
        private ITextView _textView;
        private IList<ITextBuffer> _textBuffers;
        private HoconQuickInfoControllerProvider _provider;
        private IQuickInfoSession _session;

        public HoconQuickInfoController(ITextView textView, IList<ITextBuffer> textBuffers, HoconQuickInfoControllerProvider provider)
        {
            _textView = textView;
            _textBuffers = textBuffers;
            _provider = provider;

            _textView.MouseHover += TriggerQuickViewIfNecessary;
        }

        private void TriggerQuickViewIfNecessary(object sender, MouseHoverEventArgs e)
        {
            var point = _textView.BufferGraph.MapDownToFirstMatch(
                new SnapshotPoint(_textView.TextSnapshot, e.Position), PointTrackingMode.Positive,
                snapshot => _textBuffers.Contains(snapshot.TextBuffer), PositionAffinity.Predecessor);

            if (point != null)
            {
                var triggerPoint = point.Value.Snapshot.CreateTrackingPoint(point.Value.Position,
                    PointTrackingMode.Positive);


                if (!_provider.QuickInfoBroker.IsQuickInfoActive(_textView))
                {
                    _session = _provider.QuickInfoBroker.TriggerQuickInfo(_textView, triggerPoint, true);
                }
            }
        }

        public void Detach(ITextView textView)
        {
            if (_textView != null)
            {
                _textView.MouseHover -= TriggerQuickViewIfNecessary;
            }
        }

        public void ConnectSubjectBuffer(ITextBuffer subjectBuffer)
        {

        }

        public void DisconnectSubjectBuffer(ITextBuffer subjectBuffer)
        {

        }
    }
}