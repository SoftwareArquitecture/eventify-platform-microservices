namespace Eventify.Services.Planning.Domain.Model.ValueObjects;

public record CustomerName(string NameCustomer)
{
    public CustomerName() : this(string.Empty)
    {
    }
}