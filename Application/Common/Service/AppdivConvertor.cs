using Domain.Common.Units;

namespace Application.Common.Service;
public class AppdivConvertor
{
    /**
    this method will convert any weight units to the default weight unit if convertTo unit has not been set
    */
    public static float WeightConversion(string unitName, float value, string? convertTo = null)
    {

        Unit convertFrom = WeightUnits.getUnit(unitName);
        var toBeConverted = convertTo == null ? null : WeightUnits.getUnit(convertTo);
        var to_default = value / convertFrom.rate;

        if (toBeConverted == null)
        {
            return to_default;
        }
        else
        {
            if(toBeConverted.Equals(WeightUnits.Default)) {
                return to_default;
            } else {
                return to_default * toBeConverted.rate;
            }
        }
    }

}