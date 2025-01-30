using System;
using System.Collections.Generic;

namespace TestTasks.VowelCounting
{
    public class StringProcessor
    {
        public (char symbol, int count)[] GetCharCount(string veryLongString, char[] countedChars)
        {
            if (veryLongString == null || countedChars == null)
                throw new ArgumentNullException();

            var charCountDict = new Dictionary<char, int>();

            foreach (var c in countedChars)
            {
                charCountDict[c] = 0;
            }

            foreach (var c in veryLongString)
            {
                if (charCountDict.ContainsKey(c))
                {
                    charCountDict[c]++;
                }
            }

            var result = new (char symbol, int count)[countedChars.Length];
            for (int i = 0; i < countedChars.Length; i++)
            {
                result[i] = (countedChars[i], charCountDict[countedChars[i]]);
            }

            return result;
        }
    }
}
