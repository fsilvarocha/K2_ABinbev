using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CancelSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly DomainValidationContext _domainValidationContext;
    private readonly CancelSaleHandler _handler;

    public CancelSaleHandlerTests()
    {
        _domainValidationContext = new DomainValidationContext();
        _saleRepository = Substitute.For<ISaleRepository>();

        _handler = new CancelSaleHandler(_domainValidationContext, _saleRepository);
    }


    /// <summary>
    /// Verifies that a valid cancel sale request successfully updates the sale's status to 'Cancelled' 
    /// and commits the changes without errors.
    /// </summary>

    [Fact(DisplayName = "Handle valid cancel sale request returns success and updates sale status to 'Cancelled'")]
    public async Task Handle_ValidCancelSaleRequest_ReturnsSuccessAndUpdatesSaleStatusToCancelled
()
    {
        //Given
        var command = new CancelSaleCommand
        {
            SaleId = Guid.NewGuid(),
        };

        var sale = new Sale
        {
            Id = command.SaleId,
            Status = SaleStatus.Initialized
        };

        _saleRepository.GetByIdAsync(command.SaleId).Returns(sale);

        _saleRepository.UnitOfWork.Commit().Returns(true);

        //When

        var cancelSaleResultTest = await _handler.Handle(command, CancellationToken.None);

        //Then

        cancelSaleResultTest.Should().NotBeNull();
        _saleRepository.Received(1).Update(Arg.Is<Sale>(s => s.Status == SaleStatus.Cancelled));
        Assert.False(_domainValidationContext.ExistErros);
    }


    /// <summary>
    /// Verifies that when attempting to cancel a non-existing sale, the handler returns null 
    /// and an appropriate error message.
    /// </summary>
    [Fact(DisplayName = "Handle cancel sale request with non-existing sale returns null and error message")]

    public async Task Handle_SaleNotExist_ReturnsNullWithErrorMessage()
    {
        //Given

        var command = new CancelSaleCommand
        {
            SaleId = Guid.NewGuid(),
        };

        var sale = new Sale
        {
            Id = command.SaleId,
            Status = SaleStatus.Initialized
        };

        _saleRepository.GetByIdAsync(command.SaleId).ReturnsNull();

        //When

        var cancelSaleResultTest = await _handler.Handle(command, CancellationToken.None);

        //Then

        cancelSaleResultTest.Should().BeNull();
        _saleRepository.Received(0).Update(Arg.Any<Sale>());

        Assert.True(_domainValidationContext.ExistErros);
        Assert.Equal("The sale was not found", _domainValidationContext.Erros[0].Detail);
    }


    /// <summary>
    /// Verifies that when attempting to cancel a sale that is already cancelled, 
    /// the handler returns null and an appropriate error message.
    /// </summary>
    [Fact(DisplayName = "Handle cancel sale request with already cancelled sale returns null and error message")]

    public async Task Handle_SaleNotBeCancelled_ReturnsNullWithErrorMessage()
    {
        //Given

        var command = new CancelSaleCommand
        {
            SaleId = Guid.NewGuid(),
        };

        var sale = new Sale
        {
            Id = command.SaleId,
            Status = SaleStatus.Cancelled
        };

        _saleRepository.GetByIdAsync(command.SaleId).Returns(sale);

        //When

        var cancelSaleResultTest = await _handler.Handle(command, CancellationToken.None);

        //Then

        cancelSaleResultTest.Should().BeNull();
        _saleRepository.Received(0).Update(Arg.Any<Sale>());

        Assert.True(_domainValidationContext.ExistErros);
        Assert.Equal($"Sale cannot be Cancelled because it is {sale.Status}",
            _domainValidationContext.Erros[0].Detail);
    }

}
