namespace ASS.Common.Helpers
{
    public static class CultureCodeMapping
    {
        public static string CultureCodeToCountryName(string cultureCode)
        {
            switch (cultureCode)
            {
                case "hu-HU":
                    return "Magyar";
                case "en-US":
                    return "English";
                default:
                    return "Ismeretlen";
            }
        }
    }
}
