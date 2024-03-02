namespace FakeShopee.Models;

public class FindProductByCriteriaRequest
{
    public String NameProductOrCategory { get; set; }
    public String Category { get; set; }
    public String SortBy { get; set; }
    public int PageNumber { get; set; }
    
}