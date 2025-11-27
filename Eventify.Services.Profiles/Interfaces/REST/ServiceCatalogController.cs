using System.Net.Mime;
using Eventify.Services.Profiles.Domain.Model.Commands;
using Eventify.Services.Profiles.Domain.Model.Queries;
using Eventify.Services.Profiles.Domain.Services;
using Eventify.Services.Profiles.Interfaces.REST.Resources;
using Eventify.Services.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Eventify.Services.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/profiles/{profileId:int}/service-catalogs")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Service Catalog Endpoints.")]
public class ServiceCatalogsController(
    IServiceCatalogCommandService serviceCatalogCommandService,
    IServiceCatalogQueryService serviceCatalogQueryService) : ControllerBase
{
    [HttpPost("/api/v1/profiles/{profileId:int}/service-catalogs")]
    [SwaggerOperation("Create Service Catalog", "Create a new service catalog for a profile.",
        OperationId = "CreateServiceCatalog")]
    [SwaggerResponse(201, "The service catalog was created.", typeof(ServiceCatalogResource))]
    [SwaggerResponse(400, "The service was not created.")]
    public async Task<IActionResult> CreateServiceCatalog(int profileId, CreateServiceCatalogResource resource)
    {
        var command = CreateServiceCatalogCommandFromResourceAssembler.ToCommandFromResource(profileId, resource);
        var serviceCatalog = await serviceCatalogCommandService.Handle(command);
        if (serviceCatalog is null) return BadRequest();
        var resourceResponse = ServiceCatalogResourceFromEntityAssembler.ToResourceFromEntity(serviceCatalog);
        return CreatedAtAction(nameof(GetServiceCatalogById), new { profileId, serviceCatalogId = serviceCatalog.Id },
            resourceResponse);
    }

    [HttpGet]
    [SwaggerOperation("Get All Service Catalogs by Id", "Get all service catalogs for a profile.",
        OperationId = "GetAllServiceCatalogs")]
    [SwaggerResponse(200, "The service catalogs were found.", typeof(IEnumerable<ServiceCatalogResource>))]
    public async Task<IActionResult> GetAllServiceCatalogs(int profileId)
    {
        var query = new GetServiceCatalogsByProfileIdQuery(profileId);
        var serviceCatalogs = await serviceCatalogQueryService.Handle(query);
        var resources = serviceCatalogs.Select(ServiceCatalogResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{serviceCatalogId:int}")]
    [SwaggerOperation("Get Service Catalog by Id", "Get a service catalog by its identifier.",
        OperationId = "GetServiceCatalogById")]
    [SwaggerResponse(200, "The service catalog was found.", typeof(ServiceCatalogResource))]
    [SwaggerResponse(404, "The service was not found.")]
    public async Task<IActionResult> GetServiceCatalogById(int profileId, int serviceCatalogId)
    {
        var query = new GetServiceCatalogByIdForProfileQuery(profileId, serviceCatalogId);
        var serviceCatalog = await serviceCatalogQueryService.Handle(query);
        if (serviceCatalog is null) return NotFound();
        var resource = ServiceCatalogResourceFromEntityAssembler.ToResourceFromEntity(serviceCatalog);
        return Ok(resource);
    }

    [HttpPut("{serviceCatalogId:int}")]
    [SwaggerOperation("Update Service Catalog", "Update an existing service catalog.",
        OperationId = "UpdateServiceCatalog")]
    [SwaggerResponse(200, "The service catalog was updated.", typeof(ServiceCatalogResource))]
    [SwaggerResponse(404, "The service was not found.")]
    public async Task<IActionResult> UpdateServiceCatalog(int profileId, int serviceCatalogId,
        UpdateServiceCatalogResource resource)
    {
        var command =
            UpdateServiceCatalogCommandFromResourceAssembler.ToCommandFromResource(profileId, serviceCatalogId,
                resource);
        var serviceCatalog = await serviceCatalogCommandService.Handle(command);
        if (serviceCatalog is null) return NotFound();
        var serviceCatalogResource = ServiceCatalogResourceFromEntityAssembler.ToResourceFromEntity(serviceCatalog);
        return Ok(serviceCatalogResource);
    }

    [HttpDelete("{serviceCatalogId:int}")]
    [SwaggerOperation("Delete Service Catalog", "Delete a service catalog.", OperationId = "DeleteServiceCatalog")]
    [SwaggerResponse(204, "The service was deleted.")]
    [SwaggerResponse(404, "The service was not found.")]
    public async Task<IActionResult> DeleteServiceCatalog(int profileId, int serviceCatalogId)
    {
        var command = new DeleteServiceCatalogCommand(profileId, serviceCatalogId);
        var result = await serviceCatalogCommandService.Handle(command);
        if (!result) return NotFound();
        return NoContent();
    }
}