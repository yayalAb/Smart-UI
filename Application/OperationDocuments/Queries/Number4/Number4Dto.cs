
using Application.CompanyModule.Queries;
using Application.ContainerModule;
using Application.PortModule;
using Domain.Entities;

namespace Application.OperationDocuments.Queries.Number4;

public class Number4Dto
{
    public Company company { get; set; }
    public ContactPersonDto nameOnPermit { get; set; }
    public Operation operation { get; set; }
    public ICollection<ContainerDto> containers { get; set; }
    public Payment? doPayment { get; set; }
    public PortDto destinationPort { get; set; }
    public ICollection<DocGoodDto> goods { get; set; }
}