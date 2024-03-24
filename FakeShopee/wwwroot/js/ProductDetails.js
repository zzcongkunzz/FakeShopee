function ClickProductBox_AmounyItem(){
    var btnSub = document.querySelector(".product_box__subtraction");
    var btnAdd = document.querySelector(".product_box__add");
    var soLuongObj = document.querySelector(".product_box__amouny--vallue");
    soLuong = Number(soLuongObj.value);
    var soLuongKhoObj = document.querySelector(".product_box__warehouse--value");
    soLuongKho = Number(soLuongKhoObj.innerHTML);

    btnSub.onclick = function(e){
        if(soLuong > 1){
            soLuong--;
            soLuongObj.value = soLuong;
        }
        else{
            soLuongObj.value = 1;
        }
    }

    btnAdd.onclick = function(e){
        if(soLuong < soLuongKho){
            soLuong++;
            soLuongObj.value = soLuong;
        }
        else{
            soLuongObj.value = soLuongKho;
        }
    }

    soLuongObj.onkeyup = function(e){
        var input = e.target.value;
        var regex = /^[0-9]+$/;
        if (!regex.test(input)) {
            soLuongObj.value = input.replace(/\D/g, '');
        }
        if(Number(input) > soLuongKho){
            // soLuongObj.innerHTML = soLuongKho;
            soLuongObj.value = soLuongKho;
            // soLuongObj.innerHTML = "";
        }
        if(Number(input) < 1){
            soLuongObj.value = 1;
        }
    }
}

function CheckProductSoldItem() {
    var productSoldItemobj = document.querySelector(".product__sold-item:first-child");
    var productSoldItem = Number(productSoldItemobj.innerHTML);
    if (productSoldItem >= 1000) {
        productSoldItemobj.innerHTML = `${productSoldItem / 1000}k`;
    }
}

function MoneyFomat() {
    var Price = document.getElementsByClassName("product__price");
    for (var i of Price) {
        var x = Number(i.innerHTML);
        i.innerHTML = x.toLocaleString('vi-VN');
    }
    if (Price[0].innerHTML === Price[1].innerHTML) {
        document.querySelector(".product__old-price").style.display = "none";
        document.querySelector(".product__discount").style.display = "none";
    }
}

CheckProductSoldItem();
MoneyFomat();
ClickProductBox_AmounyItem()