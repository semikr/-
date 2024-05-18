using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        string path = "JSON_sample_1.json";

        var deals = GetDeals(path);
        
        Console.WriteLine("Сделки: ");
        for (int i = 0; i < deals.Count(); i++)
        {
            Console.WriteLine($"Sum: {deals.ElementAt(i).Sum}\nId: {deals.ElementAt(i).Id}\nDate: {deals.ElementAt(i).Date}\n");
        }

        Console.WriteLine("Отфильтрованные сделки: ");

        var selectedDeals = GetNumbersOfDeals(deals);
        foreach (string i in selectedDeals)
        {
            Console.WriteLine(i);
        }

        var sumsByMonth = GetSumsByMonth(deals);

        foreach (var sumByMonth in sumsByMonth)
        {
            Console.WriteLine($"Month: {sumByMonth.Month.ToString("yyyy-MM")}, Sum: {sumByMonth.Sum}");
        }
    }

    public static IEnumerable<Deal> GetDeals(string path)
    {
        string jsonString = File.ReadAllText(path);
        IList<Deal> deals = JsonSerializer.Deserialize<IList<Deal>>(jsonString)!;   
        return deals;
    }

    public static IList<string> GetNumbersOfDeals(IEnumerable<Deal> deals)
    {
        return deals.Where(d => d.Sum >= 100).OrderBy(d => d.Date).Take(5).OrderByDescending(d => d.Sum).Select(d => d.Id).ToList();
    }

    public record SumByMonth(DateTime Month, int Sum);

    public static IList<SumByMonth> GetSumsByMonth(IEnumerable<Deal> deals)
    {
        return deals.GroupBy(d => new { Month = new DateTime(d.Date.Year, d.Date.Month, 1) }).Select(dls => new SumByMonth(dls.Key.Month, dls.Sum(d => d.Sum))).ToList();
    }


    public class Deal
    {
        public int Sum { get; set; }
        public string Id { get; set; }
        public DateTime Date { get; set; }
    }

   
}