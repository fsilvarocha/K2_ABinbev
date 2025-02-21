using Ambev.DeveloperEvaluation.Application.Sales.Commands.AddOrRemoveItemSale;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class AddOrRemoveItemSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly AddOrRemoveItemSaleHandler _handler;
    private readonly DomainValidationContext _domainValidationContext;

    public AddOrRemoveItemSaleHandlerTests()
    {
        _domainValidationContext = new DomainValidationContext();
        _saleRepository = Substitute.For<ISaleRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new AddOrRemoveItemSaleHandler(_domainValidationContext, _productRepository, _saleRepository, _mapper);

    }

    /// <summary>
    /// Verifies that a valid Add Item sale request is processed successfully
    /// </summary>

    [Fact(DisplayName = "The data for adding the item is valid, so return the successful output")]
    public async Task Handle_ValidRequest_ReturnsSucessDatasSaleWithSuccess()
    {
        //Given
        var command = AddOrRemoveItemSaleHandlerTestData.GeneratedValidCommand();

        var product = new Product
        {
            Id = command.ProductId,
            Name = "Product 01",
            Price = 10,
        };

        var sale = new Sale
        {
            Id = command.SaleId,
        };

        var addOrRemoveItemSaleResultTest = new AddOrRemoveItemSaleResult();

        _saleRepository.GetWithSaleItemsByIdAsync(command.SaleId).Returns(sale);
        _productRepository.GetByIdAsync(command.ProductId).Returns(product);
        _mapper.Map<AddOrRemoveItemSaleResult>(Arg.Any<Sale>()).Returns(addOrRemoveItemSaleResultTest);

        _saleRepository.UnitOfWork.Commit().Returns(true);

        //When

        var addOrRemoveItemSaleResult = await _handler.Handle(command, CancellationToken.None);

        //Then

        addOrRemoveItemSaleResult.Should().NotBeNull();
        _saleRepository.Received(1).Update(sale);
        Assert.False(_domainValidationContext.ExistErros);
    }


    /// <summary>
    /// Verifies that when a sale does not exist, the handler returns null and an error message.
    /// </summary>

    [Fact(DisplayName = "Handle Sale not existing should return null with error message")]
    public async Task Handle_SaleNotExist_ReturnsNullWithErrorMessage()
    {
        //Given
        var command = AddOrRemoveItemSaleHandlerTestData.GeneratedValidCommand();

        var addOrRemoveItemSaleResultTest = new AddOrRemoveItemSaleResult();

        _saleRepository.GetWithSaleItemsByIdAsync(command.SaleId).ReturnsNull();

        //When

        var addOrRemoveItemSaleResult = await _handler.Handle(command, CancellationToken.None);

        //Then

        addOrRemoveItemSaleResult.Should().BeNull();

        _saleRepository.Received(0).Update(null);
        Assert.True(_domainValidationContext.ExistErros);
        Assert.Equal("The sale was not found",
            _domainValidationContext.Erros[0].Detail);
    }

    /// <summary>
    /// Verifies that a sale with status 'Finished' cannot be modified, 
    /// returning null and an error message.
    /// </summary>

    [Fact(DisplayName = "Handle Sale not modified due to status 'Finished' returns null and error message")]

    public async Task Handle_SaleNotModified_ReturnsNullAndWithErrorMessage()
    {
        //Given
        var command = AddOrRemoveItemSaleHandlerTestData.GeneratedValidCommand();

        var sale = new Sale
        {
            Id = command.SaleId,
            Status = SaleStatus.Finished
        };

        var addOrRemoveItemSaleResultTest = new AddOrRemoveItemSaleResult();

        _saleRepository.GetWithSaleItemsByIdAsync(command.SaleId).Returns(sale);

        //When

        var addOrRemoveItemSaleResult = await _handler.Handle(command, CancellationToken.None);

        //Then

        addOrRemoveItemSaleResult.Should().BeNull();
        _saleRepository.Received(0).Update(sale);
        Assert.True(_domainValidationContext.ExistErros);

        Assert.Equal($"Sale cannot be modified because it is {sale.Status}",
            _domainValidationContext.Erros[0].Detail);
    }
}
