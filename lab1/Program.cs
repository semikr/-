using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        
        var tanks = GetTanks();
        var units = GetUnits();
        var factories = GetFactories();
        Console.WriteLine($"Количество резервуаров: {tanks.Length}, установок: {units.Length}");

        var foundUnit = FindUnit(units, tanks, "reservoir 2");
        var factory = FindFactory(factories, foundUnit);

        Console.WriteLine($"Резервуар 2 принадлежит установке {foundUnit.Name} и заводу {factory.Name}");

        var totalVolume = GetTotalVolume(tanks);
        Console.WriteLine($"Общий объем резервуаров: {totalVolume}");
    }

    // реализуйте этот метод, чтобы он возвращал массив резервуаров, согласно приложенным таблицам
    // можно использовать создание объектов прямо в C# коде через new, или читать из файла (на своё усмотрение)
    public static Tank[] GetTanks()
    {
        Tank[] tanks =
            {
                new Tank(1, "reservoir 1", "aboveground - vertical", 1500, 2000, 1),
                new Tank(2, "reservoir 2", "aboveground - horizontal", 2500, 3000, 1),
                new Tank(3, "additional reservoir 24", "aboveground - horizontal", 3000, 3000, 2),
                new Tank(4, "reservoir 35", "aboveground - vertical", 3000, 3000, 2),
                new Tank(5, "reservoir 47", "underground - dw", 4000, 5000, 2),
                new Tank(6, "reservoir 256", "underwater", 500, 500, 3)
        };
        return tanks;
 
    }
    // реализуйте этот метод, чтобы он возвращал массив установок, согласно приложенным таблицам
    public static Unit[] GetUnits()
    {
        Unit[] units =
            {
            new Unit(1, "GFU-2", "Gas fractionating unit", 1),
            new Unit(2, "AVT-6", "Atmospheric vacuum tube", 1),
            new Unit(3, "AVT-10", "Atmospheric vacuum tube", 2)
        };
        return units;
    }
    // реализуйте этот метод, чтобы он возвращал массив заводов, согласно приложенным таблицам
    public static Factory[] GetFactories()
    {
        Factory[] factories =
            {
                new Factory(1, "NPZ№1", "First NPZ"),
                new Factory(2, "NPZ№2", "Second NPZ")
        };
        return factories;
    }


    // реализуйте этот метод, чтобы он возвращал установку (Unit), которой
    // принадлежит резервуар (Tank), найденный в массиве резервуаров по имени
    // учтите, что по заданному имени может быть не найден резервуар
    public static Unit FindUnit(Unit[] units, Tank[] tanks, string tankName)
    {
        for (int i = 0; i < tanks.Length; i++)
        {
            if (tanks[i].Name == tankName)
            {
                return units[tanks[i].UnitId];
            }
            else if (tanks[i].Name != tankName & i == 5)
            {
                Console.WriteLine("По данному имени не был найден резервуар");
            }
        }
        return null;
        
    }
  
    // реализуйте этот метод, чтобы он возвращал объект завода, соответствующий установке
    public static Factory FindFactory(Factory[] factories, Unit unit)
    {
        for (int i = 0; i < factories.Length; i++)
        {
            if (factories[i].Id == unit.FactoryId)
            {
                return factories[i];
            }
            else { return null; }
        }
        return null; 
    }

    // реализуйте этот метод, чтобы он возвращал суммарный объем резервуаров в массиве
    public static int GetTotalVolume(Tank[] units)
    {
        int totalVolume = 0;

        for(int i = 0; i < units.Length; i++)
        {
            totalVolume = totalVolume + units[i].Volume;    
        }
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

