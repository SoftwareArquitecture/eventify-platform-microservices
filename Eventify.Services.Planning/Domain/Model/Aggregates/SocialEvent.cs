using Eventify.Services.Planning.Domain.Model.Commands;
using Eventify.Services.Planning.Domain.Model.ValueObjects;

namespace Eventify.Services.Planning.Domain.Model.Aggregates;

public class SocialEvent
{
    protected SocialEvent()
    {
    }


    public SocialEvent(CreateSocialEventCommand command)
        : this(command.EventTitle, command.EventDate, command.CustomerName, command.Location)
    {
        Id = new SocialEventId();
    }

    public SocialEvent(string eventTitle, DateTime eventDate, string customerName, string location)
        : this(new SocialEventTitle(eventTitle), new SocialEventDate(eventDate), new CustomerName(customerName),
            new SocialEventPlace(location), EStatusType.Active)
    {
    }

    public SocialEvent(SocialEventTitle title, SocialEventDate eventDate, CustomerName customer, SocialEventPlace place,
        EStatusType status)
    {
        Id = new SocialEventId();
        Title = title;
        EventDate = eventDate;
        NameCustomer = customer;
        Place = place;
        Status = status;
    }

    public SocialEventId Id { get; private set; }
    public SocialEventTitle Title { get; private set; }
    public SocialEventDate EventDate { get; private set; }
    public CustomerName NameCustomer { get; private set; }
    public SocialEventPlace Place { get; private set; }
    public EStatusType Status { get; private set; }

    // Computed properties para compatibilidad
    public string EventTitle => Title.Title;
    public DateTime Date => EventDate.Date;
    public string CustomerName => NameCustomer.NameCustomer;
    public string Location => Place.Place;

    public SocialEvent UpdateInformation(SocialEventTitle title, SocialEventDate eventDate, CustomerName customer,
        SocialEventPlace place, EStatusType status)
    {
        Title = title;
        EventDate = eventDate;
        NameCustomer = customer;
        Place = place;
        Status = status;
        return this;
    }
}