using Lvc.Performance.Core.Algorithms.Strings;

namespace Lvc.Performance.Algorithms.Strings
{
	public class Kmp : IKmp
	{
		protected int[] _kmpHash;

		public string Substring { get; }

		public Kmp(string substring)
		{
			Validate.NotNull(substring, nameof(substring));

			Substring = substring;
			_kmpHash = GetKmpHash();
		}

		protected int[] GetKmpHash()
		{
			var j = 0;
			var length = Substring.Length;
			var kmpHash = new int[length];
			for (var i = 1; i < length; i++)
			{
				while (j > 0 && Substring[i] != Substring[j])
				{
					j = kmpHash[j - 1];
				}

				if (Substring[i] == Substring[j])
				{
					kmpHash[i] = ++j;
				}
			}

			return kmpHash;
		}

		public int Execute(string mainString)
		{
			Validate.NotNull(mainString, nameof(mainString));	

			var i2 = 0;
			var length = mainString.Length;
			for (var i1 = 0; i1 < length; i1++)
			{
				if (mainString[i1] != Substring[i2])
				{
					i2 = i2 > 0 ? _kmpHash[i2 - 1] : 0;
				}

				if (mainString[i1] == Substring[i2])
				{
					if (++i2 == Substring.Length)
						return i1 - i2 + 1;
				}
			}

			return -1;
		}
	}
}
