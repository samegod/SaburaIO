namespace Additions.Extensions
{
	public static class FloatExtensions
	{
		public static bool IsBelowOrEqualZero(this float value) =>
			value <= 0;

		public static bool IsMoreZero(this float value) =>
			value > 0;
	}
}