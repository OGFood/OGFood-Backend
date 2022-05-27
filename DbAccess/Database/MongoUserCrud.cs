

namespace DbAccess.Database
{
    using DbAccess.Interfaces;
    using DbAccess.Models;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class MongoUserCrud : IUserCrud
    {
        private readonly IMongoCollection<User> Users;
        private readonly IPwdHelper pwdHelper;
        private readonly IMailHelper mailHelper;

        public MongoUserCrud(MongoDbAccess dbAccess, IPwdHelper pwdHelper, IMailHelper mailHelper)
        {
            Users = dbAccess.UserCollection;
            this.pwdHelper = pwdHelper;
            this.mailHelper = mailHelper;
        }

        // Create
        public async Task<bool> CreateUser(string name, string mail, string password)
        {
            string salt;

            // Valid inputs?
            if (string.IsNullOrEmpty(name) ||
                !mailHelper.IsMailValid(mail) ||
                password.Length < 12)
            {
                return false;
            }

            // Name/Mail taken?
            if (IsNameOrMailTaken(name, mail).Result)
            {
                return false;
            }

            // Prepare user to insert
            salt = pwdHelper.GetSalt();
            User user = new(name, mail, salt, pwdHelper.GetSaltedHash(password, salt));

            // Insert user
            try
            {
                await Users.InsertOneAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> DeleteUser(string name, string password)
        {
            // Fix params
            if(pwdHelper.IsPwdValid(password))
            try
            {
                await Users.DeleteOneAsync(u => u.Name == name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<User> GetUserByMail(string mail, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByName(string name, string password)
        {
               
        }

        public async Task<bool> IsNameOrMailTaken(string name = "", string mail = "")
        {
            var userName = await Users.FindAsync(u => u.Name == name);
            var userMail = await Users.FindAsync(u => u.Mail == mail);

            return userName == null || userMail == null;
        }

        public Task UpdateUser(string name, string oldPassword, string newUsername = "", string newPassword = "")
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserIngredients(string name, string password)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserWithMail(string mail, string newUsername = "", string newPassword = "")
        {
            throw new NotImplementedException();
        }
    }
}
