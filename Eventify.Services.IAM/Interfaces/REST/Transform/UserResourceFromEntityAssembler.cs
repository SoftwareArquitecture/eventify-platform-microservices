using Eventify.Services.IAM.Domain.Model.Aggregates;
using Eventify.Services.IAM.Interfaces.REST.Resources;

namespace Eventify.Services.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(user.Id, user.Username);
    }
}