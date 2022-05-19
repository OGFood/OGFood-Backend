namespace DbAccess
{
    using Interfaces;
    using Helpers;
    using Database;

    public class DbServiceContext
    {
        public static IConnectionStringHelper GetConnectionStringHelper() => ConnectionStringHelper.Instance;

        internal static MongoDbAccess GetDbAccess() => new(GetConnectionStringHelper());
        public static IIngredientCrud GetIngredientCrud() => new MongoIngredientCrud(GetDbAccess());
        public static IRecipeCrud GetRecipeCrud() => new MongoRecipeCrud(GetDbAccess());
    }
}
