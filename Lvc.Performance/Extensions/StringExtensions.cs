using Lvc.Performance.Algorithms.Strings;

namespace Lvc.Performance.Extensions
{
    public static class StringExtensions
    {
        public static int LongestCommonSubsequentLength(this string current, string str)
        {
            Validate.NotNullReference(current);

            return LongestCommonSubsequent.LongestCommonSubsequentLength(current, str);
        }
    }
}
