document.addEventListener("DOMContentLoaded", function () {

    getToCart();

    $("#btnPay").click(function () {

        btnPay();

    });

    $("#btnCheckoutHome").click(function () {

        btnCheckoutHome();

    });

    $("#cartTable").on("click", ".btnAdd", function () {

        addOne.call(this);

    });

    $("#cartTable").on("click", ".btnSub", function () {

        subOne.call(this);
    });



});

//let data = []

//$(document).on("change", "#sort", function () {

//    var sortBy = $(this).val();

//    if (sortBy === "unitprice") {

//        data.CartItems.sort(function (a, b) {
//            return b.Product.Price - a.Product.Price;
//        });
//    } else if (sortBy === "subtotal") {

//        data.CartItems.sort(function (a, b) {
//            return b.SubTotal - a.SubTotal;
//        });
//    }

//    //$("#cartTable").empty();

//    setCart(data);

//});

//加一
var addOne = function () {    

    var Qty = 1;

    // 使用 $(this) 來參照當前被點擊的按鈕
    var button = $(this);

    // 使用 closest() 方法找到包含指定類別的父元素
    var parentRow = button.closest("tr");

    // 從父元素中獲取所需的數據
    var Size = parentRow.find(".cart_size").attr("size");
    var Id = parentRow.find(".cart_size").attr("product");

    // 建立商品數據，包括商品ID、購物車數量和尺寸
    var data = {
        "ProductId": Id,
        "Qty": Qty,
        "Size": Size
    }

    //加入購物車
    let url = '/api/CartApi/AddCartItem';

    fetch(url, {
        method: 'POST',
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(data)
    }).then(function (response) {
        console.log("response=", response);
        if (response.ok) {
            //alert("加入購物車成功")
           
            getToCart();
            location.reload();               
        }
    })   
}


//減一
var subOne = function () {

    var Qty = -1;

    // 使用 $(this) 來參照當前被點擊的按鈕
    var button = $(this);

    // 使用 closest() 方法找到包含指定類別的父元素
    var parentRow = button.closest("tr");

    // 從父元素中獲取所需的數據
    var Size = parentRow.find(".cart_size").attr("size");
    var Id = parentRow.find(".cart_size").attr("product");

    //如果數量等於零,移除商品
    



    // 建立商品數據，包括商品ID、購物車數量和尺寸
    var data = {
        "ProductId": Id,
        "Qty": Qty,
        "Size": Size
    }
    
        //加入購物車
        let url = '/api/CartApi/AddCartItem';

        fetch(url, {
            method: 'POST',
            headers: new Headers({
                'Content-Type': 'application/json'
            }),
            body: JSON.stringify(data)
        }).then(function (response) {
            console.log("response=", response);
            if (response.ok) {

                getToCart();
                location.reload();
            }
        })
    
}

var getToCart = function () {

    let url = '/api/CartApi/ShowCart';

    fetch(url, {
        method: 'GET',
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    }).then(function (response) {
        return response.json();
    }).then(function (result) {
        data = result;
        setCart(result);       
    }).catch(function (err) {
        
        console.log(err);
    });

};

var setCart = function (data) {    
   
    var cartTemplate = getCartTemplate("show_cart");
    var total = 0;
    $(".cartData").empty();    
    $.each(data.CartItems, function (index, ele) {
        
        var cartItems = cartTemplate.clone();
        
        cartItems.find(".cart_img").attr("src", "/Files/" + ele.Product.Image);
        cartItems.find(".cart_productName").text(ele.Product.Name);        
        cartItems.find(".cart_size").text(ele.Size);
        cartItems.find(".cart_size").attr("size", ele.Size);
        cartItems.find(".cart_size").attr("product", ele.Product.Id);
        cartItems.find(".cart_unitPrice").text("$" +ele.Product.Price);
        cartItems.find(".cart_qty").text(ele.Qty);
        cartItems.find(".cart_subtotal").text("$" + ele.SubTotal);       
        cartItems.find(".cart_qty").attr("value", ele.Qty);
        cartItems.find(".cart_productName").attr("num", ele.Product.S);
        cartItems.find(".cart_size").attr("num", ele.Product.M);
        cartItems.find(".cart_unitPrice").attr("num", ele.Product.L);
        cartItems.find(".cart_qty").attr("num", ele.Product.XL);

        $(".cartTable").append(cartItems);        
        total += ele.SubTotal;      
    });      
    
    $(".cart_total").text(" 商品金額 : " + total);   
};

var getCartTemplate = function (name) {

    var templateName = "template." + name;
    var template = $(templateName).html();

    return $(template).clone();
}

var btnPay = function () {

    window.location.href = "/Cart/Checkout";

    $(".payHtml").show("slow", "swing");
    $(".orderDataHtml").hide()
    $(".checkoutHtml").hide()

}

var btnCheckoutHome = function () {

    window.location.href = "/Home/Index";

}