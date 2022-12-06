using Domain.Enums;

namespace Domain.Common.DestinationTypes;

public class DestinationType {
    public static string[] Types = {
        Enum.GetName(typeof(Domain.Enums.DestinationType), Domain.Enums.DestinationType.IMPORT),
        Enum.GetName(typeof(Domain.Enums.DestinationType), Domain.Enums.DestinationType.EXPORT),
        Enum.GetName(typeof(Domain.Enums.DestinationType), Domain.Enums.DestinationType.TRANSFER)
    };
}