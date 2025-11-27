using System.Net.Mime;
using Eventify.Services.Planning.Domain.Model.Commands;
using Eventify.Services.Planning.Domain.Model.Queries;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Eventify.Services.Planning.Domain.Services;
using Eventify.Services.Planning.Interfaces.REST.Resources;
using Eventify.Services.Planning.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Eventify.Services.Planning.Interfaces.REST;

[ApiController]
[Route("api/v1/quotes/{quoteId}/service-items")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Quotes")]
public class QuoteServiceItemsController(
    IServiceItemCommandService serviceItemCommandService,
    IQuoteQueryService quoteQueryService,
    IServiceItemQueryService serviceItemQueryService) : ControllerBase
{
    [HttpGet("{serviceItemId}")]
    [SwaggerOperation(Summary = "Get a service item by id", Description = "Get a service item by id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Service Item found", typeof(ServiceItemResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Service Item Not Found")]
    public async Task<IActionResult> GetServiceItemById([FromRoute] string quoteId, [FromRoute] string serviceItemId)
    {
        var quote = await quoteQueryService.Handle(new GetQuoteByIdQuery(new QuoteId(Guid.Parse(quoteId))));
        if (quote is null) return NotFound("Quote not found or doesn't exist");
        var serviceItem =
            await serviceItemQueryService.Handle(
                new GetServiceItemByIdQuery(new ServiceItemId(Guid.Parse(serviceItemId))));
        if (serviceItem is null) return NotFound("Service item not found");
        var serviceItemResource = ServiceItemResourceFromEntityAssembler.ToResourceFromEntity(serviceItem);
        return Ok(serviceItemResource);
    }


    [HttpPost]
    [SwaggerOperation(Summary = "Add a service item", Description = "Add a service item")]
    [SwaggerResponse(StatusCodes.Status200OK, "Service Item Added", typeof(ServiceItemResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Service Item could not be added")]
    public async Task<IActionResult> CreateServiceItem([FromRoute] string quoteId,
        [FromBody] CreateServiceItemResource resource)
    {
        var quote = await quoteQueryService.Handle(new GetQuoteByIdQuery(new QuoteId(Guid.Parse(quoteId))));
        if (quote is null) return BadRequest("Service Item could not be added");
        var createServiceItemCommand = CreateServiceItemCommandFromResourceAssembler.ToCommandFromResource(resource);
        var serviceItem = await serviceItemCommandService.Handle(createServiceItemCommand);
        if (serviceItem is null) return BadRequest("Service Item could not be added");
        var createdResource = ServiceItemResourceFromEntityAssembler.ToResourceFromEntity(serviceItem);
        return CreatedAtAction(nameof(GetServiceItemById),
            new { quoteId = quote.Id.Identifier.ToString(), serviceItemId = createdResource.Id }, createdResource);
    }

    [HttpPut("{serviceItemId}")]
    [SwaggerOperation(Summary = "Update a service item", Description = "Update a service item")]
    [SwaggerResponse(StatusCodes.Status200OK, "Service Item Updated", typeof(ServiceItemResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Service Item could not be updated")]
    public async Task<IActionResult> UpdateServiceItem([FromRoute] string quoteId, [FromRoute] string serviceItemId,
        [FromBody] UpdateServiceItemResource resource)
    {
        var quote = await quoteQueryService.Handle(new GetQuoteByIdQuery(new QuoteId(Guid.Parse(quoteId))));
        if (quote is null) return BadRequest("Service Item could not be added");
        var updateServiceItemCommand =
            UpdateServiceItemCommandFromResourceAssembler.ToCommandFromResource(serviceItemId, resource);
        var updatedServiceItem = await serviceItemCommandService.Handle(updateServiceItemCommand);
        if (updatedServiceItem is null) return BadRequest("Service Item could not be updated");
        var updatedResource = ServiceItemResourceFromEntityAssembler.ToResourceFromEntity(updatedServiceItem);
        return Ok(updatedResource);
    }

    [HttpDelete("{serviceItemId}")]
    [SwaggerOperation(Summary = "Delete a service item", Description = "Delete a service item")]
    [SwaggerResponse(StatusCodes.Status200OK, "Service Item Deleted")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Service Item could not be deleted")]
    public async Task<IActionResult> DeleteServiceItem([FromRoute] string quoteId, [FromRoute] string serviceItemId)
    {
        var quote = await quoteQueryService.Handle(new GetQuoteByIdQuery(new QuoteId(Guid.Parse(quoteId))));
        if (quote is null) return BadRequest("Service Item could not be added");
        var deleteServiceItemCommand = new DeleteServiceItemCommand(new ServiceItemId(Guid.Parse(serviceItemId)));
        await serviceItemCommandService.Handle(deleteServiceItemCommand);
        return Ok("Service Item deleted successfully");
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all services of a quote", Description = "Get all services of a quote")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of service items", typeof(IEnumerable<ServiceItemResource>))]
    public async Task<IActionResult> GetServiceItemsByQuoteId([FromRoute] string quoteId)
    {
        var quote = await quoteQueryService.Handle(new GetQuoteByIdQuery(new QuoteId(Guid.Parse(quoteId))));
        if (quote is null) return BadRequest("Quote not found or doesn't exist");
        var getAllServiceItemsByQuoteIdQuery = new GetAllServiceItemsByQuoteIdQuery(new QuoteId(Guid.Parse(quoteId)));
        var serviceItems = await serviceItemQueryService.Handle(getAllServiceItemsByQuoteIdQuery);
        var serviceItemResources =
            serviceItems.Select(ServiceItemResourceFromEntityAssembler.ToResourceFromEntity).ToList();
        return Ok(serviceItemResources);
    }
}