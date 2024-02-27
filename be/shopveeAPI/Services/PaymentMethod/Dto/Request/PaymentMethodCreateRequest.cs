using shopveeAPI.Enum;

namespace shopveeAPI.Services.PaymentMethod.Dto.Request;

public class PaymentMethodCreateRequest
{
    public Guid UserId { get; set; }
    
    public Guid PaymentTypeId { get; set; }
    
    public string? Provider { get; set; }
    
    public string? AccountNumber { get; set; }
    
    public DateTime ExpiryDate { get; set; }
    
    public bool IsDefault { get; set; }
    
    public PaymentType PaymentType { get; set; }
}