using System.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;

namespace HocEye.Core
{
    public class PathConstructor
    {
        private ITextStructureNavigator _textNavigator;



        public PathConstructor(ITextStructureNavigator textNavigator)
        {
            _textNavigator = textNavigator;
        }

        public string ConstructPathBackwards(ITextSnapshot currentSnapshot, int position)
        {
            StringBuilder pathBuilder = new StringBuilder();

            return InnerConstructPathBackwards(currentSnapshot, position, pathBuilder);


        }

        private string InnerConstructPathBackwards(ITextSnapshot currentSnapshot, int position, StringBuilder builder)
        {

            var currentNameSnapshot = _textNavigator.GetExtentOfWord(new SnapshotPoint(currentSnapshot, position));

            builder.Insert(0, currentNameSnapshot.Span.GetText());

            var nextPosition = currentNameSnapshot.Span.Start.Position - 2;

            if (nextPosition > 0)
            {
                builder.Insert(0, ".");

                InnerConstructPathBackwards(currentSnapshot, nextPosition, builder);
            }

            return builder.ToString();

        }
    }
}
