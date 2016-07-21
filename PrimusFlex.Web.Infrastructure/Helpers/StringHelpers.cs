namespace PrimusFlex.Web.Infrastructure.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public static class StringHelpers
    {
        /// <summary>
        /// Extension method: Cut the string to specific character
        /// </summary>
        /// <param name="ch"></param>
        /// <returns>Return the substring from the given string to specified character. If string does not 
        /// contain the character method return the same whole string.
        /// </returns>
        public static string CutTo(this string str, char ch = '@')
        {
            if(str.Contains(ch.ToString()))
            {
                var len = str.IndexOf(ch);

                return str.Substring(0, len);
            }

            return str;
        }
    }
}
