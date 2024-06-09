namespace CodeBase
{
    public static class Extensions
    {
        public static bool IsCorrect(this string input)
        {
            return !string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input);
        }
    }
}