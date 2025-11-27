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
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available quotes endpoints")]
public class QuotesController(IQuoteCommandService quoteCommandService, IQuoteQueryService quoteQueryService)
    : ControllerBase
{
    [HttpGet("{quoteId}")]
    public async Task<IActionResult> GetQuoteById([FromRoute] string quoteId)
    {
        var quote = await quoteQueryService.Handle(new GetQuoteByIdQuery(new QuoteId(Guid.Parse(quoteId))));
        if (quote is null) return NotFound();
        var quoteResource = QuoteResourceFromEntityAssembler.ToResourceFromEntity(quote);
        return Ok(quoteResource);
    }


    [HttpPost]
    [SwaggerOperation(Summary = "Creates a new quote",
        Description = "Creates a new quote and return the created quote resource", OperationId = "CreateQuote")]
    [SwaggerResponse(StatusCodes.Status201Created, "The quote resource was created", typeof(QuoteResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Quote could not be created.")]
    public async Task<IActionResult> CreateQuote([FromBody] CreateQuoteResource resource)
    {
        var createQuoteCommand = CreateQuoteCommandFromResourceAssembler.ToCommandFromResource(resource);
        var quote = await quoteCommandService.Handle(createQuoteCommand);
        if (quote is null) return BadRequest("Quote could not be created.");
        var createdQuoteResource = QuoteResourceFromEntityAssembler.ToResourceFromEntity(quote);
        return CreatedAtAction(nameof(GetQuoteById), new { quoteId = createdQuoteResource.Id }, createdQuoteResource);
    }

    [HttpPut("{quoteId}")]
    [SwaggerOperation(Summary = "Updates a quote", Description = "Updates a quote", OperationId = "UpdateQuote")]
    [SwaggerResponse(StatusCodes.Status200OK, "The quote resource was successfully updated", typeof(QuoteResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Quote could not be updated.")]
    public async Task<IActionResult> UpdateQuote([FromRoute] string quoteId, [FromBody] UpdateQuoteResource resource)
    {
        var updateQuoteCommand = UpdateQuoteCommandFromResourceAssembler.ToCommandFromResource(quoteId, resource);
        var updatedQuote = await quoteCommandService.Handle(updateQuoteCommand);
        if (updatedQuote is null) return BadRequest("Quote not found");
        var updatedQuoteResource = QuoteResourceFromEntityAssembler.ToResourceFromEntity(updatedQuote);
        return Ok(updatedQuoteResource);
    }

    [HttpDelete("{quoteId}")]
    [SwaggerOperation(Summary = "Deletes quote", Description = "Deletes quote", OperationId = "DeleteQuote")]
    [SwaggerResponse(StatusCodes.Status200OK, "The quote resource was successfully deleted", typeof(QuoteResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Quote not found")]
    public async Task<IActionResult> DeleteQuote([FromRoute] string quoteId)
    {
        var deleteQuoteCommand = new DeleteQuoteCommand(new QuoteId(Guid.Parse(quoteId)));
        await quoteCommandService.Handle(deleteQuoteCommand);
        return Ok("Quote deleted successfully");
    }


    [HttpPost("{quoteId}/confirmations")]
    [SwaggerOperation(Summary = "Confirms quote", Description = "Confirms quote", OperationId = "ConfirmQuote")]
    [SwaggerResponse(StatusCodes.Status200OK, "The quote resource was successfully confirmed", typeof(QuoteResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Quote not found")]
    public async Task<IActionResult> AcceptQuote([FromRoute] string quoteId)
    {
        var confirmQuoteCommand = new ConfirmQuoteCommand(new QuoteId(Guid.Parse(quoteId)));
        var confirmedQuoteCommand = await quoteCommandService.Handle(confirmQuoteCommand);
        if (string.IsNullOrEmpty(confirmedQuoteCommand)) return BadRequest("Quote not found");
        var message = "Quote confirmed successfully";
        return Ok(message);
    }

    [HttpPost("{quoteId}/rejections")]
    [SwaggerOperation(Summary = "Rejects quote", Description = "Rejects quote", OperationId = "RejectQuote")]
    [SwaggerResponse(StatusCodes.Status200OK, "The quote resource was successfully rejected", typeof(QuoteResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Quote not found")]
    public async Task<IActionResult> RejectQuote([FromRoute] string quoteId)
    {
        var rejectQuoteCommand = new RejectQuoteCommand(new QuoteId(Guid.Parse(quoteId)));
        var rejectedQuoteCommand = await quoteCommandService.Handle(rejectQuoteCommand);
        if (string.IsNullOrEmpty(rejectedQuoteCommand)) return BadRequest("Quote not found");
        var message = "Quote rejected successfully";
        return Ok(message);
    }
}