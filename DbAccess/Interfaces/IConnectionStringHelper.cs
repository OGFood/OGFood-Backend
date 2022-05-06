// -----------------------------------------------------------------------------------------------
//  IConnectionStringHelper.cs by Thomas Thorin, Copyright (C) 2022.
//  Published under GNU General Public License v3 (GPL-3)
// -----------------------------------------------------------------------------------------------

namespace DbAccess.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal interface IConnectionStringHelper
    {
        public string ConnectionString { get; }
    }
}
