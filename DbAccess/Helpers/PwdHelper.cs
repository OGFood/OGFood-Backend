namespace DbAccess.Helpers
{
    using System;
    using System.Security.Cryptography;
    using Interfaces;
    using System.Text;
    using SharedInterfaces.Models;

    internal sealed class PwdHelper : IPwdHelper
    {
        private static PwdHelper? instance = null;
        public static PwdHelper Instance => instance ??= new PwdHelper();

        public bool IsPwdValid(User user, string pwd)
        {
            return user.Password == GetSaltedHash(pwd, user.Salt);
        }

        public string GetSaltedHash(string pwd, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(pwd + salt);
            var SHA256String = SHA256.Create();
            byte[] hash = SHA256String.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        public string GetSalt(int size = 32)
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }
    }
}
