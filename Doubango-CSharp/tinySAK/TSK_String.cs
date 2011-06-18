using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doubango.tinySAK
{
    public class TSK_String
    {
        public static bool Contains(String str, int len, String substring)
        {
            return TSK_String.IndexOf(str, len, substring) >= 0;
        }

        public static int IndexOf(String str, int len, String substring)
        {
            if (str != null && substring != null)
            {
                return str.IndexOf(substring, 0, len);
            }
            return -1;
        }

        public static void UnQuote(ref String str, char lquote, char rquote)
        {
            str = TSK_String.UnQuote(str, lquote, rquote);
        }

        public static String UnQuote(String str, char lquote, char rquote)
        {
            if (str != null)
            {
                int len = str.Length;
                if (len >= 2 && str[0] == lquote && str[len - 1] == rquote)
                {
                    return str.Substring(1, len - 2);
                }
            }
            return str;
        }

        public static void UnQuote(ref String str)
        {
            str = TSK_String.UnQuote(str, '"', '"');
        }

        public static String UnQuote(String str)
        {
            return TSK_String.UnQuote(str, '"', '"');
        }
    }
}
