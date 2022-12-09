
using Domain.Enums;

namespace Domain.Common.DocumentType;

public class DocumentType {
    public static string[] Types = {
        Enum.GetName(typeof(Documents), Documents.Number4),
        Enum.GetName(typeof(Documents), Documents.T1),
        Enum.GetName(typeof(Documents), Documents.ImportNumber9),
        Enum.GetName(typeof(Documents), Documents.TransferNumber9),
        Enum.GetName(typeof(Documents), Documents.Number1)
};

public class DocumentationType {
    public static string[] Types = {
        Enum.GetName(typeof(Documents), Documents.GoodsRemoval),
        Enum.GetName(typeof(Documents), Documents.GatePass),
        Enum.GetName(typeof(Documents), Documents.Waybill),
        Enum.GetName(typeof(Documents), Documents.CommercialInvoice),
        Enum.GetName(typeof(Documents), Documents.PackageList),
        Enum.GetName(typeof(Documents), Documents.TruckWayBill),
        Enum.GetName(typeof(Documents), Documents.ProformaInvoice),
        Enum.GetName(typeof(Documents), Documents.CirtificateOfOrigin)
    };

    };
}