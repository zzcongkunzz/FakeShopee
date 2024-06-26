﻿using System.ComponentModel;
using System.Reflection;

namespace FakeShopee.constant;

public enum SortType
{
    [Description("Phổ Biến")]
    POPULAR,
    [Description("Mới Nhất")]
    NEW,
    [Description("Bán Chạy")]
    HOT_SELLING,
    [Description("Giá: Thấp đến Cao")]
    PRICE_ASC,
    [Description("Giá: Cao đến Thấp")]
    PRICE_DESC
}

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        Type type = value.GetType();
        MemberInfo[] memInfo = type.GetMember(value.ToString());
        if (memInfo != null && memInfo.Length > 0)
        {
            object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs != null && attrs.Length > 0)
                return ((DescriptionAttribute)attrs[0]).Description;
        }
        return value.ToString();
    }
}