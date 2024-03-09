// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
function MoneyFomatCartItem() {
    var price = document.getElementsByClassName("header-mix__cart-product-item--price");
    for (var i of price) {
        var x = Number(i.innerHTML);
        i.innerHTML = x.toLocaleString('vi-VN');
    }
}

function CheckNameProductCartItem() {
    var nameProduct = document.getElementsByClassName("header-mix__cart-product-item--name-product");
    for (var i of nameProduct) {
        var x = i.innerHTML;
        if (x.length >= 40) {
            var thay_the = x.slice(0, 37);
            thay_the += '...';
            i.innerHTML = thay_the;
        }
    }
}

function HeaderCartProduct() {
    var HeaderCartProduct = document.getElementById("header-mix__cart--main");
    var HeaderCartProductItem = document.getElementsByClassName("header-mix__cart-product-link");
    if (HeaderCartProductItem[0] == null) {
        HeaderCartProduct.classList.add("header-mix__cart-list--no-cart");
    }
    else HeaderCartProduct.classList.add("header-mix__cart-list");
}



MoneyFomatCartItem();
CheckNameProductCartItem();
HeaderCartProduct();
// Write your JavaScript code.
