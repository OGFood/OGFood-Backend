namespace DbAccess.Interfaces
{
    using SharedInterfaces.Models;
    public interface IPwdHelper
    {
        public string GetSaltedHash(string pwd, string salt);
        public string GetSalt(int size = 32);
        bool IsPwdValid(User user, string pwd);
    }
}
