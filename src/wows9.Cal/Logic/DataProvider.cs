using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;

namespace wows9.Cal;

internal class JsonExp
{
    public string Ship;
    public double average_damage_dealt;
    public double average_frags;
    public double win_rate;
}

internal class DataProvider
{
    private string ExpectedValuesJson;
    public void GetExpectedValues()
    {
        using (WebClient wc = new WebClient())
        {
            ExpectedValuesJson = wc.DownloadString("https://api.wows-numbers.com/personal/rating/expected/json/");
        }

        //ExpectedValuesJson = File.ReadAllText("stats.json");

        JObject joResponse = JObject.Parse(ExpectedValuesJson);
        JObject ojObject = (JObject)joResponse["data"];
        JObject ship = (JObject)joResponse[""];

        Console.WriteLine(ship);
    }

    public Player PlayerData(string Name)
    {
        Player _p = new Player();
        _p.Name = Name;
        _p.ID = PlayerAccountID(_p);
        _p.Warships = PlayerWarships(_p);
        _p.WinrateOverall = PlayerAccountWinrate(_p);

        return _p;
    }

    private int PlayerAccountID(Player p)
    {
        using (WebClient wc = new WebClient())
        {
            ExpectedValuesJson = wc.DownloadString($"https://api.worldofwarships.eu/wows/account/list/?application_id=e25fb4a106e03159cae9b7b4286e4e55&search={p.Name}");
        }

        JObject joResponse = JObject.Parse(ExpectedValuesJson);
        JArray array = (JArray)joResponse["data"];
        int id = Convert.ToInt32(((JValue)array[0].SelectToken("account_id")).Value);
        return id;
    }

    private double PlayerAccountWinrate(Player p)
    {
        using (WebClient wc = new WebClient())
        {
            ExpectedValuesJson = wc.DownloadString($"https://api.worldofwarships.eu/wows/account/info/?application_id=e25fb4a106e03159cae9b7b4286e4e55&account_id={p.ID}");
        }

        JObject joResponse = JObject.Parse(ExpectedValuesJson);
        JObject ojObject = (JObject)joResponse["data"];
        JObject acc = (JObject)ojObject[p.ID.ToString()];
        JObject stats = (JObject)acc["statistics"];
        JObject pvp = (JObject)stats["pvp"];
        double Wins = Convert.ToDouble(((JValue)pvp.SelectToken("wins")).Value);
        double Losses = Convert.ToDouble(((JValue)pvp.SelectToken("losses")).Value);

        double total = Wins + Losses;

        return Math.Round(Wins / (total / 100),2);
    }

    private Ship[] PlayerWarships(Player p)
    {

        using (WebClient wc = new WebClient())
        {
            ExpectedValuesJson = wc.DownloadString($"https://api.worldofwarships.eu/wows/ships/stats/?application_id=e25fb4a106e03159cae9b7b4286e4e55&account_id={p.ID}");
        }

        JObject joResponse = JObject.Parse(ExpectedValuesJson);
        JObject ojObject = (JObject)joResponse["data"];
        JArray array = (JArray)ojObject[p.ID.ToString()];

        List<Ship> Warships = new List<Ship>();
        for (int i = 0; i < array.Count; i++)
        {
                JObject pvp = (JObject)array[i]["pvp"];
                Ship ship = new Ship();

                double battles = Convert.ToDouble(((JValue)pvp.SelectToken("battles")).Value);
                ship.ID = ((JValue)array[i].SelectToken("ship_id")).Value.ToString();
                ship.Battles = Convert.ToInt32(((JValue)pvp.SelectToken("battles")).Value);
                ship.Frags = Math.Round(Convert.ToDouble(((JValue)pvp.SelectToken("frags")).Value) / battles, 2);
                ship.XP = Math.Round(Convert.ToDouble(((JValue)pvp.SelectToken("xp")).Value) / battles, 2);
                ship.AvgDmg = Math.Round(Convert.ToDouble(((JValue)pvp.SelectToken("damage_dealt")).Value) / battles, 3);
                ship.Spotted = Math.Round(Convert.ToDouble(((JValue)pvp.SelectToken("ships_spotted")).Value) / battles, 2);
                ship.SpotDmg = Math.Round(Convert.ToDouble(((JValue)pvp.SelectToken("damage_scouting")).Value) / battles, 3);
                ship.DefensePoints = Math.Round(Convert.ToDouble(((JValue)pvp.SelectToken("dropped_capture_points")).Value) / battles, 2);

                ShipMetadata _sm = new ShipMetadata();
                _sm = ShipData(ship.ID);

                ship.Tier = _sm.Tier;
                ship.Name = _sm.Name;
                ship.Type = _sm.Type;
                ship.Img = _sm.Img;

                Warships.Add(ship);
           
        }

        return Warships.ToArray();
    }

    private ShipMetadata ShipData(string ShipID)
    {
        ShipMetadata _sm = new ShipMetadata();

        using (WebClient wc = new WebClient())
        {
            ExpectedValuesJson = wc.DownloadString($"https://api.worldofwarships.eu/wows/encyclopedia/ships/?application_id=e25fb4a106e03159cae9b7b4286e4e55&ship_id={ShipID}");
        }

        JObject joResponse = JObject.Parse(ExpectedValuesJson);
        JObject data = (JObject)joResponse["data"];
        JObject ship = (JObject)data[ShipID];
        JObject img = (JObject)ship["images"];
        // JArray array = (JArray)joResponse[ShipID];
        _sm.Tier = Convert.ToInt16(((JValue)ship.SelectToken("tier")).Value);
        _sm.Name = ((JValue)ship.SelectToken("name")).Value.ToString();
        _sm.Type = ((JValue)ship.SelectToken("type")).Value.ToString();
        _sm.Img = ((JValue)img.SelectToken("small")).Value.ToString();

        return _sm;
    }
}

