using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wows9.Cal;
public class Get
{
    public static Player PlayerStats(string Name)
    {
        Calculate _Cal = new Calculate();

        DataProvider _dp = new DataProvider();

       Player player = _dp.PlayerData(Name);
        //_dp.GetExpectedValues();

        return player;
    }
}

