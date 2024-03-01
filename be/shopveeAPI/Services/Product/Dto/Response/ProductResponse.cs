namespace shopveeAPI.Services.Product.Dto.Response;

public class ProductResponse
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid CategoryId { get; set; }
    public string Image { get; set; } = null!;
    public Guid ShopId { get; set; }
    public int SoldQuantity { get; set; } = 0;
}