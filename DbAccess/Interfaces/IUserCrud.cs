using SharedInterfaces.Models;

namespace DbAccess.Interfaces
{
    using Helpers;
    public interface IUserCrud
    {
        // Create
        public Task<List<Result>> CreateUser(User user);
        
        // Read
        public Task<User> GetUserByName(string name, string password);
        public Task<List<User>> GetAllUsers();

        // Update
        public Task<bool> ReplaceUserIngredients(string name, string password, List<Ingredient> ingredients);
        public Task<bool> UpdateUser(string name, string oldPassword, string newUsername = "", string newPassword = "", string newMail = "");

        // Delete
        public Task<bool> DeleteUser(string name, string password);
    }
}
