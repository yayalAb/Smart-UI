using Domain.Common;
namespace Domain.Entities;

public class ShippingAgent : BaseAuditableEntity
{
    public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? CompanyName { get; set; }
        public int? ImageId { get; set; }
        public int AddressId { get; set; }
    
    public Image Image { get; set; }
    public Address Address { get; set; }
    
    //has many
    public ICollection<ShippingAgentFee> AgentFees { get; set; }
}