using Ambev.DeveloperEvaluation.Application.Sales.Commands.StartSale;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class StartSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly StartSaleHandler _handler;
    private readonly DomainValidationContext _domainValidationContext;

    public StartSaleHandlerTests()
    {
        _domainValidationContext = new DomainValidationContext();
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _mediator = Substitute.For<IMediator>();
        _handler = new StartSaleHandler(_domainValidationContext, _saleRepository, _mapper);

    }

    /// <summary>
    /// Tests that a valid start sale request is handled successfully.
    /// </summary>

    [Fact(DisplayName = "Sale data is valid, so return successful output data")]
    public async Task Handle_ValidRequest_ReturnsSucessDatasSaleWithSuccess()
    {
        //Given
        var command = StartSaleHandlerTestData.GenerateValidCommand();

        var StartSaleResultTest = new StartSaleResult
        {
            Id = Guid.NewGuid(),
            Number = 1,
        };

        _saleRepository.UnitOfWork.Commit().Returns(true);
        _mapper.Map<StartSaleResult>(Arg.Any<Sale>()).Returns(StartSaleResultTest);

        //When
        var createUserResult = await _handler.Handle(command, CancellationToken.None);

        //Then
        createUserResult.Should().NotBeNull();
    }

    /// <summary>
    /// Tests the behavior of the handler when an invalid sale request is received.
    /// </summary>

    [Fact(DisplayName = "Sale data is invalid, it should return null and with errors.")]
    public async Task Handle_InvalidRequest_ReturnsNullAndWIthError()
    {
        //Given
        var command = new StartSaleCommand();
        var StartSaleResultTest = new StartSaleResult { };

        _saleRepository.UnitOfWork.Commit().Returns(true);
        _mapper.Map<StartSaleResult>(Arg.Any<Sale>()).Returns(StartSaleResultTest);

        //When
        var createUserResult = await _handler.Handle(command, CancellationToken.None);

        //Then
        createUserResult.Should().BeNull();
        Assert.True(_domainValidationContext.ExistErros);
    }
}

