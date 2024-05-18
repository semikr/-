using System;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        //var tanks = GetTanks();
        //var units = GetUnits();
        //var factories = GetFactories();

        string TankPath = "Tank.json";
        string UnitPath = "Unit.json";
        string FactoryPath = "Factory.json";

        string DejsonString1 = File.ReadAllText(TankPath);
        List<Tank> tanks = JsonSerializer.Deserialize<List<Tank>>(DejsonString1)!;

        string DejsonString2 = File.ReadAllText(UnitPath);
        List<Unit> units = JsonSerializer.Deserialize<List<Unit>>(DejsonString2)!;

        string DejsonString3 = File.ReadAllText(FactoryPath);
        List<Factory> factories = JsonSerializer.Deserialize<List<Factory>>(DejsonString3)!;



        for (int i = 0; i < tanks.Count; i++) 
        { 
            //var u = FindUnit(units, tanks, tanks[i].Name);
            var u2 = FindUnit2(units, tanks, tanks[i].Name);
            //var f = FindFactory(factories, u);
            var f2 = FindFactory2(factories, u2);
            //Console.WriteLine($"Tank: {tanks[i].Name}, Unit: {u.Name}, Factory {f.Name}");
            Console.WriteLine($"Tank: {tanks[i].Name}, Unit: {u2.Name}, Factory {f2.Name}");
        }

       
        Console.WriteLine($"Количество резервуаров: {tanks.Count}, установок: {units.Count}");

        var foundUnit = FindUnit2(units, tanks, "reservoir 2");
        var factory = FindFactory2(factories, foundUnit);

        Console.WriteLine($"Резервуар 2 принадлежит установке {foundUnit.Name} и заводу {factory.Name}");

        var totalVolume = GetTotalVolume2(tanks);
        Console.WriteLine($"Общий объем резервуаров: {totalVolume}");



        // Поиск резервуара по имени
        Console.Write("Enter the name of the tank to search: ");
        string searchName = Console.ReadLine();
        var un = FindUnit2(units, tanks, searchName);
        if(un != null) { 
        var fa = FindFactory2(factories, un);
        Console.WriteLine($"Tank: {searchName}, Unit: {un.Name}, Factory {fa.Name}");
        }
        else
        {
            return;
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString1 = JsonSerializer.Serialize(GetTanks(), options);
        string jsonString2 = JsonSerializer.Serialize(GetUnits(), options);
        string jsonString3 = JsonSerializer.Serialize(GetFactories(), options);

        Console.WriteLine(jsonString1);
        Console.WriteLine(jsonString2);
        Console.WriteLine(jsonString3);

    }

    //public void PrintAll(Tank[] tanks, Unit[] units, Factory[] factories)
    //{
    //    for(int i = 0; i < tanks.Length; i++)
    //    {
    //        for(int j = 0; i < units.Length; i++)
    //        {
    //            if (tanks[i].UnitId == units[j].Id)
    //            {
    //                for (int k = 0; i < factories.Length; i++)
    //                {
    //                    if (units[j].FactoryId == factories[k].Id)
    //                    {
    //                        Console.WriteLine($"Tank {tanks[i].Name}, unit {units[j].Name}, factory {factories[k].Name} ");
    //                    }
    //                }
    //            }
    //        }          
    //    }
        
    //}

    
    // реализуйте этот метод, чтобы он возвращал массив резервуаров, согласно приложенным таблицам
    // можно использовать создание объектов прямо в C# коде через new, или читать из файла (на своё усмотрение)
    public static List<Tank> GetTanks()
    {
        List<Tank> tanks = new List<Tank>()
            {
                new Tank (1, "reservoir 1", "aboveground - vertical", 1500, 2000, 1),
                new Tank(2, "reservoir 2", "aboveground - horizontal", 2500, 3000, 1),
                new Tank(3, "additional reservoir 24", "aboveground - horizontal", 3000, 3000, 2),
                new Tank(4, "reservoir 35", "aboveground - vertical", 3000, 3000, 2),   
                new Tank(5, "reservoir 47", "underground - dw", 4000, 5000, 2),
                new Tank(6, "reservoir 256", "underwater", 500, 500, 3)
        };
        return tanks;
 
    }
    // реализуйте этот метод, чтобы он возвращал массив установок, согласно приложенным таблицам
    public static List<Unit> GetUnits()
    {
        List<Unit> units = new List<Unit>()
            {
            new Unit(1, "GFU-2", "Gas fractionating unit", 1),
            new Unit(2, "AVT-6", "Atmospheric vacuum tube", 1),
            new Unit(3, "AVT-10", "Atmospheric vacuum tube", 2)
        };
        return units;
    }
    // реализуйте этот метод, чтобы он возвращал массив заводов, согласно приложенным таблицам
    public static List<Factory> GetFactories()
    {
        List<Factory> factories = new List<Factory>()
            {
                new Factory(1, "NPZ№1", "First NPZ"),
                new Factory(2, "NPZ№2", "Second NPZ")
        };
        return factories;
    }


    // реализуйте этот метод, чтобы он возвращал установку (Unit), которой
    // принадлежит резервуар (Tank), найденный в массиве резервуаров по имени
    // учтите, что по заданному имени может быть не найден резервуар
    //public static Unit FindUnit(List<Unit> units, List<Tank> tanks, string tankName)
    //{
    //    for (int i = 0; i < tanks.Count; i++)
    //    {
    //        if (tanks[i].Name == tankName)
    //        {
    //            return units[tanks[i].UnitId-1];
    //        }
    //        else if (tanks[i].Name != tankName & i == 5)
    //        {
    //            Console.WriteLine("По данному имени не был найден резервуар");
    //        }
    //    }
    //    return null;
        
    //}

    public static Unit FindUnit2(List<Unit> units, List<Tank> tanks, string tankName)
    {
        Tank selectedTank = (from t in tanks where t.Name == tankName select t).FirstOrDefault();

        if (selectedTank != null)
        {
            return (from u in units where u.Id == selectedTank.UnitId select u).FirstOrDefault();
        }
        else
        {
            Console.WriteLine("По данному имени не был найдер резервуар");
            return null;
        }

        
    }

    // реализуйте этот метод, чтобы он возвращал объект завода, соответствующий установке
    //public static Factory FindFactory(List<Factory> factories, Unit unit)
    //{
    //    for (int i = 0; i < factories.Count; i++)
    //    {
    //        if (factories[i].Id == unit.FactoryId)
    //        {
    //            return factories[i];
    //        }
    //        else if(factories[i].Id != unit.FactoryId & i == 1)
    //            { Console.WriteLine("По данному имени не была найдена фабрика"); }
    //    }
    //    return null; 
    //}

    public static Factory FindFactory2(List<Factory> factories, Unit unit)
    {
        Factory selectedFactory = (from f in factories where f.Id == unit.FactoryId select f).FirstOrDefault();

        if(selectedFactory != null)
        {
            return selectedFactory;
        }
        else
        {
            Console.WriteLine("По данному имени не была найдена фабрика");
            return null;
        }
    }

    // реализуйте этот метод, чтобы он возвращал суммарный объем резервуаров в массиве
    //public static int GetTotalVolume(List<Tank> tanks)
    //{
    //    int totalVolume = 0;

    //    for(int i = 0; i < units.Count; i++)
    //    {
    //        totalVolume = totalVolume + units[i].Volume;    
    //    }
    //    return totalVolume;
    //}

    public static int GetTotalVolume2(List<Tank> tanks)
    {
        int totalVolume = tanks.Sum(t => t.Volume);
        return totalVolume;
    }


}





// Установка
public class Unit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int FactoryId { get; set; }

    public Unit(int id, string name, string description, int factoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        FactoryId = factoryId;
    }
}


// Завод

public class Factory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Factory(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;

    }
}


// Резервуар

public class Tank
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Volume { get; set; }
    public int MaxVolume { get; set; }
    public int UnitId { get; set; }

    public Tank(int id, string name, string description, int volume, int maxVolume, int unitId)
    {
        Id = id;
        Name = name;
        Description = description;
        Volume = volume;
        MaxVolume = maxVolume;
        UnitId = unitId;
    }

    
}



