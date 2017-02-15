using System;
using System.Linq;

namespace Flirsty.DataAccess.Test.Helpers
{
    public static class StringGenerator
    {
        public static string GenerateTimeStamp()
        {
            return DateTime.Now.Ticks.ToString();
        }

        public static string GenerateEmailWithTimeStamp(string name, string domain)
        {
            return $"{name}_{GenerateTimeStamp()}@{domain}";
        }

        public static string GenerateNameWithTimeStamp(string name)
        {
            return $"{name}_{GenerateTimeStamp()}";
        }

        public static string GenerateRandomString(int length)
        {
            Random random = new Random();
            var chars = "abcdfgefhijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }
    }
}