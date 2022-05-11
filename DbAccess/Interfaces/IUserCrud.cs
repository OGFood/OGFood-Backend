using DbAccess.Models;

namespace DbAccess.Interfaces
{
    public interface IUserCrud
    {
        // Create
        public Task<User> CreateUser(string name, string mail, string password);

        // Read
        public Task<User> GetUserByName(string name, string password);
        public Task<User> GetUserById(string id, string password);
        public Task<User> GetUserByMail(string mail, string password);

        // Update
        public Task<User> UpdateUser(string name, string oldPassword, string newUsername = "", string newPassword = "");
        public Task<User> UpdateUserWithMail(string mail, string newUsername = "", string newPassword = "");

        // Delete
        public Task<User> DeleteUser(string name, string password);
    }
}
