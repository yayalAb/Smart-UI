
namespace Application.OperationModule.Queries.GetOperationList;

public class DashboardOperationListDto {
    public int OpeartionId {get; set;}
    public string OperationNumber {get; set;}
    public DateTime Created {get; set;}
    public Double Status {get; set;}
    public Double TotalPayedMoney {get; set;}
    public string? NameOnPermit {get; set;}
}