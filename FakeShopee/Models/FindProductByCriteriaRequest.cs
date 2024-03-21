using FakeShopee.constant;

namespace FakeShopee.Models;

public class FindProductByCriteriaRequest : ICloneable
{
    public String NameProductOrCategory { get; set; }
    public String Category { get; set; }
    public SortType SortBy { get; set; } = SortType.POPULAR;
    public int PageNumber { get; set; } = 1;

    public int PageTotal { get; set; } = 1;

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}