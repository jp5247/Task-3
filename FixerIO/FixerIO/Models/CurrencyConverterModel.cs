namespace FixerIO.Models;

public class CurrencyConverterModel
{
    public string CurrencyFrom { get; set; }

    public string CurrencyTo { get; set; }

    public double CurrencyAmount { get; set; }

    public DateTime dateTimeOfConversion { get; set; }
}
