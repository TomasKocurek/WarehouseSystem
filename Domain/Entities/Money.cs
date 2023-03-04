using Domain.Enum;

namespace Domain.Entities;
public class Money
{
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }

    public Money(decimal amount, Currency currency = Currency.CzechCrowns)
    {
        Amount = amount;
        Currency = currency;
    }
}
