using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wows9.Core;

namespace wows9.Cal
{
    internal class Register : IModule
    {
        public string Name => "Calculation";
        public List<NavItem> NavItems => new List<NavItem> {
            new NavItem { Name = "Stats", Url = "/player" }
        };
    }
}
