using System.Text.Json;

[Serializable]
public class ProductModel
{
    public string Name { get; private set; }
    public int Quantity { get; private set; }
    public string Description { get; private set; }
    public int Price { get; private set; }
    public string Currency { get; private set; }
    public string ImageURL { get; private set; }

    public string GetName() => Name;
    public int GetQuantity() => Quantity;
    public string GetDescription() => Description;
    public int GetPrice() => Price;
    public string GetCurrency() => Currency;

    public string GetImageURL() => ImageURL;

    public void SetName(string name) => Name = name;
    public void SetQuantity(int quantity) => Quantity = quantity;
    public void SetDescription(string description) => Description = description;
    public void SetPrice(int price) => Price = price;
    public void SetCurrency(string currency) => Currency = currency;
    public void SetImageURL(string imageURL) => ImageURL = imageURL;

    public override string ToString()
    {
        return $"ImageURL: {GetImageURL()}, Name: {GetName()}, Quantity: {GetQuantity()}, Description: {GetDescription()}, Price: {GetPrice()} {GetCurrency()}";
    }

    public string ToJson()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true, // Optional: Format the JSON for readability
        };

        return JsonSerializer.Serialize(this, options);
    }
}