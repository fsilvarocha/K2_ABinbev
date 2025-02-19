using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Services;

public class SalesService
{
    private readonly List<Sale> _sales = new();

    public Sale CreateSale(string saleNumber, DateTime saleDate, string customer, string branch, List<SaleItem> items)
    {
        Sale sale = new(saleNumber, saleDate, customer, branch);
        foreach (SaleItem item in items)
        {
            sale.AddItem(item);
        }

        _sales.Add(sale);
        return sale;
    }

    public List<Sale> GetAllSales()
    {
        return _sales;
    }

    public Sale GetSaleById(Guid saleId) =>
        _sales.FirstOrDefault(s => s.Id == saleId);


    public void CancelSale(Guid saleId)
    {
        Sale sale = GetSaleById(saleId);
        if (sale == null)
            throw new Exception("Sales not found");

        sale.CancelSale();
    }
}
