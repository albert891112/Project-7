document.addEventListener("DOMContentLoaded", function () {  

getToCart();

$("#btnPay").click(function(){

 btnPay();        

 postCart();
     
}); 


var btnPay = function(){

 window.location.href = "Ordermerge.html"; 

}

$("#btnCheckoutHome").click(function(){

 btnCheckoutHome();    
     
});  

var btnCheckoutHome = function(){

 window.location.href = "HomePhage.html"; 

}
});

var postCart = function(cart){
 
    let url = '/api/CartApi/AddCartItem';
    
 
    fetch(url, {
        method: 'POST',
        body: JSON.stringify(cart),
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
  
    
  }


var getToCart = function(){
 
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

  var setCart = function(data){
    var cartTemplate = getCartTemplate("show_cart");

    $.each(data, function (index, ele) {

       var cartItems = cartTemplate.clone();        
       cartItems.find(".cart_img").attr("src", ele.Image);
       cartItems.find(".cart_productName").text(ele.Name);
       cartItems.find(".cart_size").text(ele.Size);
        cartItems.find(".cart_unitPrice").text(ele.UnitPrice);
        cartItems.find(".cart_qty").text(ele.Qty);
        cartItems.find(".cart_subtotal").text(ele.Subtotal);
        cartItems.find(".cart_total").text(ele.Total);
        $("#cartTable").append(cartItems);
    });
  };


  
  var getCartTemplate = function(name){
 
   var templateName = "template." +  name ;
   var template = $(templateName).html();

   return $(template).clone();
}

var postCart = function(cart){
 
    let url = '/api/CartApi/AddCartItem';
    
 
    fetch(url, {
        method: 'POST',
        body: JSON.stringify(cart),
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    }).then(function (response) {
      return response.json();
    }).then(function (result) {
      Cart(result);
    }).catch(function (err) {
        console.log(err);
    });
  
    
  }
