

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
        public async Task<bool> CreateUser(User user)
        {
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

            // Prepare user to insert
            user.Salt = pwdHelper.GetSalt();
            user.Password = pwdHelper.GetSaltedHash(user.Password, user.Salt);

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

        // Read
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
            var user = await GetUserById(await UserNameToId(name));
            var pwd = pwdHelper.GetSaltedHash(password, user.Salt);
            return (await Users.FindAsync(u => u.Name == name && u.Password == pwd)).FirstOrDefault();
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
        public Task UpdateUser(string name, string oldPassword, string newUsername = "", string newPassword = "")
        {
            throw new NotImplementedException();
        }

        public Task AddUserIngredient(string name, string password, List<Ingredient> ingredients)
        {
            throw new NotImplementedException();
        }
        // Delete
        public async Task<bool> DeleteUser(string name, string password)
        {
            var user = await GetUserById(await UserNameToId(name));

            if (pwdHelper.IsPwdValid(password, user.Salt, user.Password))
            {
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

            return false;
        }

        Task<bool> IUserCrud.AddUserIngredient(string name, string password, List<Ingredient> ingredients)
        {
            throw new NotImplementedException();
        }

        Task<bool> IUserCrud.UpdateUser(string name, string oldPassword, string newUsername, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserIngredient(string name, string password, List<Ingredient> ingredients)
        {
            throw new NotImplementedException();
        }
    }
}
