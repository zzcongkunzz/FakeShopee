using System.Diagnostics;
using FakeShopee.constant;
using Microsoft.AspNetCore.Mvc;
using FakeShopee.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Memory;

namespace FakeShopee.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMemoryCache memoryCache;
    private readonly int pageSize;
    private HomeData homeData;
    private FindProductByCriteriaRequest FindProductByCriteriaRequest;
    private MyDbContext MyDbContext{ get;}
    
    public HomeController(MyDbContext context ,ILogger<HomeController> logger,  IMemoryCache memoryCache, IConfiguration configuration)
    {
        _logger = logger;
        MyDbContext = context;
        this.memoryCache = memoryCache;
        pageSize = configuration.GetValue<int>("PageSize");
        homeData = new HomeData();
        homeData.ListCategory = MyDbContext.Categories
            .OrderByDescending(category => category.Name)
            .ToList();

        DataApp dataApp = new DataApp(context);
    }

    public IActionResult Index()
    {
        ViewBag.findProductByCriteria = new FindProductByCriteriaRequest
        {
            PageTotal = (int)Math.Ceiling((double)MyDbContext.Products.Count() / pageSize),
        };
        homeData.ListProduct = MyDbContext.Products.Take(pageSize).ToList();
        return View(homeData);
    }

    public async Task<List<Product>> GetAllProduct()
    {
        var cacheData = memoryCache.Get<IEnumerable<Product>>("ListProduct");
        
        if (cacheData == null)
        {
            var expirationTime = DateTimeOffset.Now.AddMinutes(1);
            homeData.ListProduct = await MyDbContext.Products.ToListAsync();
            memoryCache.Set("ListProduct", homeData.ListProduct, expirationTime);
        }
        else
        {
            homeData.ListProduct = cacheData.ToList();
        }

        return homeData.ListProduct;
    }
    
    [HttpGet]
    [Route("Default/FindProductByCriteria")]
    public IActionResult FindProductByCriteria(FindProductByCriteriaRequest findProductByCriteriaRequest)
    {
        IQueryable<Product> products = MyDbContext.Products
            .Include(p => p.Category)
            .AsQueryable();
        
        if (!string.IsNullOrEmpty(findProductByCriteriaRequest.NameProductOrCategory))
        {
            string name = findProductByCriteriaRequest.NameProductOrCategory.ToLower();
            products = products.Where(p => (p.Name.ToLower().Contains(name) ||
                                                  p.Category.Name.ToLower().Contains(name)
                                                  ));

            products.ToList();
        }

        if (!string.IsNullOrEmpty(findProductByCriteriaRequest.Category))
        {
            string category = findProductByCriteriaRequest.Category;
            products = products.Where(p => p.Category.Name.ToLower().Equals(category));
        }
        
        if (findProductByCriteriaRequest.SortBy != null)
        {
            switch (findProductByCriteriaRequest.SortBy)
            {
                case SortType.NEW:
                {
                    products = products.OrderBy(p => p.ProductImportDate);
                    break;
                }
                case SortType.HOT_SELLING:
                {
                    products = products.OrderByDescending(p => p.SoldQuantity);
                    break;
                }
                case SortType.PRICE_ASC:
                {
                    products = products.OrderBy(p => ((double)p.Price - ((double)p.Price * (double)p.Discount / 100)));
                    break;
                }
                case SortType.PRICE_DESC:
                {
                    products = products.OrderByDescending(p => ((double)p.Price - ((double)p.Price * (double)p.Discount / 100)));
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
        
        findProductByCriteriaRequest.PageTotal = (int)Math.Ceiling((double)products.Count() / pageSize);
        
        if (findProductByCriteriaRequest.PageNumber != null)
        {
            int pageTo = (findProductByCriteriaRequest.PageNumber - 1) * pageSize;
            products = products.Skip(pageTo).Take(pageSize);
        }
        
        ViewBag.findProductByCriteria = findProductByCriteriaRequest;
        
        homeData.ListProduct = products.ToList();
        
        return View("Index", homeData);
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}