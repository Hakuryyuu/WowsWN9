using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wows9.Cal;
public class Player
{
    public string Name { get; set; }
    public int ID { get; set; }
    public string Server { get; set; }
    public string CurrentClan { get; set; }
    public double WinrateOverall { get; set; }
    public int WN9 { get; set; }
    public double AvgDmg { get; set; }
    public string LastTimePlayed { get; set; }
    public Ship[] Warships { get; set; }
}

