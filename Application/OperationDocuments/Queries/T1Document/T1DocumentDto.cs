
using Application.OperationDocuments.Queries.T1Document.T1Dtos;
using Application.SettingModule.Queries.DefaultCompany;

namespace Application.OperationDocuments.Queries.T1Document;

public class T1DocumentDto
{
    public ICollection<T1TruckAssignmentDto> TruckAssignments { get; set; }
    public T1OperationDto Operation { get; set; }
    public SettingDto DefaultCompanyInformation { get; set; }
}