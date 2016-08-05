using System;
using System.Text;
using Microsoft.VisualStudio.Text.Differencing;

namespace HocEye.Core
{
    public class PathConstructor
    {

        private readonly char[] PATH_SAPARATORS;

        private readonly char[] ELEMENTS_SAPARATORS;


        public PathConstructor()
        {
            PATH_SAPARATORS = new[] { ':', '{', '}', '=' };

            ELEMENTS_SAPARATORS = new[] { ':', '{', '}', '=', '.' };
        }

        public string ConstructPathBackwards(string line, int position)
        {
            StringBuilder pathBuilder;

            //TODO: Handle quation mark as single varalable
            //Todo: Ignore white spaces


            pathBuilder = new StringBuilder(line.Length);

            if (char.IsLetterOrDigit(line[position]))
            {
                var endIndex = GetEndIndex(line,position);
                var word  = GetWord(line, position,endIndex);

                pathBuilder.Append(word);

                position = endIndex - word.Length;

            }
            
            return InnerConstructPathBackwards(line, position, pathBuilder);
            
        }

     

        internal string GetWord(string line, int position,int endIndex)
        {

            if (endIndex <= 0)
            {
                return string.Empty;
            }
            
            var startIndex = line.LastIndexOfAny(ELEMENTS_SAPARATORS, position - 1, position) + 1;


            //Todo: can optimize by iterating the string and add the characters to the pathBuilder instead of creating another string and call to substract.
            var word = line.Substring(startIndex, endIndex - startIndex + 1);
            
            //The start index represent the first letter in the word, which should be skipped as well as the ELEMENT_AEPARATOR if exists
            return word;
        }

        private int GetEndIndex(string line, int position)
        {
            var wordEndPosition = line.IndexOfAny(ELEMENTS_SAPARATORS, position);

            if (wordEndPosition > 0)
            {
                return wordEndPosition - 1;
            }

            return line.Length - 1;


        }

        internal string GetWord(string line, int position)
        {
            var endIndex = GetEndIndex(line, position);

            return GetWord(line, position, endIndex);
        }

        private string InnerConstructPathBackwards(string line, int position, StringBuilder pathBuilder)
        {

            if (position == 0)
            {
                return pathBuilder.ToString();
            }

            var word = GetWord(line, position,position);

            if (!String.IsNullOrEmpty(word))
            {
                var nextPosition = Math.Max(position - word.Length - 2, 0);
                pathBuilder.Insert(0, $"{word}.");


                //Todo: handle where just '}' left    
                return InnerConstructPathBackwards(line,nextPosition,pathBuilder);
            }

            return pathBuilder.ToString();
            
            
        }
    }
}
