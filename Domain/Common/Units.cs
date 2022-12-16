using System.Linq;

namespace Domain.Common.Units;

public class Unit
{
    public string name { get; set; }
    public float rate { get; set; }

    public bool Equals(Unit value)
    {
        return (value.name == this.name && this.rate == value.rate);
    }

    public bool NameEquals(Unit value) {
        return (value.name == this.name);
    }

}

public class WeightUnits
{
    public static string[] Units = {
        "KG",
        "g",
        "Ton",
        "lb"
    };

    public static Dictionary<string, Unit> dict = new Dictionary<string, Unit>(){
        { "KG", new Unit { name = "KG", rate = 1 } },
        {"Ton", new Unit { name = "Ton", rate = 0.001F }},
        {"g", new Unit { name = "g", rate = 1000 }},
        {"lb", new Unit { name = "lb", rate = 2.20462F }}
    };

    public static string Name = "WeightUnits";
    public static Unit Default = new Unit { name = "KG", rate = 1 };

    public static Unit getUnit(string name){

        foreach(KeyValuePair<string, Unit> instance in dict){
            if(instance.Key == name){
                return instance.Value;
            }
        }

        throw new Exception("unit not found");

    }

}

public class Currency
{

    public static string[] Currencies = {
        "BIR",
        "USD",
        "DJF"
    };

    public static string Name = "Currency";
    public static Unit Default = new Unit { name = "USD", rate = 1 };
    public static Dictionary<string, Unit> dict = new Dictionary<string, Unit>() {
        {"Birr", new Unit { name = "Birr", rate = 50 }},
        {"DJF", new Unit { name = "DJF", rate = 51 }},
        {"USD", new Unit { name = "USD", rate = 1 }}
    };


    public static Unit getUnit(string name){

        foreach(KeyValuePair<string, Unit> instance in dict){
            if(instance.Key == name){
                return instance.Value;
            }
        }

        throw new Exception("unit not found");

    }

}