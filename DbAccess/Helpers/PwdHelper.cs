namespace DbAccess.Helpers
{
    using System;
    using System.Security.Cryptography;
    using Interfaces;
    using System.Text;

    internal sealed class PwdHelper : IPwdHelper
    {
        private static PwdHelper? instance = null;
        public static PwdHelper Instance => instance ??= new PwdHelper();

        public bool IsPwdValid(string pwd, string salt, string saltedHash)
        {
            string newHashedPin = GetSaltedHash(pwd, salt);

            return newHashedPin.Equals(saltedHash);
        }

        public string GetSaltedHash(string pwd, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(pwd + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        public string GetSalt(int size = 32)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }
    }
}
