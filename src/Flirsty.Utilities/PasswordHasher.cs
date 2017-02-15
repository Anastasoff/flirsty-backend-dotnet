using System;
using System.Security.Cryptography;

namespace Flirsty.Utilities
{
    public static class PasswordHasher
    {
        private const int SaltByteSize = 32;
        private const int HashByteSize = 32;
        private const int Pbkdf2Iterations = 10000;
        private const int IterationIndex = 0;
        private const int SaltIndex = 1;
        private const int Pbkdf2Index = 2;

        private const char DelimiterCharacter = ':';

        public static string HashPassword(string password)
        {
            byte[] salt = GenerateSalt();
            byte[] hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);

            var saltStr = Convert.ToBase64String(salt);
            var hastStr = Convert.ToBase64String(hash);
            string hashedPassword = $"{Pbkdf2Iterations}:{saltStr}:{hastStr}";

            return hashedPassword;
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            char[] delimiter = {DelimiterCharacter};
            string[] split = correctHash.Split(delimiter);
            var iterations = int.Parse(split[IterationIndex]);
            byte[] salt = Convert.FromBase64String(split[SaltIndex]);
            byte[] hash = Convert.FromBase64String(split[Pbkdf2Index]);

            byte[] testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static byte[] GenerateSalt()
        {
            var salt = new byte[SaltByteSize];
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                cryptoProvider.GetBytes(salt);
            }

            return salt;
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }

            return diff == 0;
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return pbkdf2.GetBytes(outputBytes);
            }
        }
    }
}