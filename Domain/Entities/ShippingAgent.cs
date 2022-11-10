using Domain.Common;
namespace Domain.Entities;

public class ShippingAgent : BaseAuditableEntity
{
        public string FullName { get; set; } = null!;
        public string? CompanyName { get; set; }
        public byte[]? Image { get; set; }
        public int AddressId { get; set; }
   
    public Address Address { get; set; }
    
    //has many
    public ICollection<ShippingAgentFee> AgentFees { get; set; }
    public ICollection<BillOfLoading> BillOfLoadings { get; set; }
}