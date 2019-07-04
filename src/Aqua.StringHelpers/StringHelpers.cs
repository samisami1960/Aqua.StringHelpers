﻿using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Aqua.StringHelpers
{
    public static class StringHelpers
    {
        /// <summary>
        /// Is Null Or Empty String?
        /// </summary>
        /// <param name="s">string</param>
        /// <returns>true/false</returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Is Null Or White Space String?
        /// </summary>
        /// <param name="s">string</param>
        /// <returns>true/false</returns>
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// Is the string alphanumeric?
        /// </summary>
        /// <param name="s">text</param>
        /// <returns>true/false</returns>
        public static bool IsAlphaNumeric(this string s)
        {
            if (s.IsNullOrEmpty())
                return false;

            Regex reg_exp = new Regex("[^a-zA-Z0-9]", RegexOptions.Compiled);

            return !reg_exp.IsMatch(s); ;
        }

        /// <summary>
        /// Reverse the input string
        /// </summary>
        /// <param name="s">text</param>
        /// <returns>reversed text</returns>
        public static string Reverse(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                return s;
            }

            char[] chars = s.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// Remove Extra Spaces from the text
        /// </summary>
        /// <param name="s">text</param>
        /// <returns>clean text</returns>
        public static string RemoveExtraSpaces(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                return s;
            }

            string pattern = "\\s+";
            string replacement = " ";

            Regex rx = new Regex(pattern);

            return rx.Replace(s, replacement).Trim();
        }

        /// <summary>
        /// Replace the Tabs with Spaces and remove the extra spaces
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ReplaceTabsWithSpaces(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                return s;
            }

            return s.Replace("\t", " ").RemoveExtraSpaces();
        }

        /// <summary>
        /// Replace the New Line Chars with Spaces and remove the extra spaces
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ReplaceNewLinesWithSpaces(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                return s;
            }

            return s.Replace("\n", " ").RemoveExtraSpaces();
        }

        /// <summary>
        /// Clean the text from the tabs, New Line Chars, and Extra Spaces
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToCleanString(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                return s;
            }

            return s.Replace("\n", " ").Replace("\t", " ").RemoveExtraSpaces();
        }

        /// <summary>
        /// Capitalise Each Word
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string CapitaliseEachWord(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                return s;
            }

            var x = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());

            return x;
        }

        /// <summary>
        /// Finds the number of charachters in a string - include/exclude extra spaces, tabs and new line characters
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int GetTotalNumberOfCharachters(this string s, bool clean = false)
        {
            if (s.IsNullOrEmpty())
                return 0;

            return clean != true ? s.Length : s.ToCleanString().Length;
        }

        /// <summary>
        /// Returns Sentence Case of a text
        /// </summary>
        /// <param name="s"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public static string ToSentenceCase(this string s, char seperator)
        {
            if (s.IsNullOrEmpty())
                return s;

            s = s.Trim().ToCleanString();
            if (s.IsNullOrEmpty())
                return s;

            // Only 1 seperator
            if (s.IndexOf(seperator) < 0)
            {
                s = s.ToLower();
                s = s[0].ToString().ToUpper() + s.Substring(1);
                return s;
            }

            if (s.Trim().Last() == seperator)
            {
                s.Trim().Remove(s.Length - 1, 1);
            }

            // More than 1 seperator.
            string[] sentences = s.Split(seperator);
            StringBuilder buffer = new StringBuilder();

            foreach (string sentence in sentences)
            {
                string currentSentence = sentence.ToLower().Trim();
                if (!string.IsNullOrWhiteSpace(currentSentence))
                {
                    currentSentence = currentSentence[0].ToString().ToUpper() + currentSentence.Substring(1);
                    buffer.Append(currentSentence + seperator + ' ');
                }
            }

            s = buffer.ToString();
            return s.Trim();
        }

        /// <summary>
        /// Finds the number of words in a string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int GetTotalNumberOfWords(this string s)
        {
            if (s.IsNullOrWhiteSpace())
                return 0;

            int result = 1;

            s = s.ToCleanString();

            for (int i = 0; i <= s.Length - 1; i++)
            {
                if (s[i] == ' ')
                {
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// To Abbreviation
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToAbbreviation(this string s)
        {
            if (s.IsNullOrWhiteSpace())
            {
                return s;
            }

            string result = string.Empty;

            string[] words = s.ToCleanString().Split(' ');

            foreach (string word in words)
            {
                result += char.ToUpper(word[0]);
            }

            return result;
        }

    }
}
