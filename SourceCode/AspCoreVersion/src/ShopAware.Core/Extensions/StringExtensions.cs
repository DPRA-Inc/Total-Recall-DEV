namespace ShopAware.Core.Extensions
{
    public static class StringExtensions
    {
        #region Public Methods

        /// <summary>
        ///     Use the current thread's culture info for conversion
        /// </summary>
        public static string ToTitleCase(this string value)
        {
            // If there are 0 or 1 characters, just return the string.
            if (value == null)
                return value;
            if (value.Length < 2)
                return value.ToUpper();

            // Start with the first character.
            var result = value.Substring(0, 1).
                               ToUpper();

            // Add the remaining characters.
            for (var i = 1; i < value.Length; i++)
            {
                if (char.IsUpper(value[i]))
                    result += " ";
                result += value[i];
            }

            return result;
        }

        #endregion
    }
}