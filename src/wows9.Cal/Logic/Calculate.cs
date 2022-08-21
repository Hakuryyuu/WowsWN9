using System.Linq;

namespace wows9.Cal;
internal class Calculate
{
    public double CalcShipWN9(Ship Ship, ExpVals expVals, double multiplier){

    // AVERAGE STATS FOR TIER
    TierAvg _ta = new TierAvg();

        //DETERMITE TIER
        for (int i = 0; i < Constants.TIERAVG.Count; i++)
        {
            if (Ship.Tier == i)
            {
                _ta = Constants.TIERAVG[i];
            }
        }


    var rdmg = Ship.AvgDmg / (Ship.Battles * _ta.Dmg);
    var rfrag = Ship.Frags / (Ship.Battles * _ta.Frag);
    var rspot = Ship.Spotted / (Ship.Battles * _ta.Spot);
    var rdef = Ship.DefensePoints / (Ship.Battles * _ta.Def);

    var baseWN9 = 0.7*rdmg;
    if (Ship.Battles < 10)
    {
        baseWN9 += 0.14*rfrag + 0.13*Math.Sqrt(rspot) + 0.03*Math.Sqrt(rdef);
    } else{
        baseWN9 += 0.25*Math.Sqrt(rfrag*rspot) + 0.05*Math.Sqrt(rfrag*Math.Sqrt(rdef));
    }

        var WN9 = multiplier * Math.Max(0, 1 + (baseWN9 / expVals.WN9 - 1) / expVals.WN9Scale);
        return WN9;
    }

    public double CalcAccountWN9(Ship[] Ships, List<ExpVals> expected, double wn9multi)
    {  
        int totalbat = 0;
        double weight = 0;
        List<AccountShip> shiplist = new List<AccountShip>();

        for (int i = 0; i < Ships.Length; i++)
        {
            foreach (ExpVals exp in expected)
            {
                if (exp.Ship == Ships[i].Name)
                {
                    var WN9 = CalcShipWN9(Ships[i], exp, wn9multi);
                    AccountShip _ship = new AccountShip
                    {
                        WN9 = WN9,
                        battles = Ships[i].Battles,
                        exp = exp
                    };
                    shiplist.Add(_ship);

                    totalbat += _ship.battles;
                }
            }
            
        }
        if (totalbat == 0)
        {
            return totalbat;
        }

        for (int i = 0; i < shiplist.Count; i++)
        {
            var exp = shiplist[i].exp;
            var bcap = exp.Tier * (40.0 + exp.Tier * totalbat / 2000.0);
            shiplist[i].weight = Math.Min(bcap, shiplist[i].battles);
            weight += shiplist[i].weight;
        }

        weight *= 0.65;

        double accWN9 = 0;
        double usedweight = 0;
        int x = 0;
        for (; usedweight + shiplist[x].weight <= weight; x++)
        {
            accWN9 += shiplist[x].WN9 * shiplist[x].weight;
            usedweight += shiplist[x].weight;
        }

        accWN9 += shiplist[x].WN9 * (weight - usedweight);
        return accWN9 / weight;
    }
}
