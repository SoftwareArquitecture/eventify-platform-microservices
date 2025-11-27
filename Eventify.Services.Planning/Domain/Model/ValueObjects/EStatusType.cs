namespace Eventify.Services.Planning.Domain.Model.ValueObjects;

/// <summary>
///     Represents the status of a social event.
/// </summary>
public enum EStatusType
{
    Active,
    ToConfirm,
    Cancelled,
    Completed
}