using System.Diagnostics;
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
        homeData.ListCategory = MyDbContext.Categories.ToList();
        // AddData();
    }

    public IActionResult Index()
    {
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
        ViewBag.findProductByCriteria = findProductByCriteriaRequest;
        IQueryable<Product> products = MyDbContext.Products
            .Include(product => product.Category)
            .AsQueryable();
        if (!string.IsNullOrEmpty(findProductByCriteriaRequest.NameProductOrCategory))
        {
            string name = findProductByCriteriaRequest.NameProductOrCategory.ToLower();
            products = products.Where(product => (product.Name.ToLower().Contains(name) ||
                                                  product.Category.Name.ToLower().Contains(name)
                                                  ));

            products.ToList();
        }

        if (!string.IsNullOrEmpty(findProductByCriteriaRequest.Category))
        {
            string category = findProductByCriteriaRequest.Category;
            products = products.Where(product => product.Category.Name.ToLower().Equals(category));
        }

        if (findProductByCriteriaRequest.PageNumber != null)
        {
            int pageTo = findProductByCriteriaRequest.PageNumber * pageSize;
            products = products.Skip(pageTo).Take(pageSize);
        }
        
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

    public void AddData()
    {
        List<Category> Categorys = AddCategory();
       
       AddProduct(Categorys);
    }

    public List<Category> AddCategory()
    {
        List<Category> Categorys = new List<Category>
        {
            new Category { Name = "Thời trang" },
            new Category { Name = "Điện thoại và Phụ Kiện" },
            new Category { Name = "Thiết bị điện tử" },
            new Category { Name = "Máy Tính và Laptop" },
            new Category { Name = "Sức khỏe" }
        };
        
        MyDbContext.Categories.AddRange(Categorys);

        MyDbContext.SaveChanges();

        return Categorys;
    }

    public void AddProduct(List<Category> Categorys)
    {
        List<Product> products = new List<Product>
        {
            new Product()
            {
                Name = "áo khoác nữ sinh Harajuku JK 100% ảnh thật",
                LinkImage = "https://i.imgur.com/SH9AozR.jpg",
                Rating = 4.5,
                Favorite = true,
                Price = 100000,
                Discount = 50,
                InventoryQuantity = 10000000,
                SoldQuantity = 22200,
                ProductImportDate = DateTime.ParseExact("22/10/2023", "dd/MM/yyyy", null),
                Category = Categorys[0],
            },
            new Product
            {
                Name = "áo thun siêu hot",
                LinkImage = "https://i.imgur.com/kJI23qi.jpg",
                Rating = 4,
                Favorite = true,
                Price = 150000,
                Discount = 55,
                InventoryQuantity = 1000000000,
                SoldQuantity = 3333000,
                ProductImportDate = DateTime.ParseExact("20/10/2023", "dd/MM/yyyy", null),
                Category = Categorys[0] // Replace with the actual category
            },
            new Product
            {
                Name = "áo thun nam siêu hot",
                LinkImage = "https://i.imgur.com/Bys2h2s.jpg",
                Rating = 4,
                Favorite = true,
                Price = 150000,
                Discount = 50,
                InventoryQuantity = 1000,
                SoldQuantity = 200,
                ProductImportDate = DateTime.ParseExact("20/10/2023", "dd/MM/yyyy", null),
                Category = Categorys[0] // Replace with the actual category
            },
            new Product
            {
                Name = "áo thun dài tay nữ cá tính",
                LinkImage = "https://i.imgur.com/XKiLgrr.jpg",
                Rating = 5,
                Favorite = true,
                Price = 90000,
                Discount = 30,
                InventoryQuantity = 1000,
                SoldQuantity = 15,
                ProductImportDate = DateTime.ParseExact("25/10/2023", "dd/MM/yyyy", null),
                Category = Categorys[0] // Replace with the actual category
            },
            new Product
            {
                Name = "ốp lưng điện thoại iPhone dễ thương",
                LinkImage = "https://i.imgur.com/jisHoPt.png",
                Rating = 5,
                Favorite = true,
                Price = 50000,
                Discount = 35,
                InventoryQuantity = 1000,
                SoldQuantity = 150,
                ProductImportDate = DateTime.ParseExact("30/10/2023", "dd/MM/yyyy", null),
                Category = Categorys[1] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "điện thoại iPhone 11 chính hãng giá rẻ",
                LinkImage = "https://i.imgur.com/FU9aHWK.jpg",
                Rating = 1.5,
                Favorite = false,
                Price = 15000000,
                Discount = 35,
                InventoryQuantity = 1000,
                SoldQuantity = 150,
                ProductImportDate = DateTime.ParseExact("01/10/2023", "dd/MM/yyyy", null),
                Category = Categorys[1] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "tai nghe hình tai mèo màu xanh cute",
                LinkImage = "https://i.imgur.com/S0zMK33.jpg",
                Rating = 4.5,
                Favorite = true,
                Price = 300000,
                Discount = 10,
                InventoryQuantity = 1000,
                SoldQuantity = 300,
                ProductImportDate = DateTime.ParseExact("10/10/2023", "dd/MM/yyyy", null),
                Category = Categorys[1] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "đèn livestream sịn sò",
                LinkImage = "https://i.imgur.com/D4aJAqR.jpg",
                Rating = 4.5,
                Favorite = true,
                Price = 366000,
                Discount = 13,
                InventoryQuantity = 1000,
                SoldQuantity = 124,
                ProductImportDate = DateTime.ParseExact("10/09/2023", "dd/MM/yyyy", null),
                Category = Categorys[1] // Replace with the actual category
            },
            new Product
            {
                Name = "robot hút bụi thông minh",
                LinkImage = "https://i.imgur.com/bMKor2H.jpg",
                Rating = 0.5,
                Favorite = false,
                Price = 150000,
                Discount = 3,
                InventoryQuantity = 1000,
                SoldQuantity = 60,
                ProductImportDate = DateTime.ParseExact("09/09/2023", "dd/MM/yyyy", null),
                Category = Categorys[2] // Replace with the actual category
            },
            new Product
            {
                Name = "tủ lạnh 2 ngăn dung tích lớn cho đại gia đình",
                LinkImage = "https://i.imgur.com/BuPTCnQ.jpg",
                Rating = 5,
                Favorite = true,
                Price = 20000000,
                Discount = 60,
                InventoryQuantity = 1000,
                SoldQuantity = 360,
                ProductImportDate = DateTime.ParseExact("09/01/2023", "dd/MM/yyyy", null),
                Category = Categorys[2] // Replace with the actual category
            },
            new Product
            {
                Name = "tủ lạnh 3 ngăn dung tích lớn cho đại gia đình",
                LinkImage = "https://i.imgur.com/BuPTCnQ.jpg",
                Rating = 5,
                Favorite = true,
                Price = 20000000,
                Discount = 35,
                InventoryQuantity = 1000,
                SoldQuantity = 360,
                ProductImportDate = DateTime.ParseExact("09/01/2023", "dd/MM/yyyy", null),
                Category = Categorys[2] // Replace with the actual category
            },
            new Product
            {
                Name = "máy giặt cửa ngang siêu tiết kiệm nước",
                LinkImage = "https://i.imgur.com/DsMUwrc.jpg",
                Rating = 3,
                Favorite = false,
                Price = 8200000,
                Discount = 63,
                InventoryQuantity = 1000,
                SoldQuantity = 266,
                ProductImportDate = DateTime.ParseExact("25/01/2023", "dd/MM/yyyy", null),
                Category = Categorys[2] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "máy lọc nước Khangkaru hàng chính hãng",
                LinkImage = "https://i.imgur.com/QrycWGx.jpg",
                Rating = 3.6,
                Favorite = true,
                Price = 5200000,
                Discount = 53,
                InventoryQuantity = 1000,
                SoldQuantity = 133,
                ProductImportDate = DateTime.ParseExact("20/02/2023", "dd/MM/yyyy", null),
                Category = Categorys[2] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "máy tính HP bảo hành trọn đời",
                LinkImage = "https://i.imgur.com/MSfcvUq.jpg",
                Rating = 4.6,
                Favorite = true,
                Price = 9200000,
                Discount = 13,
                InventoryQuantity = 1000,
                SoldQuantity = 333,
                ProductImportDate = DateTime.ParseExact("23/02/2023", "dd/MM/yyyy", null),
                Category = Categorys[3] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "macbook hạt rẻ ngon",
                LinkImage = "https://i.imgur.com/YWFohYq.jpg",
                Rating = 2.6,
                Favorite = false,
                Price = 9230000,
                Discount = 15,
                InventoryQuantity = 1000,
                SoldQuantity = 233,
                ProductImportDate = DateTime.ParseExact("23/03/2023", "dd/MM/yyyy", null),
                Category = Categorys[3] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "dàn PC siêu chất lượng cho game thủ",
                LinkImage = "https://i.imgur.com/ldXj8CM.png",
                Rating = 4.6,
                Favorite = true,
                Price = 52230000,
                Discount = 45,
                InventoryQuantity = 1000,
                SoldQuantity = 500,
                ProductImportDate = DateTime.ParseExact("23/04/2023", "dd/MM/yyyy", null),
                Category = Categorys[3] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "máy tính DELL gọn nhẹ",
                LinkImage = "https://i.imgur.com/ch6LU8h.jpg",
                Rating = 4.6,
                Favorite = true,
                Price = 32000000,
                Discount = 25,
                InventoryQuantity = 1000,
                SoldQuantity = 550,
                ProductImportDate = DateTime.ParseExact("23/03/2023", "dd/MM/yyyy", null),
                Category = Categorys[4] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "máy đo chỉ số SpO2 chính xác",
                LinkImage = "https://i.imgur.com/PzxWaZS.jpg",
                Rating = 4.6,
                Favorite = false,
                Price = 300000,
                Discount = 0,
                InventoryQuantity = 1000,
                SoldQuantity = 10,
                ProductImportDate = DateTime.ParseExact("23/10/2023", "dd/MM/yyyy", null),
                Category = Categorys[4] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "máy đo nhiệt độ chất lượng cao",
                LinkImage = "https://i.imgur.com/Koztkhp.jpg",
                Rating = 1.6,
                Favorite = false,
                Price = 330000,
                Discount = 33,
                InventoryQuantity = 1000,
                SoldQuantity = 100,
                ProductImportDate = DateTime.ParseExact("15/06/2023", "dd/MM/yyyy", null),
                Category = Categorys[4] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "nước xát khuẩn tay, loại bỏ 99,99% vi khuẩn",
                LinkImage = "https://i.imgur.com/KHSaNOp.jpg",
                Rating = 1.6,
                Favorite = false,
                Price = 330000,
                Discount = 99,
                InventoryQuantity = 1000,
                SoldQuantity = 800,
                ProductImportDate = DateTime.ParseExact("15/05/2023", "dd/MM/yyyy", null),
                Category = Categorys[4] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "khẩu trang 4 lớp chắc chắn, bảo vệ bạn và mọi người",
                LinkImage = "https://i.imgur.com/c7sfKX5.jpg",
                Rating = 4,
                Favorite = false,
                Price = 100000,
                Discount = 88,
                InventoryQuantity = 1000,
                SoldQuantity = 860,
                ProductImportDate = DateTime.ParseExact("15/05/2023", "dd/MM/yyyy", null),
                Category = Categorys[4] // Thay thế bằng danh mục thực tế
            },
        };
        
        MyDbContext.Products.AddRange(products);

        MyDbContext.SaveChanges();
    }
}