using FakeShopee.constant;

namespace FakeShopee.Models;

public class FindProductByCriteriaRequest
{
    public String NameProductOrCategory { get; set; }
    public String Category { get; set; }
    public SortType SortBy { get; set; } = SortType.POPULAR;
    public int PageNumber { get; set; } = 1;

}