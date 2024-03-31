using System.ComponentModel;
using System.Reflection;

namespace FakeShopee.constant;

public enum RoleType
{
    [Description("Nhân Viên")]
    STAFF,
    [Description("Admin")]
    ADMIN,
    [Description("Khách Hàng")]
    CUSTOMER,
}
