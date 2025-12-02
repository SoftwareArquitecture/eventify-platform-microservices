using Eventify.Services.IAM.Domain.Model.Commands;
using Eventify.Services.IAM.Interfaces.REST.Resources;

namespace Eventify.Services.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(
            resource.Username,
            resource.Password,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.PhoneNumber,
            resource.Role,
            resource.Street,
            resource.Number,
            resource.City,
            resource.PostalCode,
            resource.Country,
            resource.WebSite,
            resource.Biography);
    }
}