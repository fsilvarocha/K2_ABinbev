using Ambev.DeveloperEvaluation.Application.Sales.Events;
using Ambev.DeveloperEvaluation.Common.Domain.Messages;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using static Ambev.DeveloperEvaluation.Domain.Entities.Sale;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.StartSale;

public class StartSaleHandler : CommandHandler, IRequestHandler<StartSaleCommand, StartSaleResult?>
{

    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public StartSaleHandler(DomainValidationContext domainValidationContext, ISaleRepository saleRepository,
        IMapper mapper) : base(domainValidationContext)
    {
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }
    }

    public async Task<StartSaleResult?> Handle(StartSaleCommand command, CancellationToken cancellationToken)
    {

        if (!ValidCommand(command)) return null;

        var newSale = SaleFactory.Initiate(command.ClientId, command.CompanyId);

        _saleRepository.Add(newSale);
        newSale.AddEvent(new SaleCreatedEvent(newSale.Id));

        if (!await PersistData(_saleRepository.UnitOfWork))
            return null;

        var result = _mapper.Map<StartSaleResult>(newSale);
        return result;
    }

}
