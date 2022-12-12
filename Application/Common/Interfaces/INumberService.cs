namespace Application.Common.Interfaces ; 
public interface INumberService {
   string NumberToCurrencyText(decimal number, MidpointRounding midpointRounding);
   string NumberToText(long number);

}