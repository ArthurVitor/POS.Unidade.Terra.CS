namespace PointOfSale;

public class DataProcessing
{
    private readonly string _filePath;
    
    public DataProcessing(string filePath)
    {
        _filePath = filePath;
    }

    public Dictionary<string, Dictionary<string, string>> ProcessData()
    {
        Dictionary<string, Dictionary<string, string>> data = new Dictionary<string, Dictionary<string, string>>();
        string[] lines = File.ReadAllLines(_filePath);
        
        foreach (var line in lines)
        {
            string[] splitedLine = line.Split(",");
            data.Add(splitedLine[0],
                new Dictionary<string, string>
                {
                    {"nome", splitedLine[1]},
                    {"valor", splitedLine[2]},
                    {"quantidade", splitedLine[3]}
                }
            );
        }

        return data;
    }
}