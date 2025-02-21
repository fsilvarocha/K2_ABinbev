using Ambev.DeveloperEvaluation.Common.Domain.Messages;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companys.CreateCompany;

public class CreateCompanyHandler : CommandHandler, IRequestHandler<CreateCompanyCommand, CreateCompanyResult?>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public CreateCompanyHandler(DomainValidationContext domainNotificationContext,
        ICompanyRepository companyRepository, IMapper mapper) : base(domainNotificationContext)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<CreateCompanyResult?> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        if (!ValidCommand(command)) return null;

        if (command.HeadOfficeId != null)
        {
            var existsHeadOffice = await _companyRepository.GetByIdAsync(command.HeadOfficeId.Value);

            if (existsHeadOffice == null)
            {
                AddError("Create Company Error", "The specified head office does not exist. Please " +
                    "check the provided details and try again.");
            }

            if (command.IsHeadOffice)
            {
                AddError("Create Company Error", $"The company is not the head office. " +
                    $"Please verify the company's status and try again.");
                return null;
            }
        }

        var company = _mapper.Map<Company>(command);

        company.Id = Guid.NewGuid();

        _companyRepository.Add(company);

        if (!await PersistData(_companyRepository.UnitOfWork))
            return null;

        var result = _mapper.Map<CreateCompanyResult>(company);

        return result;
    }
}
