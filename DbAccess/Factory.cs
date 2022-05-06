namespace DbAccess
{
    using Interfaces;
    using Helpers;

    public static class Factory
    {
        public static IConnectionStringHelper GetConnectionStringHelper() => ConnectionStringHelper.Instance;
    }
}
