namespace TindaTrackAPI.Utils
{
    public class StringUtils
    {
        public static string ToPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return char.ToUpperInvariant(input[0]) + input.Substring(1);
        }
    }
}
