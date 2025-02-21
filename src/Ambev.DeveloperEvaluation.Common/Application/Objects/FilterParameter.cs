namespace Ambev.DeveloperEvaluation.Common.Application.Objects;

public abstract class FilterParameter
{
    private const int MaxPageSize = 16;
    public int PageNumber { get; set; } = 1;

    private int _pageSize = 16;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public string? OrderBy { get; set; }

}
