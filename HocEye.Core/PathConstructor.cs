using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text.Differencing;

namespace HocEye.Core
{
    public class PathConstructor : IPathConstructor
    {

        private readonly char[] PATH_SAPARATORS;

        private readonly char[] ELEMENTS_SAPARATORS;

        private readonly char[] VALUE_INDICATORS;


        public PathConstructor()
        {
            PATH_SAPARATORS = new[] { '{', '}', };

            ELEMENTS_SAPARATORS = new[] { ':', '{', '}', '=', '.',' ','\t' };

            VALUE_INDICATORS = new[] {':', '='};
        }

        public string ConstructPathBackwards(IEnumerator<string> lines, int position)
        {
            StringBuilder pathBuilder;

            //TODO: Handle quation mark as single varalable
            //Todo: Ignore white spaces


            var line = GetCurrentLine(lines);

            if (String.IsNullOrEmpty(line))
            {
                return string.Empty;
            }

            pathBuilder = new StringBuilder(line.Length);

            if (char.IsLetterOrDigit(line[position]))
            {
                var endIndex = GetEndIndex(line,position);
                var word  = GetWord(line, position,endIndex);

                pathBuilder.Append(word);

                position = endIndex - word.Length -1; //going before the (.) char as well

            }
            
            return InnerConstructPathBackwards(lines, position, pathBuilder);
            
        }

        private string GetCurrentLine(IEnumerator<string> lines)
        {
            if (lines.Current == null)
            {
                if (lines.MoveNext())
                {
                    return lines.Current;
                }

                return string.Empty;
            }

            return lines.Current;
        }


        internal string GetWord(string line , int position,int endIndex)
        {
            
            if (endIndex <= 0 || ELEMENTS_SAPARATORS.Contains(line[endIndex])) 
            {
                return string.Empty;
            }
           
            var startIndex = line.LastIndexOfAny(ELEMENTS_SAPARATORS, position - 1, position) + 1;


            if (startIndex == endIndex)
            {
                //Enter this condition when having 2 element saperators one after the other

                return string.Empty;
            }

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

        private string InnerConstructPathBackwards(IEnumerator<string> lines , int position, StringBuilder pathBuilder)
        {

            position = RemoveWhiteSpace(lines.Current, position);

            if (position < 0)
            {
                if (lines.MoveNext())
                {

                    return InnerConstructPathBackwards(lines, lines.Current.Length - 1, pathBuilder);
                }
                //Id there is no more lines to process, the task is completed.
                return pathBuilder.ToString();
            }

            if (VALUE_INDICATORS.Contains(lines.Current[position]))
            {
                //The cursor is on a value, nothing to display

                return string.Empty;
            }

            if (PATH_SAPARATORS.Contains(lines.Current[position]))
            {
                //Id the current char is '{' or '}' the path require no more processing by the Path Constructor

                return pathBuilder.ToString();
            }

            var word = GetWord(lines.Current, position,position);

            var nextPosition = position - Math.Max(word.Length,1);

            if (!String.IsNullOrEmpty(word))
            {
                if (pathBuilder.Length > 0)
                {
                    pathBuilder.Insert(0, $"{word}.");
                }
                else
                {
                    pathBuilder.Insert(0,word);
                }
            }

            return InnerConstructPathBackwards(lines, nextPosition, pathBuilder);


        }

        private int RemoveWhiteSpace(string line, int position)
        {
            while (position > 0 && Char.IsWhiteSpace(line, position))
            {
                position --;
            }
            
            return position;
        
        }
    }
}
