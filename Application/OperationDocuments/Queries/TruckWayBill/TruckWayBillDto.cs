using Domain.Entities;

namespace Application.OperationDocuments.Queries.TruckWayBill;

public class TruckWayBillDto
{
    public Operation operation { get; set; }
    public Documentation? documentation { get; set; }
    public ICollection<Good>? goods { get; set; }
    public ICollection<Container>? containers { get; set; }
}