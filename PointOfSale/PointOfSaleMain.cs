namespace PointOfSale;

public class PointOfSaleMain
{
    private Dictionary<string, Dictionary<string, string>> _inventory = new Dictionary<string, Dictionary<string, string>>();

    private Sale _sale = new Sale();
    
    public PointOfSaleMain(string filePath)
    {
        InitializeInventory(filePath);
    }

    private void InitializeInventory(string filePath)
    {
        Dictionary<string, Dictionary<string, string>> dataProcessing = new DataProcessing(filePath).ProcessData();
        _inventory = dataProcessing;
    }

    public void RunMenu()
    {
        Console.WriteLine("Bem vindo ao Ponto de Venda da DBserver \n" +
                          "Escolha uma das opções \n" +
                          "1 - Listar produtos do estoque \n" +
                          "2 - Listar pedidos \n" +
                          "3 - Adicionar produto \n" +
                          "4 - Remover produto \n" +
                          "5 - Pagar");
        Console.Write("Opção escolhida: ");
        
        string option = Console.ReadLine()!;

        switch (option)
        {
            case "1":
                ListProducts();
                break;
            case "2":
                ListSale();
                break;
            case "3":
                AddProducts();
                break;
            case "4":
                RemoveProduct();
                break;
            case "5":
                Pay();
                break;
            default:
                return;
        }
        
    }

    private void ListProducts()
    {
        Console.WriteLine("Listando produtos \n");
        foreach (var item in _inventory)
        {
            Console.WriteLine($"Código: {item.Key}, Nome: {_inventory[item.Key]["nome"]}, Preço: {_inventory[item.Key]["valor"]}, Quantidade: {_inventory[item.Key]["quantidade"]}");
        }

        string option = "";
        while (option != "-1")
        {
            Console.Write("Para voltar para o menu, digite -1: ");
            option = Console.ReadLine()!;
        }
        
        RunMenu();
    }

    private void ListSale()
    {
        Dictionary<string, Dictionary<string, string>> saleProducts = _sale.Products;
        Console.WriteLine("Listando pedido");
        foreach (var product in saleProducts)
        {
            Console.WriteLine($"Nome: {saleProducts[product.Key]["nome"]}, Quantidade: {saleProducts[product.Key]["quantidade"]}, Valor: {saleProducts[product.Key]["valor"]}, Código: {product.Key}");
        }

        Console.WriteLine($"Total: {_sale.GetTotal()}");
        
        string option = "";
        while (option != "-1")
        {
            Console.Write("Para voltar para o menu, digite -1: ");
            option = Console.ReadLine()!;
        }
        
        RunMenu();
    }

    private void AddProducts()
    {
        string productCode = "0";
        string productQuantity = "0";
        
        // If product code is lower than 1, ask for another code. If product code is higher than the length of the inventory, ask for another code.
        while (int.Parse(productCode) < 1 || int.Parse(productCode) > _inventory.Count)
        {
            Console.Write("Insira um código de produto válido: ");
            productCode = Console.ReadLine()!;
        }

        while (int.Parse(productQuantity) < 1 || int.Parse(productQuantity) > int.Parse(_inventory[productCode]["quantidade"]))
        {
            Console.Write($"Insira a quantidade desejada, máximo ({_inventory[productCode]["quantidade"]}): ");
            productQuantity = Console.ReadLine()!;
        }

        Dictionary<string, Dictionary<string, string>> saleData = new Dictionary<string, Dictionary<string, string>>();
        Dictionary<string, string> saleInfo = new Dictionary<string, string>();
        
        saleInfo.Add("nome", _inventory[productCode]["nome"]);
        saleInfo.Add("valor", _inventory[productCode]["valor"]);
        saleInfo.Add("quantidade", productQuantity);
        _inventory[productCode]["quantidade"] = (int.Parse(_inventory[productCode]["quantidade"]) - int.Parse(productQuantity)).ToString();
        
        saleData.Add(productCode, saleInfo);
        
        _sale.InsertProduct(saleData);
        
        RunMenu();
    }

    private void RemoveProduct()
    {
        Dictionary<string, Dictionary<string, string>> products = _sale.Products;

        string productCode = "0";
        while (!products.ContainsKey(productCode))
        {
            Console.Write("Insira um código válido: ");
            productCode = Console.ReadLine()!;
        }

        string productRestock = _sale.RemoveProduct(productCode);
        _inventory[productCode]["quantidade"] = (int.Parse(_inventory[productCode]["quantidade"]) + int.Parse(productRestock)).ToString();

        RunMenu();
    }

    private void Pay()
    {
        string paymentMethod = "";
        while (paymentMethod != "cartao" || paymentMethod != "dinheiro")
        {
            Console.Write("Qual o metodo de pagamento? (cartao ou dinheiro): ");
            paymentMethod = Console.ReadLine()!;
        }
        
        if (paymentMethod == "cartao")
        {
            Console.WriteLine("Compra paga");
            return;
        }
        
        Console.WriteLine($"O total foi de R$ {_sale.GetTotal()}. Valor pago: ");
        double valuePayed = Console.Read();
        double valueTotal = _sale.GetTotal()[2];
        
        _sale.CalcChange(valuePayed - valueTotal);
    }
}