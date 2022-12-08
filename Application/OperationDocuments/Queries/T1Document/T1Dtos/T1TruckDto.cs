using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationDocuments.Queries.T1Document.T1Dtos;

public class T1TruckDto : IMapFrom<Truck> {
    public string TruckNumber {get; set;}
}