using Ambev.DeveloperEvaluation.Common.Domain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.AddOrRemoveItemSale;

/// <summary>
/// Represents the command to add an item to a sale.
/// </summary>
public class AddOrRemoveItemSaleCommand : Command<AddOrRemoveItemSaleResult?>
{
    /// <summary>
    /// Unique identifier of the sale.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Unique identifier of the product to be added to the sale.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Quantity of the product to be added to the sale.
    /// </summary>
    public int QuantityProduct { get; set; }


    public override bool IsValid()
    {
        var validationResult = new AddOrRemoveItemSaleCommandValidator().Validate(this);
        return validationResult.IsValid;
    }


}
