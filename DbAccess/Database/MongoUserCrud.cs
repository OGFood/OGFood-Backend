using DbAccess.Interfaces;
using DbAccess.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbAccess.Database
{
    internal class MongoUserCrud : IUserCrud
    {
        private readonly IMongoCollection<User> Users;
        public MongoUserCrud(MongoDbAccess dbAccess) => Users = dbAccess.UserCollection;

        // Create
        public async Task<User> CreateUser(string name, string mail, string password)
        {
            throw new NotImplementedException();
        }

        // Read
        public async Task<User> GetUserById(string id, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByMail(string mail, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByName(string name, string password)
        {
            throw new NotImplementedException();
        }

        // Update
        public async Task<User> UpdateUser(string name, string oldPassword, string newUsername = "", string newPassword = "")
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdateUserWithMail(string mail, string newUsername = "", string newPassword = "")
        {
            throw new NotImplementedException();
        }

        // Delete
        public async Task<User> DeleteUser(string name, string password)
        {
            throw new NotImplementedException();
        }
    }
}
