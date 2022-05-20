namespace DbAccess.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IConnectionStringHelper
    {
        public string ConnectionString { get; }
    }
}
