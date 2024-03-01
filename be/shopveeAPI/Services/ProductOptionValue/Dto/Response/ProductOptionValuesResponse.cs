namespace shopveeAPI.Services.ProductOptionValue.Dto.Response;

public class ProductOptionValuesResponse
{
    public Guid OptionId { get; set; }
    public string Value { get; set; } = null!;
    public Guid ProductId { get; set; }
    public decimal BasePrice { get; set; }
    public decimal SalePrice { get; set; }
    public decimal SalePercentage { get; set; }
    public int? SoldQuantity { get; set; }
    public int? AvailableQuantity { get; set; }
    public string OptionName { get; set; } = null!;
}