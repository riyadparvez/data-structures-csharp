using System.Diagnostics;


namespace DataStructures.Utils
{
    public static class StringUtils
    {
        public static string CommonPrefix(this string str1, string str2)
        {
            return str1.Substring(0, str1.CommonPrefixLength(str2));
        }

        public static int CommonPrefixLength(this string str1, string str2)
        {
            Debug.Assert(str2 != null);
            int count = 0;
            for (int i = 0; i < str1.Length && i < str2.Length; i++)
            {
                if (str1[i] != str2[i])
                {
                    count = i;
                    break;
                }
            }

            return count;
        }
    }
}
