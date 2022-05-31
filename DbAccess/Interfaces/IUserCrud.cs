using SharedInterfaces.Models;

namespace DbAccess.Interfaces
{
    public interface IUserCrud
    {
        // Create
        public Task<bool> CreateUser(User user);

        // Read
        public Task<User> GetUserByName(string name, string password);
        public Task<User> GetUserByMail(string mail, string password);

        // Update
        public Task<bool> ReplaceUserIngredients(string name, string password, List<Ingredient> ingredients);
        public Task<bool> UpdateUser(string name, string oldPassword, string newUsername = "", string newPassword = "", string newMail = "");

        // Delete
        public Task<bool> DeleteUser(string name, string password);
    }
}
