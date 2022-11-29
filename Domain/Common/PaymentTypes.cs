
namespace Domain.Common.PaymentTypes;

public class ShippingAgentPaymentType {
    public static string[] Types = {
        "Delivery Order", 
        "Advanced Demurrage",
        "Container Deposit",
        "Final Demurrage",
        "Amendment"
    };
    public static string DeliveryOrder = "Delivery Order";
    public static string AdvancedDemurrage = "Advanced Demurrage";
    public static string ContainerDeposit = "Container Deposit";
    public static string FinalDemurrage = "Final Demurrage";
    public static string Amendment = "Amendment";

}

public class TerminalPortPaymentType {
    
    public static string[] Types = {
        "Facture",
        "Storage",
        "Complimentary"
    };

    public static string Facture = "Facture";
    public static string Storage = "Storage";
    public static string Complimentary = "Complimentary";
    
}