using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.Commands;
using Eventify.Services.Planning.Domain.Repositories;
using Eventify.Services.Planning.Domain.Services;
using Eventify.Shared.Domain.Repositories;

namespace Eventify.Services.Planning.Application.Internal.CommandServices;

public class QuoteCommandService(IQuoteRepository quoteRepository, IUnitOfWork unitOfWork) : IQuoteCommandService
{
    public async Task<Quote?> Handle(CreateQuoteCommand command)
    {
        var quote = new Quote(command);
        await quoteRepository.AddAsync(quote);
        await unitOfWork.CompleteAsync();
        return quote;
    }

    public async Task<Quote?> Handle(UpdateQuoteCommand command)
    {
        var quote = await quoteRepository.FindByIdAsync(command.QuoteId);
        if (quote is null) throw new Exception("Quote not found");
        var updatedQuote = quote.UpdateInformation(command.Title, command.EventType, command.GuestQuantity,
            command.Location, command.TotalPrice, command.EventDate);
        quoteRepository.Update(quote);
        await unitOfWork.CompleteAsync();
        return updatedQuote;
    }

    public async Task Handle(DeleteQuoteCommand command)
    {
        var quote = await quoteRepository.FindByIdAsync(command.QuoteId);
        if (quote is null) throw new Exception("Quote not found");
        quoteRepository.Remove(quote);
        await unitOfWork.CompleteAsync();
    }

    public async Task<string> Handle(ConfirmQuoteCommand command)
    {
        var quote = await quoteRepository.FindByIdAsync(command.QuoteId);
        if (quote is null) throw new ArgumentException("Quote not found");
        quote.Accept();
        quoteRepository.Update(quote);
        await unitOfWork.CompleteAsync();
        return quote.Id.ToString();
    }

    public async Task<string> Handle(RejectQuoteCommand command)
    {
        var quote = await quoteRepository.FindByIdAsync(command.QuoteId);
        if (quote is null) throw new ArgumentException("Quote not found");
        quote.Reject();
        quoteRepository.Update(quote);
        await unitOfWork.CompleteAsync();
        return quote.Id.ToString();
    }
}