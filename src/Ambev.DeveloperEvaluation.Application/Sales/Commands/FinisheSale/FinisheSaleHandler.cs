using Ambev.DeveloperEvaluation.Common.Domain.Messages;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.FinisheSale;

public class FinisheSaleHandler : CommandHandler, IRequestHandler<FinisheSaleCommand, FinisheSaleResult?>
{
    private readonly ISaleRepository _saleRepository;

    public FinisheSaleHandler(DomainValidationContext domainValidationContext, ISaleRepository saleRepository)
        : base(domainValidationContext)
    {
        _saleRepository = saleRepository;
    }

    public async Task<FinisheSaleResult?> Handle(FinisheSaleCommand command, CancellationToken cancellationToken)
    {
        if (!ValidCommand(command)) return null;

        var sale = await _saleRepository.GetByIdAsync(command.SaleId);

        if (sale == null)
        {
            AddError("Cancel Sale Error", "The sale was not found");
            return null;
        }

        if (!sale.IsSaleActiveForModification())
        {
            AddError("Sale Error", $"Sale cannot be Finished because it is {sale.Status}");
            return null;
        }

        sale.FinisheSale();

        _saleRepository.Update(sale);

        if (!await PersistData(_saleRepository.UnitOfWork))
            return null;

        return new FinisheSaleResult { Success = true };
    }
}
