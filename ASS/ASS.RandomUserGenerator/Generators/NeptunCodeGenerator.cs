using System;
using System.Collections.Generic;
using System.Text;

namespace ASS.RandomUserGenerator.Generators
{
    public static class NeptunCodeGenerator
    {
        private static readonly string charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random random = new Random();

        public static string GenerateNeptunCode()
        {
            int length = 6;
            StringBuilder builder = new StringBuilder();
            while (builder.Length < length)
            {
                builder.Append(charSet[random.Next(charSet.Length)]);
            }
            return builder.ToString();
        }
    }
}
