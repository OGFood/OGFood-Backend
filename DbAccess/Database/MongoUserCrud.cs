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
        public async Task<List<Result>> CreateUser(User user)
        {
            // Prevents accidental ID insertion from sources such as swagger
            user.Id = string.Empty;

            var result = new List<Result>
            {
                new Result() { Name = UserResult.CompletedSuccessfully, Success = true },
                new Result() { Name = UserResult.ValidName, Success = true },
                new Result() { Name = UserResult.ValidMail, Success = true },
                new Result() { Name = UserResult.PwdNotTooShort, Success = true},
                new Result() { Name = UserResult.PwdNotTooLong, Success = true},
            };

            // Valid inputs?
            if(string.IsNullOrEmpty(user.Name))
            {
                result[(int)UserResult.CompletedSuccessfully].Success = false;
                result[(int)UserResult.ValidName].Success = false;
            }
            if (!mailHelper.IsMailValid(user.Mail))
            {
                result[(int)UserResult.CompletedSuccessfully].Success = false;
                result[(int)UserResult.ValidMail].Success = false;
            }
            if(user.Password.Length < 8)
            {
                result[(int)UserResult.CompletedSuccessfully].Success = false;
                result[(int)UserResult.PwdNotTooShort].Success = false;
            }
            if(user.Password.Length > 20)
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
            if(nameOrMailTaken[1])
            {
                result[(int)UserResult.CompletedSuccessfully].Success = false;
                result[(int)UserResult.ValidMail].Success = false;
            }

            // Insert user
            if(result[(int)UserResult.CompletedSuccessfully].Success)
            {
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
            user.Password = String.Empty;
            user.Salt = String.Empty;

            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await (await Users.FindAsync(_ => true)).ToListAsync();
        }

        public async Task<bool[]> IsNameOrMailTaken(string name = "", string mail = "")
        {
            var userName = await Users.FindAsync(u => u.Name == name);
            var userMail = await Users.FindAsync(u => u.Mail == mail);

            return new bool[] { userName == null, userMail == null };
        }

        // Update
        //=============================================================================================
        public async Task<List<Result>> UpdateUser(User user)
        {
            // Gets user to update
            var userById = await GetUserById(await UserNameToId(user.Name));

            // success list
            var result = new List<Result>
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

            // Insert updated values
            userById.Cupboard = user.Cupboard;
            userById.Mail = user.Mail;
            userById.Name = user.Name;
            await Users.ReplaceOneAsync(u => userById.Id == u.Id, userById);
            return result;
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
