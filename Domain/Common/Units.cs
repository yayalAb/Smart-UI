
namespace Domain.Common.Units;

public class Unit {
    public string name {get; set;}
    public Double rate {get; set;}

    public bool Equals(Unit value){
        return (value.name == this.name && this.rate == value.rate);
    }

}

public class WeightUnits {
    public static string[] Units = {
        "KG", 
        "g",
        "Ton",
        "lb"
    };

    public static string Name = "WeightUnits";
    public static Unit Default = new Unit {name = "KG", rate = 1};
    public static Unit KG = new Unit {name = "KG", rate = 1};
    public static Unit Ton = new Unit {name = "Ton", rate = 0.001};
    public static Unit g = new Unit {name = "g", rate = 1000};
    public static Unit lb = new Unit {name = "lb", rate = 2.20462};

}

public class Currency {
    
    public static string[] Currencies = {
        "Birr",
        "USD",
        "DJF"
    };

    public static string Name = "Currency";
    public static Unit Default = new Unit {name = "USD", rate = 1};

    public static Unit Birr = new Unit {name = "Birr", rate = 50};
    public static Unit DJF = new Unit {name = "DJF", rate = 51};
    public static Unit USD = new Unit {name = "USD", rate = 1};

}