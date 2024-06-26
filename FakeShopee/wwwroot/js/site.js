﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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
    console.log('HeaderCartProductItem', HeaderCartProductItem);
    if (HeaderCartProductItem[0] == null) {
        HeaderCartProduct.classList.add("header-mix__cart-list--no-cart");
        HeaderCartProduct.classList.remove("header-mix__cart-list");
    }
    else {
        HeaderCartProduct.classList.remove("header-mix__cart-list--no-cart");
        HeaderCartProduct.classList.add("header-mix__cart-list");
    } 
}

const RenderCartItem = (Cart) => {
    // console.log(Cart);
    let modifiedLinkImage = Cart?.product?.linkImage.substring(1);
    
    return `<a class="header-mix__cart-product-link" href="http://localhost:5295/ProductDetails/Index/${Cart.product.id}" >` +
        '<div class="header-mix__cart-product-item">' +
        '<div class="header-mix__cart-product-item--img" ' +
        `style="background-image: url(${modifiedLinkImage})"></div>` +
        `<div class="header-mix__cart-product-item--name-product">${Cart.product.name}</div>` +
        '<div class="header-mix__cart-product-item--combox-price">' +
        '<span class="header-mix__cart-product-item--unit">đ</span>' +
        `<span class="header-mix__cart-product-item--price">${(100 - Cart.product.discount)/100*Cart.product.price}</span>` +
        '</div>' +
        '</div>' +
        '</a>';
}


const GetCartMinUser = async () => {
    const Cart = document.querySelector(".header-mix__cart-list-item");
    const CartNotice = document.querySelector(".header-mix__cart-notice");
    
    Cart.innerHTML = "";
    const xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = () => {
        // console.log(xhttp)
        let quantity = 0;
        if (xhttp.readyState == 4 && xhttp.status == 200){
            const response = JSON.parse(xhttp.responseText);
            // console.log(response);
            response.forEach((item, index) => {
                Cart.innerHTML += RenderCartItem(item);
                quantity += item.quantity;
            })
            CartNotice.innerHTML = quantity;
        }

    }

    xhttp.open("GET", "http://localhost:5295/User/GetCartUser/", false);
    await xhttp.send();
    
    await HeaderCartProduct();
    MoneyFomatCartItem();
    CheckNameProductCartItem();
    
}

const CheckUserLogin = async () => {
    const dangki = document.getElementById("dangki");
    const dangnhap = document.getElementById("dangnhap");
    const User = document.getElementById("User");

    const xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = () => {
        // console.log(xhttp)
        if (xhttp.readyState == 4 && xhttp.status == 200){
            const response = JSON.parse(xhttp.responseText);
            
            dangki.style.display = "none";
            dangnhap.style.display = "none";
            User.style.display = "block";
            const UserImg = document.querySelector("#User > img");
            const UserName = document.querySelector("#User .userName")
            UserImg.src = response.fullName === "Đặng Thành Công" ? "/img/avata.jpg" : ""
            UserName.innerHTML = response.fullName;
            GetCartMinUser();
            
        }
        else {
            dangki.style.display = "block";
            dangnhap.style.display = "block";
            User.style.display = "none";
        }
    }

    xhttp.open("GET", "http://localhost:5295/User/GetUserLogin/", false);
    await xhttp.send();
}
CheckUserLogin();


const DangXuat = () => {
    const xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = () => {
        if (xhttp.readyState == 4 && xhttp.status == 200){
            alert("Đăng xuất thành công")
            window.location.reload();

        }
    }

    xhttp.open("GET", "http://localhost:5295/User/LogOut/", true);
    xhttp.send();
}


// Write your JavaScript code.