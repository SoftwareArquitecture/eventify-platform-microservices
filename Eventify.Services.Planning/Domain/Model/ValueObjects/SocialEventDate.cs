namespace Eventify.Services.Planning.Domain.Model.ValueObjects;

public record SocialEventDate(DateTime Date)
{
    //public SocialEventDate() : this(DateTime.UtcNow) {}
    public SocialEventDate() : this(DateTime.Now.AddDays(1))
    {
    }

    public override string ToString()
    {
        return Date.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public static implicit operator DateTime(SocialEventDate socialEventDate)
    {
        return socialEventDate.Date;
    }

    public static implicit operator SocialEventDate(DateTime date)
    {
        return new SocialEventDate(date);
    }
}