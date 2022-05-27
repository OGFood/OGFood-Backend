using SharedInterfaces.Models;

namespace DbAccess.Interfaces
{
    public interface IUserCrud
    {
        // Create
        public Task<bool> CreateUser(string name, string mail, string password);

        // Read
        public Task<User> GetUserByName(string name, string password);
        public Task<User> GetUserById(string id);
        public Task<User> GetUserByMail(string mail, string password);

        // Update
        public Task<bool> UpdateUser(string name, string oldPassword, string newUsername = "", string newPassword = "");
        public Task<bool> UpdateUserWithMail(string mail, string newUsername = "", string newPassword = "");

        // Delete
        public Task<bool> DeleteUser(string name, string password);
    }
}
