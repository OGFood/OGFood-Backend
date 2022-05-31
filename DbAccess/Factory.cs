namespace DbAccess
{
    using Interfaces;
    using Helpers;
    using Database;

    public static class Factory
    {
        public static IConnectionStringHelper GetConnectionStringHelper() => ConnectionStringHelper.Instance;
        public static IPwdHelper GetPwdHelper() => PwdHelper.Instance;
        public static IMailHelper GetMailHelper() => MailHelper.Instance;

        internal static MongoDbAccess GetDbAccess() => new(GetConnectionStringHelper());
        public static IIngredientCrud GetIngredientCrud() => new MongoIngredientCrud(GetDbAccess());
        public static IUserCrud GetUserCrud() => new MongoUserCrud(GetDbAccess());
        public static IRecipeCrud GetRecipeCrud() => new MongoRecipeCrud(GetDbAccess());
        internal static RecipeHelper GetRecipeHelper() => new(GetIngredientCrud());
    }
}
