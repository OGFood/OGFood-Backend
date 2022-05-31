namespace DbAccess.Interfaces
{
    public interface IPwdHelper
    {
        public string GetSaltedHash(string pwd, string salt);
        public string GetSalt(int size = 32);
        bool IsPwdValid(string pwd, string salt, string saltedHash);
    }
}
