namespace Lvc.Utils
{
	public static class Tools
	{
		public static void Swap<T>(
            ref T x, 
            ref T y)
		{
			T t = x;
			x = y;
			y = t;
		}
	}
}
