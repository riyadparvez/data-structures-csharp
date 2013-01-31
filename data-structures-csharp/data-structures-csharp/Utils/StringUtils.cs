using System;
using System.Diagnostics.Contracts;


namespace DataStructures.Utils
{
    public static class StringUtils
    {
        public static string CommonPrefix(this string str1, string str2)
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return str1.Substring(0, str1.CommonPrefixLength(str2));
        }

        public static int CommonPrefixLength(this string str1, string str2)
        {
            Contract.Requires<ArgumentNullException>(str2 != null);
            Contract.Ensures(Contract.Result<int>() >= 0);

            int count = 0;
            for (int i = 0; i < str1.Length && i < str2.Length; i++, count++)
            {
                if (str1[i] != str2[i])
                {
                    break;
                }
            }
            return count;
        }
    }
}
