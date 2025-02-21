using Ambev.DeveloperEvaluation.Application.Sales.Commands.FinisheSale;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class FinishSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly DomainValidationContext _domainValidationContext;
    private readonly FinisheSaleHandler _handler;

    public FinishSaleHandlerTests()
    {
        _domainValidationContext = new DomainValidationContext();
        _saleRepository = Substitute.For<ISaleRepository>();

        _handler = new FinisheSaleHandler(_domainValidationContext, _saleRepository);
    }

    /// <summary>
    /// Verifies that a valid finish sale request successfully updates the sale's status to 'Finished' 
    /// and commits the changes without errors.
    /// </summary>
    [Fact(DisplayName = "Handle valid finish sale request returns success and updates sale status to 'Finished'")]
    public async Task Handle_ValidFinishSaleRequest_ReturnsSuccessAndUpdatesSaleStatusToFinished()
    {
        //Given
        var command = new FinisheSaleCommand
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
        var finishSaleResultTest = await _handler.Handle(command, CancellationToken.None);

        //Then
        finishSaleResultTest.Should().NotBeNull();
        _saleRepository.Received(1).Update(Arg.Is<Sale>(s => s.Status == SaleStatus.Finished));
        Assert.False(_domainValidationContext.ExistErros);
    }

    /// <summary>
    /// Verifies that when attempting to finish a non-existing sale, the handler returns null 
    /// and an appropriate error message.
    /// </summary>
    [Fact(DisplayName = "Handle finish sale request with non-existing sale returns null and error message")]
    public async Task Handle_SaleNotExist_ReturnsNullWithErrorMessage()
    {
        //Given
        var command = new FinisheSaleCommand
        {
            SaleId = Guid.NewGuid(),
        };

        _saleRepository.GetByIdAsync(command.SaleId).ReturnsNull();

        //When
        var finishSaleResultTest = await _handler.Handle(command, CancellationToken.None);

        //Then
        finishSaleResultTest.Should().BeNull();
        _saleRepository.Received(0).Update(Arg.Any<Sale>());
        Assert.True(_domainValidationContext.ExistErros);
        Assert.Equal("The sale was not found", _domainValidationContext.Erros[0].Detail);
    }

    /// <summary>
    /// Verifies that when attempting to finish a sale that is already finished, 
    /// the handler returns null and an appropriate error message.
    /// </summary>
    [Fact(DisplayName = "Handle finish sale request with already finished sale returns null and error message")]
    public async Task Handle_SaleAlreadyFinished_ReturnsNullWithErrorMessage()
    {
        //Given
        var command = new FinisheSaleCommand
        {
            SaleId = Guid.NewGuid(),
        };

        var sale = new Sale
        {
            Id = command.SaleId,
            Status = SaleStatus.Finished
        };

        _saleRepository.GetByIdAsync(command.SaleId).Returns(sale);

        //When
        var finishSaleResultTest = await _handler.Handle(command, CancellationToken.None);

        //Then
        finishSaleResultTest.Should().BeNull();
        _saleRepository.Received(0).Update(Arg.Any<Sale>());
        Assert.True(_domainValidationContext.ExistErros);
        Assert.Equal($"Sale cannot be Finished because it is {sale.Status}",
            _domainValidationContext.Erros[0].Detail);
    }
}
