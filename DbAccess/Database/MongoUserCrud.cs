namespace DbAccess.Database
{
    using DbAccess.Interfaces;
    using SharedInterfaces.Models;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DbAccess.Helpers;

    public class MongoUserCrud : IUserCrud
    {
        private readonly IMongoCollection<User> Users;
        private readonly IPwdHelper pwdHelper;
        private readonly IMailHelper mailHelper;

        public MongoUserCrud(MongoDbAccess dbAccess)
        {
            Users = dbAccess.UserCollection;
            pwdHelper = new PwdHelper();
            mailHelper = new MailHelper();
        }

        // Create
        //=============================================================================================
        public async Task<bool> CreateUser(User user)
        {
            // Prevents accidental ID insertion from sources such as swagger
            user.Id = string.Empty;

            // Valid inputs?
            if (string.IsNullOrEmpty(user.Name) ||
                !mailHelper.IsMailValid(user.Mail) ||
                user.Password.Length < 12)
            {
                return false;
            }

            // Name/Mail taken?
            if (await IsNameOrMailTaken(user.Name, user.Name))
            {
                return false;
            }

            // Hash & Salt
            user.Salt = pwdHelper.GetSalt();
            user.Password = pwdHelper.GetSaltedHash(user.Password, user.Salt);

            // Insert user
            return Users.InsertOneAsync(user).IsCompletedSuccessfully;
        }

        // Read
        //=============================================================================================
        private async Task<User> GetUserById(string id)
        {
            return (await Users.FindAsync(u => u.Id == id)).FirstOrDefault()!;
        }

        public Task<User> GetUserByMail(string mail, string password)
        {
            throw new NotImplementedException();
        }

        private async Task<string?> UserNameToId(string name)
        {
            return (await Users.FindAsync(u => u.Name == name)).FirstOrDefault()?.Id ?? null;
        }

        public async Task<User> GetUserByName(string name, string password)
        {
            User user = await GetUserById(await UserNameToId(name));
            if (!pwdHelper.IsPwdValid(user, password))
            {
                return null;
            }

            //Scrub
            user.Password = String.Empty;
            user.Salt = String.Empty;

            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await (await Users.FindAsync(_ => true)).ToListAsync();
        }

        public async Task<bool> IsNameOrMailTaken(string name = "", string mail = "")
        {
            var userName = await Users.FindAsync(u => u.Name == name);
            var userMail = await Users.FindAsync(u => u.Mail == mail);

            return userName == null || userMail == null;
        }

        // Update
        //=============================================================================================
        public async Task<bool> UpdateUser(string name, string oldPassword,
            string newUsername, string newPassword, string newMail)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ReplaceUserIngredients(string name, string password,
            List<Ingredient> ingredients)
        {
            //TODO: add more safety
            var user = await GetUserById(await UserNameToId(name));

            if(pwdHelper.IsPwdValid(user, password))
            {
                user.Cupboard = ingredients;
                var result = await Users.ReplaceOneAsync(u => user.Id == u.Id, user);
                return result.IsAcknowledged;
            }
            else
            {
                return false;
            }
        }

        // Delete
        //=============================================================================================
        public async Task<bool> DeleteUser(string name, string password)
        {
            var user = await GetUserById(await UserNameToId(name));

            if (pwdHelper.IsPwdValid(user, password))
            {
                var result = await Users.DeleteOneAsync(u => u.Name == name);
                return result.IsAcknowledged;
            }

            return false;
        }
    }
}
