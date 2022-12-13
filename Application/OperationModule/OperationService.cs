using Application.Common.Exceptions;
using Application.Common.Models;
using Domain.Enums;

namespace Application.OperationModule;
public class OperationService
{
    public string GenerateOperationNumber(int operationId, string destinationType)
    {
        var DestinatinTypes = Enum.GetNames(typeof(DestinationType)).ToList();
        string prefix = "";
        DestinatinTypes.ForEach(dt =>
        {
            if (dt.ToUpper() == destinationType.ToUpper())
            {
                prefix = dt.Substring(0, 2);
            }
        });
        if (prefix == "")
        {
            throw new GhionException(CustomResponse.BadRequest("invalid destination type"));
        }
        string YY = DateTime.Now.Year.ToString().Substring(2);
        string id = operationId.ToString("D" + 4);
        return prefix + YY + id;
    }
}