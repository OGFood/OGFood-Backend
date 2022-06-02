namespace DbAccess.Helpers
{
    using Interfaces;
    using System.Net.Mail;

    internal sealed class MailHelper : IMailHelper
    {
        private static MailHelper? instance = null;
        public static MailHelper Instance => instance ??= new MailHelper();

        public bool IsMailValid(string mail)
        {
            try
            {
                MailAddress mailAddr = new(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
