using System.Buffers;
using Domain.Common.Units;
using Domain.Enums;

namespace Domain.Common.DocumentType;

public class DocumentType
{
    public static string[] Types = {
        Enum.GetName(typeof(Documents), Documents.Number4),
        Enum.GetName(typeof(Documents), Documents.T1),
        Enum.GetName(typeof(Documents), Documents.ImportNumber9),
        Enum.GetName(typeof(Documents), Documents.TransferNumber9),
        Enum.GetName(typeof(Documents), Documents.Number1)
    };

    public static Dictionary<string, int> statusDictionary = new Dictionary<string, int>(){
        { Enum.GetName(typeof(Status), Status.Opened), 1 },
        { Enum.GetName(typeof(Status), Status.ShippingAgentFeePaid), 1 },
        { Enum.GetName(typeof(Status), Status.ImportNumber9Generated), 1 },
        { Enum.GetName(typeof(Status), Status.ImportNumber9Approved), 1 },
        { Enum.GetName(typeof(Status), Status.EntranceGatePassGenerated), 2 },
        { Enum.GetName(typeof(Status), Status.ExitGatePassGenerated), 3 },
        { Enum.GetName(typeof(Status), Status.Number1Generated), 3 },
        { Enum.GetName(typeof(Status), Status.Number4Generated), 3 },
        { Enum.GetName(typeof(Status), Status.Number4Approved), 3 },
        { Enum.GetName(typeof(Status), Status.TransferNumber9Generated), 3 },
        { Enum.GetName(typeof(Status), Status.TransferNumber9GeneratedApproved), 3 },
        { Enum.GetName(typeof(Status), Status.T1Generated), 4 },
        { Enum.GetName(typeof(Status), Status.GoodsRemovalGenerated), 4 },
        { Enum.GetName(typeof(Status), Status.ECDDispatched), 4 },
        { Enum.GetName(typeof(Status), Status.WaybillIssued), 4 },
        { Enum.GetName(typeof(Status), Status.Closed), 5 }
    };

}

public class DocumentationType
{
    public static string[] Types = {
        Enum.GetName(typeof(Documents), Documents.GoodsRemoval),
        Enum.GetName(typeof(Documents), Documents.EntranceGatePass),
        Enum.GetName(typeof(Documents), Documents.ExitGatePass),
        Enum.GetName(typeof(Documents), Documents.Waybill),
        Enum.GetName(typeof(Documents), Documents.CommercialInvoice),
        Enum.GetName(typeof(Documents), Documents.PackageList),
        Enum.GetName(typeof(Documents), Documents.TruckWayBill),
        Enum.GetName(typeof(Documents), Documents.ProformaInvoice),
        Enum.GetName(typeof(Documents), Documents.CirtificateOfOrigin)
    };

};