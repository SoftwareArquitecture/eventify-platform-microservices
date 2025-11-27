using System.Net.Mime;
using Eventify.Services.Planning.Domain.Model.Queries;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Eventify.Services.Planning.Domain.Services;
using Eventify.Services.Planning.Interfaces.REST.Resources;
using Eventify.Services.Planning.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Eventify.Services.Planning.Interfaces.REST;

[ApiController]
[Route("api/v1/organizers/{organizerId}/quotes")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Organizers")]
public class OrganizerQuotesController(IQuoteQueryService quoteQueryService, IQuoteCommandService quoteCommandService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Gets quotes by Organizer Id", Description = "Gets quotes by Organizer Id",
        OperationId = "GetQuotesByOrganizerId")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of quotes ", typeof(IEnumerable<QuoteResource>))]
    public async Task<IActionResult> GetQuotesByOrganizerId([FromRoute] int organizerId)
    {
        var getAllQuotesByOrganizerIdQuery = new GetAllQuotesByOrganizerIdQuery(new OrganizerId(organizerId));
        var quotes = await quoteQueryService.Handle(getAllQuotesByOrganizerIdQuery);
        var quoteResources = quotes.Select(QuoteResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(quoteResources);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Creates a new quote for organizer",
        Description = "Creates a new quote for the specified organizer and returns the created quote resource",
        OperationId = "CreateQuoteForOrganizer")]
    [SwaggerResponse(StatusCodes.Status201Created, "The quote resource was created", typeof(QuoteResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Quote could not be created.")]
    public async Task<IActionResult> CreateQuoteForOrganizer([FromRoute] int organizerId,
        [FromBody] CreateQuoteResource resource)
    {
        var createQuoteCommand = CreateQuoteCommandFromResourceAssembler.ToCommandFromResource(resource);
        var quote = await quoteCommandService.Handle(createQuoteCommand);
        if (quote is null) return BadRequest("Quote could not be created.");
        var createdQuoteResource = QuoteResourceFromEntityAssembler.ToResourceFromEntity(quote);
        return CreatedAtAction(nameof(GetQuotesByOrganizerId), new { organizerId }, createdQuoteResource);
    }

    [HttpPut("{quoteId}")]
    [SwaggerOperation(Summary = "Updates a quote for organizer",
        Description = "Updates a quote for the specified organizer", OperationId = "UpdateQuoteForOrganizer")]
    [SwaggerResponse(StatusCodes.Status200OK, "The quote resource was successfully updated", typeof(QuoteResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Quote could not be updated.")]
    public async Task<IActionResult> UpdateQuoteForOrganizer([FromRoute] int organizerId, [FromRoute] string quoteId,
        [FromBody] UpdateQuoteResource resource)
    {
        var updateQuoteCommand = UpdateQuoteCommandFromResourceAssembler.ToCommandFromResource(quoteId, resource);
        var updatedQuote = await quoteCommandService.Handle(updateQuoteCommand);
        if (updatedQuote is null) return BadRequest("Quote not found");
        var updatedQuoteResource = QuoteResourceFromEntityAssembler.ToResourceFromEntity(updatedQuote);
        return Ok(updatedQuoteResource);
    }
}