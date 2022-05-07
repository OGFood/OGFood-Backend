﻿namespace DbAccess
{
    using Interfaces;
    using Helpers;
    using Database;

    public static class Factory
    {
        public static IConnectionStringHelper GetConnectionStringHelper() => ConnectionStringHelper.Instance;

        internal static MongoDbAccess GetDbAccess() => new(GetConnectionStringHelper());
        public static IIngredientCrud GetIngredientCrud() => new MongoIngredientCrud(GetDbAccess());
    }
}