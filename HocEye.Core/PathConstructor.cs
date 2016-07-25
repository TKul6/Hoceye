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
            
            var startIndex =  line.LastIndexOf('.', position - 1, position - 1) + 1;


            //Todo: can optimize by iterating the string and add the characters to the builder instead of creating another string and call to substract.
            pathBuilder.Append(line.Substring(startIndex, endIndex - startIndex +1));

            //The start index represent the first letter in the word, which should be skipped as well as the ELEMENT_AEPARATOR if exists
            return Math.Max(0,startIndex -2);
        }

        private int GetEndIndex(string line, int position)
        {
            var wordEndPosition = line.IndexOfAny(ELEMENTS_SAPARATORS,position);

            if (wordEndPosition > 0)
            {
                return wordEndPosition -1;
            }
            
            return line.Length -1;
            

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
