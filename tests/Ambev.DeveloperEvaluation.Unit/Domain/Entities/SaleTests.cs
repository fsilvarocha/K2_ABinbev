using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{

    /// <summary>
    /// Verifies that the total amount, discount, and amount to pay are correctly calculated 
    /// for a sale without applying any discounts or restrictions.
    /// </summary>
    [Fact(DisplayName = "Sale Calculate Without Discount or Restrictions Updates Total Correctly")]

    public void Sale_CalculateWithoutDiscountAndRestrictions_CalculatesTotalCorrectly()
    {
        //Given
        const decimal totalAmountExptected = 13, discountAmountExpected = 0, amountToPayExpected = 13;

        var sale = SaleTestData.GenerateValidSale();

        Guid productOne = Guid.NewGuid(), productTwo = Guid.NewGuid();
        decimal priceProductOne = 5, priceProductTwo = 4;

        List<string> messageErrors = [];

        //when

        //Addding Items

        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productOne, priceProductOne, 1));
        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productOne, priceProductOne, 1));

        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productTwo, priceProductTwo, 2));

        //Removeing Items

        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productOne, priceProductOne, -1));

        //than
        Assert.Empty(messageErrors);
        Assert.Equal(totalAmountExptected, sale.TotalAmount);
        Assert.Equal(discountAmountExpected, sale.DiscountAmount);
        Assert.Equal(amountToPayExpected, sale.AmountToPay);
    }

    /// <summary>
    /// Verifies that the sale total is recalculated correctly after removing a sale item.
    /// It ensures that the sale's total amount, discount, and amount to pay are accurate after the removal of items.
    /// </summary>
    [Fact(DisplayName = "Sale recalculates total correctly after removing an item")]

    public void Sale_CalculateAfterRemovingSaleItem_CalculatesTotalCorrectly()
    {
        //Given
        const decimal totalAmountExptected = 13, discountAmountExpected = 0, amountToPayExpected = 13;

        var sale = SaleTestData.GenerateValidSale();

        Guid productOne = Guid.NewGuid(), productTwo = Guid.NewGuid();
        decimal priceProductOne = 5, priceProductTwo = 4;

        List<string> messageErrors = [];

        //when

        //Addding Items

        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productOne, priceProductOne, 2));
        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productTwo, priceProductTwo, 2));

        //Removeing Items

        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productOne, priceProductOne, -2));

        //than
        Assert.Empty(messageErrors);
        Assert.Equal(totalAmountExptected, sale.TotalAmount);
        Assert.Equal(discountAmountExpected, sale.DiscountAmount);
        Assert.Equal(amountToPayExpected, sale.AmountToPay);
    }



    /// <summary>
    /// Verifies that when a product's quantity exceeds the maximum allowed, 
    /// an error is returned, and the sale total is calculated correctly.
    /// </summary>
    [Fact(DisplayName = "Sale calculates total correctly with max quantity restriction and returns error for exceeding quantity")]

    public void Sale_CalculateWithMaxQuantityRestriction_ReturnsErrorAndCalculatesTotalCorrectly()
    {
        //Given
        const int totalAmountExptected = 19, discountAmountExpected = 0, amountToPayExpected = 19;

        var sale = SaleTestData.GenerateValidSale();

        Guid productOne = Guid.NewGuid(), productTwo = Guid.NewGuid();
        decimal priceProductOne = 5, priceProductTwo = 4;

        List<string> messageErrors = [];

        //when

        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productOne, priceProductOne, 1));
        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productOne, priceProductOne, 2));

        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productTwo, priceProductTwo, 1));
        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productTwo, priceProductTwo, 20));

        //than

        Assert.Single(messageErrors);

        Assert.Equal(totalAmountExptected, sale.TotalAmount);
        Assert.Equal(discountAmountExpected, sale.DiscountAmount);
        Assert.Equal(amountToPayExpected, sale.AmountToPay);
    }

    /// <summary>
    /// Verifies that when attempting to add a sale item with a negative quantity, 
    /// an error message is returned and the sale's total remains correct.
    /// </summary>
    [Fact(DisplayName = "Sale Add Item with Negative Quantity Returns Error and Calculates Total Correctly")]

    public void Sale_AddItemWithNegativeQuantity_ReturnsErrorAndCalculatesTotalCorrectly()
    {
        //Given
        const int totalAmountExptected = 5, discountAmountExpected = 0, amountToPayExpected = 5;

        var sale = SaleTestData.GenerateValidSale();

        Guid productOne = Guid.NewGuid(), productTwo = Guid.NewGuid();
        decimal priceProductOne = 5, priceProductTwo = 4;

        List<string> messageErrors = [];

        //when

        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productOne, priceProductOne, 1));

        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productTwo, priceProductTwo, -1));

        //than

        Assert.Single(messageErrors);
        Assert.Contains("It is not possible to add a product item with zero or negative quantity.", messageErrors);

        Assert.Equal(totalAmountExptected, sale.TotalAmount);
        Assert.Equal(discountAmountExpected, sale.DiscountAmount);
        Assert.Equal(amountToPayExpected, sale.AmountToPay);
    }


    /// <summary>
    /// Verifies that when a discount is applied to a sale, 
    /// the total amount, discount amount, and amount to pay are calculated correctly.
    /// </summary>
    [Fact(DisplayName = "Sale Calculate Discount and Update Total Correctly")]

    public void Sale_CalculateWithDiscount_AppliesDiscountAndCalculatesTotalCorrectly()
    {
        //Given
        const decimal totalAmountExptected = 88, discountAmountExpected = 17.6M, amountToPayExpected = 70.4M;

        var sale = SaleTestData.GenerateValidSale();

        Guid productOne = Guid.NewGuid(), productTwo = Guid.NewGuid();
        decimal priceProductOne = 5, priceProductTwo = 4;

        List<string> messageErrors = [];

        //when

        //Addding Items

        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productOne, priceProductOne, 1));
        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productTwo, priceProductTwo, 2));

        AddErrorIfExists(messageErrors, sale.AddOrRemoveSaleItem(productOne, priceProductOne, 15));

        //than
        Assert.Empty(messageErrors);
        Assert.Equal(totalAmountExptected, sale.TotalAmount);
        Assert.Equal(discountAmountExpected, sale.DiscountAmount);
        Assert.Equal(amountToPayExpected, sale.AmountToPay);
    }

    private void AddErrorIfExists(List<string> messageErrors, string error)
    {
        if (!string.IsNullOrWhiteSpace(error))
            messageErrors.Add(error);
    }

}
