namespace Wororo.Utilities
{
    public static class IntExtensions
    {
        public static bool IsEven(this int i) => i % 2 == 0;
        public static bool IsOdd(this int i) => !i.IsEven();
    }
}
