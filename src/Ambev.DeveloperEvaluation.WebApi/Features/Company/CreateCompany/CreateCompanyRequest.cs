namespace Ambev.DeveloperEvaluation.WebApi.Features.Company.CreateCompany;

public class CreateCompanyRequest
{
    /// <summary>
    /// Gets the name of the company.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets  the Tax Identification Number (TIN) for the company.
    /// This is equivalent to the CNPJ in Brazil.
    /// </summary>
    public string TaxId { get; set; }

    /// <summary>
    /// Gets the address of the company.
    /// -
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Gets a value indicating whether this company is the head office (main company).
    /// </summary>
    public bool IsHeadOffice { get; set; } = false;

    /// <summary>
    /// Gets the ID of the head office for this company. This is null if the company is a head office.
    /// </summary>
    public Guid? HeadOfficeId { get; set; }
}
