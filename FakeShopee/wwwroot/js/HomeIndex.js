// function CreateOnClick_productHeader__productHeadeItem(){
//
//     var productHeadeItem = document.querySelectorAll(".product-header-left > .product-header__item");
//     for(var i = 0; i < productHeadeItem.length - 1; i++){
//         productHeadeItem[i].addEventListener("click", function(e){
//             for(var x of productHeadeItem){
//                 x.classList.remove("product-header__item-follow");
//             }
//             e.target.classList.add("product-header__item-follow");
//             document.querySelector(".product-header__item--price-text").innerHTML = "Giá";
//
//         });
//     }
//
//     var productHeaderSortPrice = document.getElementsByClassName("product-header-sort_price__item");
//     for(var i of productHeaderSortPrice){
//         i.addEventListener("click", function(e){
//             document.querySelector(".product-header__item--price-text").innerHTML = e.target.innerHTML;
//             var productHeadeItem_lastChild = document.querySelector(".product-header__item:last-child");
//             var productHeadeItem = document.querySelectorAll(".product-header-left > .product-header__item");
//             for(var i of productHeadeItem){
//                 i.classList.remove("product-header__item-follow");
//             }
//             productHeadeItem_lastChild.classList.add("product-header__item-follow");
//         });
//     }
//    
// }

// function CreateOnClick_CategoryItem(){
//     var categoryItem = document.getElementsByClassName("category-item");
//     var categoryItemLink = document.getElementsByClassName("category-item-link");
//     for(var i of categoryItemLink){
//         i.addEventListener("click", function(e){
//             for(var x of categoryItem){
//                 x.classList.remove("category-item-follow");
//             }
//             // console.log(e);
//             // e.path[1].classList.add("category-item-follow");
//             e.target.parentNode.classList.add("category-item-follow");
//         });
//     }
// }

function CheckNameProduct(){
    var nameProduct = document.getElementsByClassName("product-item__review");
    for(var i of nameProduct){
        var x = i.innerHTML;
        // console.log(x);
        if(x.length >= 50){
            var thay_the = x.slice(0, 47);
            thay_the += '...';
            i.innerHTML = thay_the;
        }
    }
}

function SoldQuantityFomat(){
    var SoldQuantity = document.querySelectorAll(".product-item__Quantity-purchased span");
    for(var i of SoldQuantity){
        let x =  Number(i.innerHTML);
        if(x > 1000000){
            x = (x/1000000.0).toFixed(1) + "tr";
        }
        else if(x > 1000){
            x = (x/1000.0).toFixed(1) + "k";
        }
        i.innerHTML = x;
        // console.log(x);
    }
}

function MoneyFomat(){
    var Price = document.getElementsByClassName("product-item__price");
    for(var i of Price){
        var x = Number(i.innerHTML);
        i.innerHTML = x.toLocaleString('vi-VN');
    }
}



//khởi chạy
// CreateOnClick_productHeader__productHeadeItem();
// CreateOnClick_CategoryItem();
CheckNameProduct();
MoneyFomat();
SoldQuantityFomat();
