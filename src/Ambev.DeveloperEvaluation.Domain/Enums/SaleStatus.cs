namespace Ambev.DeveloperEvaluation.Domain.Enums;

public enum SaleStatus
{

    /// <summary>
    /// Indicates that sale is in progress, that is, adding or removing products
    /// </summary>
    Initialized = 3,

    /// <summary>
    /// Indicates that sale is with Cancelled Status
    /// </summary>
    Cancelled = 6,

    /// <summary>
    /// Indicates that sale is with Fineshed Status
    /// </summary>
    Finished = 9
}
