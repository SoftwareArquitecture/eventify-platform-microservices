using Eventify.Services.IAM.Domain.Model.Commands;
using Eventify.Services.IAM.Interfaces.REST.Resources;

namespace Eventify.Services.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    }
}