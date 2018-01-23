using System;

namespace Lvc.Performance.Algorithms.Strings
{
	internal static class LongestCommonSubsequent
	{
		internal static int LongestCommonSubsequentLength(string current, string str)
		{
			Validate.NotNull(current, nameof(str));
			Validate.NotNull(str, nameof(str));

			if (current == string.Empty || str == string.Empty)
				return 0;

			int[,] mat = GetLongestCommonSubsequentMat(current, str);

			return mat[current.Length, str.Length];
		}

		private static int[,] GetLongestCommonSubsequentMat(string current, string str)
		{
			var currentLength = current.Length;
			var strLength = str.Length;

			var mat = new int[currentLength + 1, strLength + 1];

			for (var i = 1; i <= currentLength; i++)
				for (var j = 1; j <= strLength; j++)
					mat[i, j] = current[i - 1] == str[j - 1]
						? mat[i - 1, j - 1] + 1
						: Math.Max(mat[i, j - 1], mat[i - 1, j]);

			return mat;
		}
	}
}
