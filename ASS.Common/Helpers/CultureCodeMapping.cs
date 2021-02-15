using System;
using System.Collections.Generic;
using System.Text;

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
                case "de-DE":
                    return "Deutsche";
                default:
                    return "Ismeretlen országkód";
            }
        }
    }
}
