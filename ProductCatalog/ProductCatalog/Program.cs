using ProductCatalog;
using System.Runtime.InteropServices;


var MyProductList = new List<Product>();

Console.WriteLine("What do you wanna do? exit, add, list");
var Answer = Console.ReadLine();

while (Answer == "add")
{
    Console.WriteLine("Enter Product Name");
    var ProductName = Console.ReadLine();
    Console.WriteLine("How Much?");
    var ProductPrice = Console.ReadLine();

    var PruductPriceDouble = double.Parse(ProductPrice);
    var MyObject = new Product(ProductName, PruductPriceDouble);
    
    MyProductList.Add(MyObject);

    foreach (var CurrentProduct in MyProductList)
    {
        Console.WriteLine(CurrentProduct.Name + " " + CurrentProduct.Price + "£");
    }
    Console.WriteLine("What do you wanna do? exit, add");
    Answer = Console.ReadLine();

    if (Answer == "list")
    {
        Console.WriteLine(MyObject);
    }
}


Console.ReadKey();


