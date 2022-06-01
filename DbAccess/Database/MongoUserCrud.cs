namespace DbAccess.Database
{
    using DbAccess.Helpers;
    using DbAccess.Interfaces;
    using MongoDB.Driver;
    using SharedInterfaces.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
        public async Task<List<Result>> CreateUser(User user)
        {
            // Prevents accidental ID insertion from sources such as swagger
            user.Id = string.Empty;

            List<Result>? result = new List<Result>
            {
                new Result() { Name = UserResult.CompletedSuccessfully, Success = true },
                new Result() { Name = UserResult.ValidName, Success = true },
                new Result() { Name = UserResult.ValidMail, Success = true },
                new Result() { Name = UserResult.PwdNotTooShort, Success = true},
                new Result() { Name = UserResult.PwdNotTooLong, Success = true},
            };

            // Valid inputs?
            if (string.IsNullOrEmpty(user.Name))
            {
                result[(int)UserResult.CompletedSuccessfully].Success = false;
                result[(int)UserResult.ValidName].Success = false;
            }
            if (!mailHelper.IsMailValid(user.Mail))
            {
                result[(int)UserResult.CompletedSuccessfully].Success = false;
                result[(int)UserResult.ValidMail].Success = false;
            }
            if (user.Password.Length < 8)
            {
                result[(int)UserResult.CompletedSuccessfully].Success = false;
                result[(int)UserResult.PwdNotTooShort].Success = false;
            }
            if (user.Password.Length > 20)
            {
                result[(int)UserResult.CompletedSuccessfully].Success = false;
                result[(int)UserResult.PwdNotTooLong].Success = false;
            }

            // Name/Mail taken?
            bool[] nameOrMailTaken = await IsNameOrMailTaken(user.Name, user.Mail);
            if (nameOrMailTaken[0])
            {
                result[(int)UserResult.CompletedSuccessfully].Success = false;
                result[(int)UserResult.ValidName].Success = false;
            }
            if (nameOrMailTaken[1])
            {
                result[(int)UserResult.CompletedSuccessfully].Success = false;
                result[(int)UserResult.ValidMail].Success = false;
            }

            // Insert user
            if (result[(int)UserResult.CompletedSuccessfully].Success)
            {
                user.Salt = pwdHelper.GetSalt();
                user.Password = pwdHelper.GetSaltedHash(user.Password, user.Salt);
                await Users.InsertOneAsync(user);
            }

            return result;
        }

        // Read
        //=============================================================================================
        private async Task<User> GetUserById(string id)
        {
            return (await Users.FindAsync(u => u.Id == id)).FirstOrDefault();
        }

        private async Task<string?> UserNameToId(string name)
        {
            return (await Users.FindAsync(u => u.Name == name)).FirstOrDefault()?.Id;
        }

        public async Task<User> GetUserByName(string name, string password)
        {
            User user = await GetUserById(await UserNameToId(name));
            if (!pwdHelper.IsPwdValid(user, password))
            {
                return null;
            }

            //Scrub
            user.Password = string.Empty;
            user.Salt = string.Empty;

            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await (await Users.FindAsync(_ => true)).ToListAsync();
        }

        public async Task<bool[]> IsNameOrMailTaken(string name = "", string mail = "")
        {
            var userName = (await Users.FindAsync(u => u.Name == name)).FirstOrDefault();
            var userMail = (await Users.FindAsync(u => u.Mail == mail)).FirstOrDefault();

            return new bool[] { userName != null, userMail != null };
        }

        // Update
        //=============================================================================================
        public async Task<List<Result>> UpdateUser(User user)
        {
            // Gets user to update
            User userById = await GetUserById(user.Id);

            // Success list
            List<Result>? result = new List<Result>
            {
                new Result() { Name = UserResult.CompletedSuccessfully, Success = true },
                new Result() { Name = UserResult.ValidName, Success = true },
                new Result() { Name = UserResult.ValidMail, Success = true },
                new Result() { Name = UserResult.ValidPwd, Success = true}
            };

            // Valid pwd for user?
            if (!pwdHelper.IsPwdValid(userById, user.Password))
            {
                result[(int)UserResult.CompletedSuccessfully].Success = false;
                result[(int)UserResult.ValidPwd].Success = false;
                return result;
            }

            // Valid inputs?
            if (string.IsNullOrEmpty(user.Name))
            {
                result[(int)UserResult.CompletedSuccessfully].Success = false;
                result[(int)UserResult.ValidName].Success = false;
            }
            if (!mailHelper.IsMailValid(user.Mail))
            {
                result[(int)UserResult.CompletedSuccessfully].Success = false;
                result[(int)UserResult.ValidMail].Success = false;
            }

            // Name/Mail taken?
            bool[] nameOrMailTaken = await IsNameOrMailTaken(user.Name, user.Mail);
            if (nameOrMailTaken[0] && user.Name != userById.Name)
            {
                result[(int)UserResult.CompletedSuccessfully].Success = false;
                result[(int)UserResult.ValidName].Success = false;
            }
            if (nameOrMailTaken[1] && user.Mail != userById.Mail)
            {
                result[(int)UserResult.CompletedSuccessfully].Success = false;
                result[(int)UserResult.ValidMail].Success = false;
            }

            if (result[(int)UserResult.CompletedSuccessfully].Success)
            {
                user.Password = userById.Password;
                user.Salt = userById.Salt;
                await Users.ReplaceOneAsync(u => user.Id == u.Id, user);
            }
            return result;
        }

        // Delete
        //=============================================================================================
        public async Task<bool> DeleteUser(string name, string password)
        {
            User? user = await GetUserById(await UserNameToId(name));

            if (pwdHelper.IsPwdValid(user, password))
            {
                await Users.DeleteOneAsync(u => u.Name == name);
                return true;
            }

            return false;
        }
    }
}
