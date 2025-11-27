using Eventify.Services.Planning.Domain.Model.Commands;
using Eventify.Services.Planning.Domain.Model.ValueObjects;

namespace Eventify.Services.Planning.Domain.Model.Aggregates;

public class Quote
{
    public Quote(string title, ESocialEventType eventType, int guestQuantity, string location, double totalPrice,
        EQuoteStatus status,
        DateTime eventDate, OrganizerId organizerId, HostId hostId)
    {
        Title = title;
        EventType = eventType;
        GuestQuantity = guestQuantity;
        Location = location;
        TotalPrice = totalPrice;
        Status = status;
        EventDate = eventDate;
        OrganizerId = organizerId;
        HostId = hostId;
    }

    public Quote(CreateQuoteCommand command)
    {
        Id = new QuoteId();
        Title = command.Title;
        EventType = command.EventType;
        GuestQuantity = command.GuestQuantity;
        Location = command.Location;
        TotalPrice = command.TotalPrice;
        Status = command.Status;
        EventDate = command.EventDate;
        OrganizerId = command.OrganizerId;
        HostId = command.HostId;
    }

    public QuoteId Id { get; }
    public string Title { get; private set; }

    public ESocialEventType EventType { get; private set; }
    public int GuestQuantity { get; private set; }
    public string Location { get; private set; }
    public double TotalPrice { get; private set; }
    public EQuoteStatus Status { get; private set; }
    public DateTime EventDate { get; private set; }

    public OrganizerId OrganizerId { get; private set; }

    public HostId HostId { get; private set; }

    public Quote UpdateInformation(string title, ESocialEventType eventType, int guestQuantity, string location,
        double totalPrice, DateTime eventDate)
    {
        Title = title;
        EventType = eventType;
        GuestQuantity = guestQuantity;
        Location = location;
        TotalPrice = totalPrice;
        EventDate = eventDate;
        return this;
    }

    public void Accept()
    {
        Status = EQuoteStatus.Accepted;
    }

    public void Reject()
    {
        Status = EQuoteStatus.Rejected;
    }
}