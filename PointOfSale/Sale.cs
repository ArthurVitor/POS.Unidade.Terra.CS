namespace PointOfSale;

public class Sale
{
    private Dictionary<string, Dictionary<string, string>> _products = new Dictionary<string, Dictionary<string, string>>();

    public void InsertProduct(Dictionary<string, Dictionary<string, string>> sale)
    {
        foreach (var product in sale)
        {
            if (_products.ContainsKey(product.Key))
            {
                _products[product.Key]["quantidade"] = (int.Parse(_products[product.Key]["quantidade"]) + int.Parse(sale[product.Key]["quantidade"])).ToString();
                return;
            }
            
            _products.Add(product.Key, sale[product.Key]);
        }
    }

    public string RemoveProduct(string productCode)
    {
        string productQuantity = _products[productCode]["quantidade"];
        _products.Remove(productCode);

        return productQuantity;
    }

    public Dictionary<string, Dictionary<string, string>> Products
    {
        get => _products;
    }

    public string GetTotal()
    {
        double result = 0;
        foreach (var product in _products)
        {
            result += double.Parse(product.Value["valor"].Replace(".", ",")) * int.Parse(product.Value["quantidade"]);
        }

        return $"R$ {result.ToString("F2")}";
    }

    public void CalcChange(double change)
    {
        int[] valores = { 200, 100, 50, 20, 10, 5, 2 };
        double[] moedas = { 1, 0.50, 0.25, 0.10, 0.05, 0.01 };

        for (int i = 0; i < valores.Length; i++)
        {
            int quantidade = (int)(change / valores[i]);
            if (quantidade > 0)
            {
                Console.WriteLine($"{quantidade} {(valores[i] > 1 ? "cédulas" : "cédula")} de R$ {valores[i]}");
                change -= quantidade * valores[i];
            }
        }

        foreach (double moeda in moedas)
        {
            int quantidade = (int)(change / moeda);
            if (quantidade > 0)
            {
                Console.WriteLine($"{quantidade} {(moeda > 1 ? "moedas" : "moeda")} de R$ {moeda.ToString("F2")}");
                change -= quantidade * moeda;
            }
        }
    }
    
    
}