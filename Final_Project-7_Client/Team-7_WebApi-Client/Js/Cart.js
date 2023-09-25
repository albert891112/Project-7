document.addEventListener("DOMContentLoaded", function () {

    getToCart();  

    $("#btnPay").click(function () {       

        btnPay();

    });
 

    $("#btnCheckoutHome").click(function () {

        btnCheckoutHome();

    });

    $(document).on("click",".btnAdd", function () {
        
        addOne();
    });

    $(document).on("click", ".btnSub", function () {
        
        subOne();
    });

    
    //$("#sort").change(function () {

    //    var sortBy = $(this).val(); 

    //    if (sortBy === "unitprice") {
          
    //        data.CartItems.sort(function (a, b) {
    //            return a.Product.Price - b.Product.Price;
    //        });
    //    } else if (sortBy === "subtotal") {
            
    //        data.CartItems.sort(function (a, b) {
    //            return b.SubTotal - a.SubTotal;
    //        });
    //    }
        
    //    $("#cartTable").empty();
     
    //    setCart(data);
    //});
  
});



//�[�@
var addOne = function () {

    //���o�ӫ~Id
    const urlParams = new URLSearchParams(window.location.search);
   // const Id = urlParams.get('Id');

    //�ӫ~�ƶq+1
    var Qty = 1;   

    var Size = $(".cart_size").attr("size");

    var Id = $(".cart_size").attr("product");

    //�إ߰ӫ~��ƨ��o�ӫ~ID,�ʪ����ƶq,�ʪ����ؤo
    var data = {            
        "ProductId": Id,        
        "Qty": Qty,
        "Size": Size
    }
    

    console.log("data=", data);

    //�[�J�ʪ���
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
            //alert("�[�J�ʪ������\")
          
        }
    })
    

}

//��@
var subOne = function () {

    //���o�ӫ~Id
    const urlParams = new URLSearchParams(window.location.search);
    const Id = urlParams.get('Id');

    //�ӫ~�ƶq-1
    var Qty = -1;    

    var Size = Size;



    //�إ߰ӫ~��ƨ��o�ӫ~ID,�ʪ����ƶq,�ʪ����ؤo
    var data = {
        "ProductId": Id,
        "Qty": Qty,
        "Size": Size
    }

    //�[�J�ʪ���
    let url = '/api/CartApi/AddCartItem';

    fetch(url, {
        method: 'POST',
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(data)
    }).then(function (response) {
        console.log("data=", data);
        if (response.ok) {
            //alert("�[�J�ʪ������\")
            getToCart();
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
        setCart(result);       
    }).catch(function (err) {
        
        console.log(err);
    });

};



var setCart = function (data) {
    var cartTemplate = getCartTemplate("show_cart");


    var total = 0;

    $("#cartData").empty();
    $.each(data.CartItems, function (index, ele) {         

        var cartItems = cartTemplate.clone();
        cartItems.find(".cart_img").attr("src", "../../Files/" + ele.Product.Image);
        cartItems.find(".cart_productName").text(ele.Product.Name);
        cartItems.find(".cart_size").text(ele.Size);
        cartItems.find(".cart_size").attr("size", ele.Size);
        cartItems.find(".cart_size").attr("product", ele.Product.Id);
        cartItems.find(".cart_unitPrice").text("$" +ele.Product.Price);
        cartItems.find(".cart_qty").text(ele.Qty);
        cartItems.find(".cart_subtotal").text("$" + ele.SubTotal);
        $("#cartTable").append(cartItems);
        
        total += ele.SubTotal;                   
      
    });
    
    $(".cart_total").text(" Total : " + total);   
};





var getCartTemplate = function (name) {

    var templateName = "template." + name;
    var template = $(templateName).html();

    return $(template).clone();
}



var btnPay = function () {

    window.location.href = "/Cart/Checkout";

}

var btnCheckoutHome = function () {

    window.location.href = "/Home/Index";

}