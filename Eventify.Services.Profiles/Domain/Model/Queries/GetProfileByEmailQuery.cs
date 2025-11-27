using Eventify.Services.Profiles.Domain.Model.ValueObjects;

namespace Eventify.Services.Profiles.Domain.Model.Queries;

public record GetProfileByEmailQuery(EmailAddress Email);