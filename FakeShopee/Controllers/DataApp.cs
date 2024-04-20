using FakeShopee.constant;
using FakeShopee.Models;
using Microsoft.Extensions.Caching.Memory;

namespace FakeShopee.Controllers;

public class DataApp
{
    private MyDbContext MyDbContext{ get;}
    
    public DataApp(MyDbContext context )
    {
        MyDbContext = context;

        DeleteAll();
        
        List<UserGroup> userGroups = AddUserGroup();

        List<Users> users = AddUser(userGroups);
        
        List<Category> Categorys = AddCategory();
       
        AddProduct(Categorys);
    }
    

    public List<UserGroup> AddUserGroup()
    {
        
        List<UserGroup> userGroups = new List<UserGroup>
        {
            new UserGroup
            {
                Name = RoleType.ADMIN.GetDescription()
            },
            new UserGroup
            {
                Name = RoleType.STAFF.GetDescription()
            },
            new UserGroup
            {
                Name = RoleType.CUSTOMER.GetDescription()
            }
        };
        
        MyDbContext.UserGroups.AddRange(userGroups);

        MyDbContext.SaveChanges();

        return userGroups;
    }
    
    public List<Users> AddUser(List<UserGroup> userGroups)
    {
        List<Users> users = new List<Users>
        {
            new Users
            {
              FullName  = "Đặng Thành Công",
              UserGroup = userGroups[0],
              PhoneNumber = "0914133719",
              Email = "dangcong200502@gmail.com",
              Password = "1",
              Gender = GenderType.Nam,
            },
        };
        
        MyDbContext.Users.AddRange(users);

        MyDbContext.SaveChanges();

        return users;
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
                LinkImage = "~/img/sanpham1.jpg",
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
                LinkImage = "~/img/sanpham2.jpg",
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
                LinkImage = "~/img/sanpham3.jpg",
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
                Name = "áo thun nam siêu hot 2",
                LinkImage = "~/img/sanpham3.jpg",
                Rating = 4.4,
                Favorite = true,
                Price = 200000,
                Discount = 50,
                InventoryQuantity = 1000,
                SoldQuantity = 200,
                ProductImportDate = DateTime.ParseExact("20/10/2023", "dd/MM/yyyy", null),
                Category = Categorys[0] // Replace with the actual category
            },
            new Product
            {
                Name = "áo thun dài tay nữ cá tính",
                LinkImage = "~/img/sanpham4.jpg",
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
                LinkImage = "~/img/sanpham5.jpg",
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
                LinkImage = "~/img/sanpham6.jpg",
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
                Name = "điện thoại iPhone 11 chính hãng giá đắt",
                LinkImage = "~/img/sanpham6.jpg",
                Rating = 1.5,
                Favorite = false,
                Price = 500000000,
                Discount = 35,
                InventoryQuantity = 1000,
                SoldQuantity = 150,
                ProductImportDate = DateTime.ParseExact("01/10/2023", "dd/MM/yyyy", null),
                Category = Categorys[1] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "tai nghe hình tai mèo màu xanh cute",
                LinkImage = "~/img/sanpham7.jpg",
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
                LinkImage = "~/img/sanpham8.jpg",
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
                LinkImage = "~/img/sanpham9.jpg",
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
                LinkImage = "~/img/sanpham10.jpg",
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
                LinkImage = "~/img/sanpham11.jpg",
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
                LinkImage = "~/img/sanpham12.jpg",
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
                LinkImage = "~/img/sanpham12.jpg",
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
                LinkImage = "~/img/sanpham13.jpg",
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
                LinkImage = "~/img/sanpham14.jpg",
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
                Name = "macbook hạt rẻ ngon 2",
                LinkImage = "~/img/sanpham14.jpg",
                Rating = 2.6,
                Favorite = false,
                Price = 9930000,
                Discount = 15,
                InventoryQuantity = 1000,
                SoldQuantity = 233,
                ProductImportDate = DateTime.ParseExact("23/03/2023", "dd/MM/yyyy", null),
                Category = Categorys[3] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "macbook hạt rẻ ngon 3",
                LinkImage = "~/img/sanpham14.jpg",
                Rating = 2.6,
                Favorite = false,
                Price = 8830000,
                Discount = 15,
                InventoryQuantity = 1000,
                SoldQuantity = 233,
                ProductImportDate = DateTime.ParseExact("23/03/2023", "dd/MM/yyyy", null),
                Category = Categorys[3] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "dàn PC siêu chất lượng cho game thủ",
                LinkImage = "~/img/sanpham15.jpg",
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
                Name = "máy đo chỉ số SpO2 chính xác",
                LinkImage = "~/img/sanpham17.jpg",
                Rating = 4.6,
                Favorite = true,
                Price = 320000,
                Discount = 25,
                InventoryQuantity = 1000,
                SoldQuantity = 550,
                ProductImportDate = DateTime.ParseExact("23/03/2023", "dd/MM/yyyy", null),
                Category = Categorys[4] // Thay thế bằng danh mục thực tế
            },
            new Product
            {
                Name = "máy đo chỉ số SpO2 chính xác",
                LinkImage = "~/img/sanpham17.jpg",
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
                LinkImage = "~/img/sanpham18.jpg",
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
                LinkImage = "~/img/sanpham19.jpg",
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
                LinkImage = "~/img/sanpham20.jpg",
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

        foreach (Product product in products)
        {
            product.WarehouseQuantity = product.InventoryQuantity - product.SoldQuantity;
        }
        
        MyDbContext.Products.AddRange(products);

        MyDbContext.SaveChanges();
    }

    public void DeleteAll()
    {
        MyDbContext.RemoveRange(MyDbContext.Carts);
        MyDbContext.RemoveRange(MyDbContext.Products);
        MyDbContext.RemoveRange(MyDbContext.Categories);
        MyDbContext.RemoveRange(MyDbContext.Users);
        MyDbContext.RemoveRange(MyDbContext.UserGroups);
        MyDbContext.SaveChanges();
    }
}