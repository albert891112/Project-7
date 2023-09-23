document.addEventListener("DOMContentLoaded", function(){

    //畫面初始化，取得所有商品
    initLoadPay();
   
//paymentmethod======================================================
// 做出選擇後，顯示注意事項
var paymentMethodSelect = document.getElementById("paymentMethodSelect");    
var shippingMethodSelect = document.getElementById("shippingMethodSelect");   

document.getElementById("carddesc").style.display = "none";
document.getElementById("shippinghomedesc").style.display = "none";
document.getElementById("shippingsuperstoredesc").style.display = "none";
document.getElementById("coupondesc").style.display = "none";

//付款方式顯示注意事項
paymentMethodSelect.addEventListener("change", function () 
{                     
   if(paymentMethodSelect.value == "信用卡"){        
    document.getElementById("carddesc").style.display = "block";        
   }else{
    document.getElementById("carddesc").style.display = "none";
   }
});

//運送方式顯示注意事項
shippingMethodSelect.addEventListener("change", function () 
{            
   if(shippingMethodSelect.value == "送到府"){
    document.getElementById("shippinghomedesc").style.display = "block";  
    document.getElementById("shippingsuperstoredesc").style.display = "none";      
   }
   if(shippingMethodSelect.value == "超商取貨"){
    document.getElementById("shippingsuperstoredesc").style.display = "block";
    document.getElementById("shippinghomedesc").style.display = "none";
   }
});

// payment end====================================================

//orderData  begin======================================================
document.getElementById("btnNextOrderData").addEventListener("click", function() {    
       
    var paymentMethodselectedOption = paymentMethodSelect.options[paymentMethodSelect.selectedIndex].value;   
    var paymentMethodSelectresultLabel = document.getElementById("paymentMethodValue");
   
    paymentMethodSelectresultLabel.textContent =  paymentMethodselectedOption;

    //如選選擇信用卡 顯示填寫信用卡資料
    if(paymentMethodSelect.value == "信用卡"){        
        document.getElementById("creditCardFieldsPill").style.display = "block";
        document.getElementById("cvvFieldPill").style.display = "block";
       }else{
        document.getElementById("creditCardFieldsPill").style.display = "none";
        document.getElementById("cvvFieldPill").style.display = "none";
       }

});


$("#btnNextOrderData").on("click", function() {    

    // 獲取選取的選項值
    var shippingMethodselectedOption = $("#shippingMethodSelect option:selected").val();

    // 獲取顯示結果的標籤元素
    var ShippingMethodValue = $("#ShippingMethodValue");

    // 更新標籤的內容以顯示選擇的選項
    ShippingMethodValue.text(shippingMethodselectedOption);
});



//orderData  end======================================================

//checkout  begin======================================================
document.getElementById("btnNextCheckout").addEventListener("click", function() {    
       
    var paymentMethodselectedOption = paymentMethodSelect.options[paymentMethodSelect.selectedIndex].value;   
    var paymentMethodSelectresultLabel = document.getElementById("paymentMethodValue1");
   
    paymentMethodSelectresultLabel.textContent =  paymentMethodselectedOption;

    if(paymentMethodSelect.value == "信用卡"){        
        document.getElementById("creditCardFields").style.display = "block";
        document.getElementById("cvvField").style.display = "block";
       }else{
        document.getElementById("creditCardFields").style.display = "none";
        document.getElementById("cvvField").style.display = "none";
       }

});




//coupon begin======================================================

//btn上下頁 begin===========================================
//回到購物車
const btnLastCart = document.getElementById("btnLastCart");

btnLastCart.addEventListener("click", function () {
  
    LastpageCart();
    // window.location.href = "Cart.html"; 
     //todo getcaritems();
    initLoad();

    });

    //顯示下一頁orderDataHtml
    $("#btnNextOrderData").click(function(){
        showOrderData();            
    });      

    //顯示上一頁payHtml
        $("#btnLastPay").click(function(){
            
            showpay();         

        }); 
    //顯示下一頁checkoutHtml
        $("#btnNextCheckout").click(function(){

            showcheckout();    
                
        }); 

        $("#btnLastOrderData").click(function(){

            showOrderData();    
                
        }); 

        $("#btnNextOrderFinish").click(function(){

            OrderFinish();    
                
        }); 
    
});
//設定顯示checkoutHtml
var showcheckout = function(){
  
    $(".payHtml").hide("slow" , "swing");
    $(".orderDataHtml").hide("slow" , "swing")
    $(".checkoutHtml").show("slow" , "swing")   
  
  }
  
//設定初始payHtml
var initLoadPay = function(){

        showpay();
       
    }         

//設定顯示orderDataHtml
var showOrderData = function(){
  
    $(".payHtml").hide("slow" , "swing");
    $(".orderDataHtml").show("slow" , "swing")
    $(".checkoutHtml").hide("slow" , "swing")   
  
  }
//顯示初始payHtml
 var showpay = function(){

    $(".payHtml").show();
    $(".orderDataHtml").hide()
    $(".checkoutHtml").hide()
 }
//設定顯示checkoutHtml
 var showcheckout = function(){
  
    $(".payHtml").hide("slow" , "swing");
    $(".orderDataHtml").hide("slow" , "swing")
    $(".checkoutHtml").show("slow" , "swing")   
  
  }

  //設定感謝訂購
 var OrderFinish = function(){
  
    window.location.href = "OrderFinish.html"; 
  
  }

  var LastpageCart = function(){
  
    window.location.href = "Cart.html"; 
  
  }

//btn上下頁 end===========================================







