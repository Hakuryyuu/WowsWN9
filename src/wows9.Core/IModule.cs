using System;
using System.Collections.Generic;

namespace wows9.Core
{
    public interface IModule
    {
        string Name { get; }

        List<NavItem> NavItems { get; }

    }
}
