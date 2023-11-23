namespace webapi.builder{
    public class ProductModelBuilder
    {
        private readonly ProductModel product;

        public ProductModelBuilder()
        {
            product = new ProductModel();
        }

        public ProductModelBuilder SetName(string name)
        {
            product.SetName(name);
            return this;
        }

        public ProductModelBuilder SetQuantity(int quantity)
        {
            product.SetQuantity(quantity);
            return this;
        }

        public ProductModelBuilder SetDescription(string description)
        {
            product.SetDescription(description);
            return this;
        }

        public ProductModelBuilder SetPrice(int price)
        {
            product.SetPrice(price);
            return this;
        }

        public ProductModelBuilder SetCurrency(string currency)
        {
            product.SetCurrency(currency);
            return this;
        }

        public ProductModelBuilder SetImageURL(string imageURL) {
            product.SetImageURL(imageURL);
            return this;
        }

        public ProductModel Build()
        {
            return product;
        }
    }
}