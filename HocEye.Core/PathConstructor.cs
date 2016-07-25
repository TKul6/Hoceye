using System;
using System.Text;

namespace HocEye.Core
{
    public class PathConstructor
    {

        private readonly char[] PATH_SAPARATORS;

        private readonly char[] ELEMENTS_SAPARATORS;


        public PathConstructor()
        {
            PATH_SAPARATORS = new[] {':', '{', '}', '='};

            ELEMENTS_SAPARATORS = new[] { ':', '{', '}', '=','.' };
        }

        public string ConstructPathBackwards(string line, int position)
        {
            StringBuilder pathBuilder;

            //TODO: Handle quation mark as single varalable
            //Todo: Ignore white spaces


            pathBuilder = new StringBuilder(line.Length);

            if (char.IsLetterOrDigit(line[position]))
            {
                position = AppendFirstWord(line, position, pathBuilder);
            }


            return InnerConstructPathBackwards(line, position, pathBuilder);


        }

        internal int AppendFirstWord(string line, int position, StringBuilder pathBuilder)
        {
            var endIndex = GetEndIndex(line, position);

            //Todo: Handle end index  -1


            var startIndex = Math.Max(0, line.LastIndexOf('.', position - 1, position - 1));


            //Todo: can optimize by iterating the string and add the characters to the builder instead of creating another string and call to substract.
            var word = line.Substring(startIndex +1 , endIndex-startIndex -1);
            pathBuilder.Append(word);

            //The start index represent a dot, which should be skipped
            return startIndex -1;
        }

        private int GetEndIndex(string line, int position)
        {
            var wordEndPosition = line.IndexOfAny(ELEMENTS_SAPARATORS,position);

            if (wordEndPosition > 0)
            {
                return wordEndPosition;
            }

            

            return -1;
            //TODO: test where standing on the last word in the path which any saparator followed as next character as a ELEMENT_SAPARATOR
            //TODO: test where standing on the last word in the path

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
