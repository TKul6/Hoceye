using System;
using System.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;

namespace HocEye.Core
{
    public class PathConstructor
    {
       
        
        public string ConstructPathBackwards(string line, int position)
        {
            StringBuilder pathBuilder = new StringBuilder();

            //TODO: Handle quation mark as single varalable
            //Todo: Ignore white spaces


            if (char.IsLetterOrDigit(line[position]))
            {
                var startWord = ExtractWord(line, position);
            }

            return InnerConstructPathBackwards(line, position, pathBuilder);


        }

        private string ExtractWord(string line, int position)
        {
            throw new NotImplementedException();
        }

        private string InnerConstructPathBackwards(string line, int position, StringBuilder builder)
        {

            var previousDotLocation = line.LastIndexOf('.', position - 1);


            if (previousDotLocation < 0)
            {
                return builder.ToString();
            }
            var extentWord = line.Substring(previousDotLocation, position - previousDotLocation);

            builder.Insert(0, ".");
            builder.Insert(0, extentWord);

            return InnerConstructPathBackwards(line, previousDotLocation, builder);



            //TextExtent currentNameSnapshot = _textNavigator.GetExtentOfWord(new SnapshotPoint(currentSnapshot, position));

            //builder.Insert(0, currentNameSnapshot.Span.GetText());

            //var nextPosition = currentNameSnapshot.Span.Start.Position - 2;

            //if (nextPosition > 0)
            //{
            //    builder.Insert(0, ".");

            //    InnerConstructPathBackwards(currentSnapshot, nextPosition, builder);
            //}

            //return builder.ToString();

        }
    }
}
