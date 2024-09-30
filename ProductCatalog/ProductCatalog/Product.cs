namespace ProductCatalog
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price  { get; set; }

        public Product(string inputName, double inputPrice)
        {
            this.Name = inputName;
            this.Price = inputPrice;
        }

    }
}
