document.addEventListener("DOMContentLoaded", function () {  

getToCart();

$("#btnPay").click(function(){

 btnPay();        

 postCart();
     
}); 


$(document).ready(function () {
  // 獲取表格和排序選項的引用
  var $table = $('#cartTable');
  var $sort = $('#sort');

  // 定義排序函數
  function sortTableByColumn(columnIndex) {
      var rows = $table.find('tbody > tr').get();
      rows.sort(function (a, b) {
          var keyA = $(a).find('td').eq(columnIndex).text();
          var keyB = $(b).find('td').eq(columnIndex).text();
          return keyA.localeCompare(keyB, 'zh-TW'); // 使用繁體中文排序
      });
      $.each(rows, function (index, row) {
          $table.find('tbody').append(row);
      });
  }

  // 當選擇框值發生變化時觸發排序
  $sort.on('change', function () {
      var selectedValue = $(this).val();
      var columnIndex = 0; // 默認按第一列排序

      if (selectedValue === 'unitprice') {
          columnIndex = 3; // 按單價排序
      } else if (selectedValue === 'subtotal') {
          columnIndex = 5; // 按小計遞減排序
      }

      sortTableByColumn(columnIndex);
  });

  // 初始化按默認選項排序
  sortTableByColumn(3); // 默認按單價排序
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
