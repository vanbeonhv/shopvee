using shopveeAPI.Enum;

namespace shopveeAPI.Services.PaymentMethod.Dto.Response;

public class PaymentMethodReponse
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid PaymentTypeId { get; set; }
    
    public string? Provider { get; set; }
    
    public string? AccountNumber { get; set; }
    
    public DateTime ExpiryDate { get; set; }
    
    public bool IsDefault { get; set; }
    
    public PaymentType PaymentType { get; set; }
    
}