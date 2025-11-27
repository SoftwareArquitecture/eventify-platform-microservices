using System.Net.Mime;
using Eventify.Services.Operation.Domain.Model.Queries;
using Eventify.Services.Operation.Domain.Services;
using Eventify.Services.Operation.Interfaces.REST.Resources;
using Eventify.Services.Operation.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Eventify.Services.Operation.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Review Endpoints")]
public class ReviewsController(
    IReviewCommandService reviewCommandService,
    IReviewQueryService reviewQueryService
) : ControllerBase
{
    [HttpGet("{reviewId:int}")]
    [SwaggerOperation(
        Summary = "Get Tutorial by Id",
        Description = "Returns a tutorial by its unique identifier.",
        OperationId = "GetReviewById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Review found", typeof(ReviewResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Review not found")]
    public async Task<IActionResult> GetReviewById([FromRoute] int reviewId)
    {
        var review = await reviewQueryService.Handle(new GetReviewByIdQuery(reviewId));
        if (review is null) return NotFound();
        var resource = ReviewResourceFromEntityAssembler.ToResourceFromEntity(review);
        return Ok(resource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Reviews",
        Description = "Returns a list of all available reviews.",
        OperationId = "GetAllReviews")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of reviews", typeof(IEnumerable<ReviewResource>))]
    public async Task<IActionResult> GetAllReviews()
    {
        var reviews = await reviewQueryService.Handle(new GetAllReviewsQuery());
        var reviewResources = reviews
            .Select(ReviewResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(reviewResources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create Review",
        Description = "Creates a new review and returns the created review resource.",
        OperationId = "CreateReview")]
    [SwaggerResponse(StatusCodes.Status201Created, "Review created successfully", typeof(ReviewResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Review could not be created")]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewResource resource)
    {
        var createReviewCommand = CreateReviewCommandFromResourceAssembler.ToCommandFromResource(resource);
        var review = await reviewCommandService.Handle(createReviewCommand);
        if (review is null) return BadRequest("Review could not be created");
        var createdResource = ReviewResourceFromEntityAssembler.ToResourceFromEntity(review);
        return CreatedAtAction(nameof(GetReviewById), new { reviewId = createdResource.Id }, createdResource);
    }
}