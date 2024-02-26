namespace shopveeAPI.Services.ProductOptionValue.Dto.Request;

public class ProductOptionValueRequest
{
    public Guid? Id { get; set; }
    public Guid OptionId { get; set; }
    public string Value { get; set; } = null!;
    public Guid ProductId { get; set; }
    public decimal BasePrice { get; set; }
    public decimal SalePrice { get; set; }
    public decimal SalePercentage { get; set; }
    public int? SoldQuantity { get; set; }
    public int? AvailableQuantity { get; set; }
}