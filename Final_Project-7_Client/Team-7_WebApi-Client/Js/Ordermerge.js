document.addEventListener("DOMContentLoaded", function () {

    //初始讀取
    initLoadPay();   

    //下拉清單讀取
    shippingMethod(); 

    paymentMethod();

    couponMethod();

    //計算總金額    
    var shippingcostLabel = $(".shippingcost");
    var couponcostLabel = $(".couponcost");
    var totalAmountLabel = $(".totalAmount"); 
    
    // 下拉列表的更改事件
    $(".shippingMethodSelect, .couponMethodSelect").change(function () {
        
        // 獲取所選選項的值
        var productTotalValue = $(".cart_total").attr("value");
        var shippingMethodValue = $(".shippingMethodSelect").val();
        var couponValue = $(".couponMethodSelect").val();
        var totalAmountValue = $(".totalAmount").attr("value");              
      
        //如果couponcostValue為空值，則設為0
        if (isNaN(couponValue)) {
            couponValue = 0;            
        }

        //如果productTotalValue為空值，則設為0
        if (isNaN(productTotalValue)) {
            productTotalValue = 0;
            
        }

        //如果shippingMethodValue為空值，則設為0
        if (isNaN(shippingMethodValue)) {
            shippingMethodValue = 0;            
        }

        //如果總金額為NAN，則設為0
        if (isNaN(totalAmountValue)) {
            totalAmountValue = 0;                     
        }
        
        // 進行相應的加法操作
        var cart_totalValue = parseFloat(productTotalValue);
        var shippingcostValue = parseFloat(shippingMethodValue);
        var couponcostValue = parseFloat(couponValue);        

        totalAmountValue = cart_totalValue + shippingcostValue - couponcostValue;       

        //如果totalAmount為小於0，則設為0
        if (totalAmountValue < 0) {
            totalAmountValue = 0;
            alert("總金額小於0");
        }
      
        // 更新標籤       
        shippingcostLabel.text(shippingcostValue);
        shippingcostLabel.attr("value", shippingcostValue);
        couponcostLabel.text(couponcostValue);        
        couponcostLabel.attr("value", couponcostValue);
        totalAmountLabel.text(totalAmountValue);
        $(".totalAmount").attr("value", totalAmountValue);


        //將選取Coupon的值存入label
        $(".couponNameValue").text($(".couponMethodSelect option:selected").text());

        //將選取shipping的值存入label
        $(".shippingcost").attr("value", shippingMethodValue);  

                
        //將運送方式選擇的值存入label     

       var selectedValue = $(".shippingMethodSelect").val();
       
        var selectedOption = $(".shippingMethodSelect option[value='" + selectedValue + "']");
       
        var shippingMethodselectedOption = selectedOption.text();
    
        var shippingMethodSelectresultLabel = $(".shippingMethodValue");

        shippingMethodSelectresultLabel.text(shippingMethodselectedOption);  

    });  

    //paymentmethod Start======================================================
     //做出選擇後，顯示注意事項
    var paymentMethodSelect = document.getElementById("paymentMethodSelect");
    var shippingMethodSelect = document.getElementById("shippingMethodSelect");
    
    //付款change方式顯示注意事項
    $(document).on("change", ".paymentMethodSelect", function () {
        if ($(".paymentMethodSelect").val() == "金融卡") {
            document.getElementById("carddesc").style.display = "block";
        } else {
            document.getElementById("carddesc").style.display = "none";
        }
    });   

    //運送change事件方式顯示注意事項
    $(document).on("change", ".shippingMethodSelect", function () {
      
        if ($(".shippingMethodSelect").val() == "130") {

            document.getElementById("shippinghomedesc").style.display = "block";
            document.getElementById("pillAddressTable").style.display = "block";
            document.getElementById("shippingsuperstoredesc").style.display = "none";

        } else if ($(".shippingMethodSelect").val() == "70") {
            document.getElementById("shippingsuperstoredesc").style.display = "block";
            document.getElementById("shippinghomedesc").style.display = "none";
            document.getElementById("pillAddressTable").style.display = "none";

        } else {
            document.getElementById("shippingsuperstoredesc").style.display = "none";
            document.getElementById("shippinghomedesc").style.display = "none";
            document.getElementById("pillAddressTable").style.display = "none";
        }
    });

    //按下OrderData的Click事件 Start======================================================
    document.getElementById("btnNextOrderData").addEventListener("click", function () {        

        getEmail();

        //將付款方式選擇的值存入label
        var paymentMethodselectedOption = paymentMethodSelect.options[paymentMethodSelect.selectedIndex].value;
        var paymentMethodSelectresultLabel = document.getElementById("paymentMethodValue");

        paymentMethodSelectresultLabel.textContent = paymentMethodselectedOption;     


        //如選選擇金融卡 顯示填寫金融卡資料
        if (paymentMethodSelect.value == "金融卡") {
            document.getElementById("creditCardFieldsPill").style.display = "block";
            document.getElementById("cvvFieldPill").style.display = "block";
        } else {
            document.getElementById("creditCardFieldsPill").style.display = "none";
            document.getElementById("cvvFieldPill").style.display = "none";
        }

        //驗證付款方式、運送方式是否沒選擇
        if (paymentMethodSelect.value === "請選擇" || shippingMethodSelect.value === "請選擇") {

            alert("請選擇付款方式及運送方式");
            return;
        }
        showOrderData();

    });

//按下OrderData的Click事件 End======================================================

  //按下Pay上一頁的Click事件 Start======================================================
    $("#btnLastCart").click(function () {

        LastpageCart();

        initLoad();

    });

//按下Pay上一頁的Click事件 End======================================================

    // payment end====================================================

    //orderData  begin======================================================
    document.getElementById("btnNextCheckout").addEventListener("click", function () {

        var paymentMethodselectedOption = paymentMethodSelect.options[paymentMethodSelect.selectedIndex].value;
        var paymentMethodSelectresultLabel = document.getElementById("paymentMethodValue1");

        paymentMethodSelectresultLabel.textContent = paymentMethodselectedOption;

        //如選選擇金融卡 顯示填寫金融卡資料
        if (paymentMethodSelect.value == "金融卡") {
            document.getElementById("creditCardFields").style.display = "block";
            document.getElementById("cvvField").style.display = "block";
        } else {
            document.getElementById("creditCardFields").style.display = "none";
            document.getElementById("cvvField").style.display = "none";
        }

        //將金融卡日期input的值存入label
        var inputexpiryDate = document.getElementById("expiryDate").value;
        var myselfLabelexpiryDateValue = document.getElementById("myselfLabelexpiryDateValue");
        myselfLabelexpiryDateValue.textContent = inputexpiryDate;

        //將金融卡安全碼input的值存入label
        var inputcvv = document.getElementById("cvv").value;
        var myselfLabelcvvValue = document.getElementById("myselfLabelcvvValue");
        myselfLabelcvvValue.textContent = inputcvv;

        //將姓名input的值存入label
        var inputName = document.getElementById("inputName").value; 
        var myselfLabelNameValue = document.getElementById("myselfLabelNameValue");
        myselfLabelNameValue.textContent = inputName;

        $(".inputName").attr("value", inputName);
      
        //將地址textarea的值存入label
        var inputAddress = document.getElementById("inputAddress").value;
        var myselfLabelAddressValue = document.getElementById("myselfLabelAddressValue");
        myselfLabelAddressValue.textContent = inputAddress;

        $(".inputAddress").attr("value", inputAddress);
        //將電話input的值存入label
        var inputPhone = document.getElementById("inputPhone").value;
        var myselfLabelPhoneValue = document.getElementById("myselfLabelPhoneValue");
        myselfLabelPhoneValue.textContent = inputPhone;

        $(".inputPhone").attr("value", inputPhone);

        //驗證姓名、地址、電話是否為空值    
        var nameError = document.getElementById("nameError");        
        var addressError = document.getElementById("addressError");
        var phoneError = document.getElementById("phoneError");
        var parts = expiryDate.value.split('/');
        var month = parseInt(parts[0], 10);

        if (expiryDate.value === "" && paymentMethodSelect.value === "金融卡") {

            //如果金融卡日期為空值,顯示錯誤訊息
            showExpiryDateError();

            expiryDateError.textContent = "請輸入有效期限";

            return;
        }        
        else if (!/^\d{2}\/\d{2}$/.test(expiryDate.value) && paymentMethodSelect.value === "金融卡") {

            //如果金融卡日期不是MM/YY格式,顯示錯誤訊息
            showExpiryDateError();

            expiryDateError.textContent = "請輸入格式為MM/YY";
            return;
        }
        else if (month < 1 || month > 12 && paymentMethodSelect.value === "金融卡") {
            //如果金融卡日期不是1~12月,顯示錯誤訊息

            showExpiryDateError();

            expiryDateError.textContent = "月份必須在1到12之間";
            return;
        }
        else if (cvv.value === "" && paymentMethodSelect.value === "金融卡") {

            //如果安全碼為空值,顯示錯誤訊息
            showCvvError();

            cvvError.textContent = "請輸入安全碼";
            return;
        }
        else if (cvv.value.length !== 3 && paymentMethodSelect.value === "金融卡") {

            //如果安全碼不是3碼,顯示錯誤訊息
            showCvvError();

            cvvError.textContent = "安全碼必須為3碼";
            return;
        }
        else if (inputName === "") {

            //如果姓名為空值,顯示錯誤訊息
            showNameError();

            nameError.textContent = "請輸入姓名";
            return;
        }
        else if (inputAddress === "" && shippingMethodSelect.value === "送到府") {

            //如果地址為空值,顯示錯誤訊息
            showAddressError();

            addressError.textContent = "請輸入地址";
            return;
        }
        else if (inputPhone === "") {

            //如果電話為空值,顯示錯誤訊息
            showPhoneError();

            phoneError.textContent = "請輸入手機號碼";
            return;

        } else if (!/^\d+$/.test(inputPhone) || !/^09\d{8}$/.test(inputPhone)) {

            //如果電話不是數字或格式不是0900000000,顯示錯誤訊息
            showPhoneError();

            phoneError.textContent = "手機號碼只能是數字且格式為0900000000";
            return;
        }

        initErrortext();

        showCheckout();

    });

    //顯示上一頁payHtml
    $("#btnLastPay").click(function () {

        showPay();

    });
    //orderData  end======================================================


    //checkout  begin======================================================

    $("#btnNextOrderFinish").click(function () {

        postOrderData();

        OrderFinish();      

    });

    $("#btnLastOrderData").click(function () {

        showOrderData();

    });
});

//設定函數庫=============================================================================================
//設定送出訂單 Start=============================================================================================

var postOrderData = function () {
    
    //將ORDERS參數放入變數
    var MemberId = $(".head_productName").attr("memberId")    
    var PhoneNumber = $(".inputPhone").attr("value");
    var Address = $(".inputAddress").attr("value");    
    var ShippingId = $(".shippingMethodSelect option:selected").data("id");
    var PaymentId = $(".paymentMethodSelect option:selected").data("id");     
    var Total = $(".totalAmount").attr("value");
    var OrderStatusId = "2";
    var OrderTime = Date.now();
    var CouponId = "2";

    if (ShippingId == "70") {
       
        Address = "超商取貨";
    }    
   

    // 建立商品數據，包括商品ID、購物車數量和尺寸  
    var data = {       
            "MemberId": MemberId,
            "PhoneNumber": PhoneNumber,
            "Address": Address,
            "ShippingId": ShippingId,
            "PaymentId": PaymentId,
            "Total": Total,
            "OrderStatusId": OrderStatusId,
            "OrderTime": OrderTime,
            "CouponId": CouponId        
    }       

    let url = '/api/OrderApi/CreateOrder';

    fetch(url, {
        method: 'POST',
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(data)
    }).then(function (response) {           
       
    })
}

//設定送出訂單 END=============================================================================================

//設定取用CouponMethodSelect Start=============================================================================================
var setcoupon = function (data) {

    var couponSelect = $('.couponMethodSelect');
    couponSelect.empty();

    //塞入一個option為請選擇
    var firstOption = document.createElement('option');
    $(firstOption).attr('data-id', 0);
    firstOption.innerText = '請選擇';
    couponSelect.append(firstOption);

    //塞入CouponMethodSelect的值
    data.forEach(function (ele) {
        var option = document.createElement('option');
        $(option).attr('data-id', ele.Id);
        $(option).attr('desc', ele.CouponDescription);
        $(option).attr('enable', ele.Enalbe);
        $(option).attr('endDate', ele.EndDate)
        $(option).attr('discountType', ele.DiscountTypeId)
        option.innerText = ele.CouponName;
        option.value = ele.DiscountValue;
        couponSelect.append(option);
    });
};

var couponMethod = function () {

    let url = '/api/CartApi/GetCouponMethod';

    fetch(url, {
        method: 'GET',
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    }).then(function (response) {
        return response.json();
    }).then(function (result) {
        setcoupon(result);
    }).catch(function (err) {
        console.log(err);
    });
};

//設定取用CouponMethodSelect END=============================================================================================

//設定取用paymentMethodSelect Start=============================================================================================
var setPayment = function (data) {
    var paymentSelect = $('.paymentMethodSelect');
    paymentSelect.empty();

    //塞入一個option為請選擇
    var firstOption = document.createElement('option');
    $(firstOption).attr('data-id', 0);
    firstOption.innerText = '請選擇';
    paymentSelect.append(firstOption);

    //創建Select下的option並塞入值
        data.forEach(function (ele) {
            var option = document.createElement('option');
            $(option).attr('data-id', ele.Id);               
            option.innerText = ele.PaymentMethod;           

            paymentSelect.append(option);
        });   
};

var paymentMethod = function () {

    let url = '/api/CartApi/GetPaymentMethod';

    fetch(url, {
        method: 'GET',
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    }).then(function (response) {
        return response.json();
    }).then(function (result) {       
        setPayment(result);   
    }).catch(function (err) {

        console.log(err);
    });
};

//設定取用paymentMethodSelect END=============================================================================================

//設定取用Email Start=============================================================================================
var getEmail = function () {   
    
    var account = $("#btnNextOrderData").attr("data-value");    

    var url = "/api/MemberApi/GetEmail?account="+ account ;

    fetch(url, {
        method: 'GET',
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    }).then(function (response) {
        return response.json();
    }).then(function (result) {
        console.log(result);
       //將信箱其塞入label
        var myselfLabelEmailValue = document.getElementById("myselfLabelEmailValue");
        myselfLabelEmailValue.textContent = result.Email;

    }).catch(function (err) {
        console.log(err);
    });
}

//設定取用Email END=============================================================================================

//設定取用ShippingMethod Start=============================================================================================
var setShipping = function (data) {
    
    var shippingSelect = $('.shippingMethodSelect');
    shippingSelect.empty();

    //塞入一個option為請選擇
    var firstOption = document.createElement('option');
    $(firstOption).attr('data-id', 0);
    firstOption.innerText = '請選擇';
    shippingSelect.append(firstOption);

    //塞入ShippingMethodSelect的值
    data.forEach(function (ele) {
        var option = document.createElement('option');
        $(option).attr('data-id', ele.Id);
        option.innerText = ele.ShippingMethod;
        option.value = ele.Price;
        shippingSelect.append(option);       
       
    });
};
var shippingMethod = function () {   

    let url = '/api/CartApi/GetShippingMethod';

    fetch(url, {
        method: 'GET',
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    }).then(function (response) {
        return response.json();
    }).then(function (result) {
        setShipping(result);
    }).catch(function (err) {

        console.log(err);
    });

};

//設定取用ShippingMethod END=============================================================================================

//設定getCartTemplate Start=============================================================================================
var getTemplate = function (name) {

    var templateName = "template." + name;
    var template = $(templateName).html();

    return $(template).clone();
}

//設定getCartTemplate END=============================================================================================

//設置cartItems Start=============================================================================================
var cartItems = function (data) {    
    var cartTemplate = getTemplate("cartItem_list");
    var total = 0;   

    $(".cartItemData").empty();

    //塞入會員ID,用於傳回
    $(".head_productName").attr("memberId", data.MemberId);

    //塞入購物車商品資料
    $.each(data.CartItems, function (index, ele) {

        var cartItems = cartTemplate.clone();
        
        $(".head_productName").attr("cartId", ele.CartId);
        cartItems.find(".cart_img").attr("src", "../../Files/" + ele.Product.Image);
        cartItems.find(".cart_productName").attr("data_id", ele.ProductId)
        cartItems.find(".cart_productName").text(ele.Product.Name);
        cartItems.find(".cart_size").text(ele.Size);
        cartItems.find(".cart_size").attr("size", ele.Size);
        cartItems.find(".cart_size").attr("product", ele.Product.Id);
        cartItems.find(".cart_unitPrice").text("$" + ele.Product.Price);
        cartItems.find(".cart_unitPrice").attr("value", ele.Product.Price);
        cartItems.find(".cart_qty").text(ele.Qty);
        cartItems.find(".cart_qty").attr("value",ele.Qty);
        cartItems.find(".cart_subtotal").text("$" + ele.SubTotal); 
        cartItems.find(".cart_subtotal").attr("value", ele.SubTotal);    
       
        //將加入的值加進TABLE
        $(".cartTable").append(cartItems);

        total += ele.SubTotal;             
    });
    $(".cartPriceTotal").attr("value", total);
    $(".cartPriceTotal").text("商品總額 : "+ total);
    $(".cart_total").attr("value", total);
    $(".cart_total").text(total);
};

//設置cartItems END=============================================================================================

//設定getToCartItem Start=============================================================================================
var getToCartItem = function () {

    let url = '/api/CartApi/ShowCart';

    fetch(url, {
        method: 'GET',
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    }).then(function (response) {
        return response.json();
    }).then(function (result) {        
         cartItems(result);       
    }).catch(function (err) {
        console.log(err);
    });

};

//設定getToCartItem END=============================================================================================

//設定PayHtml Start=============================================================================================

var showPay = function () {

    $(".payHtml").show("slow", "swing");
    $(".orderDataHtml").hide()
    $(".checkoutHtml").hide()

}

//設定PayHtml END=============================================================================================

//設定initLoadPay Start=============================================================================================

var initLoadPay = function () {

    showPay();

    getToCartItem();
}

//設定initLoadPay END=============================================================================================

//設定顯示checkoutHtml Start=============================================================================================
var showCheckout = function () {

    $(".payHtml").hide("slow", "swing");
    $(".orderDataHtml").hide("slow", "swing")
    $(".checkoutHtml").show("slow", "swing")

}

//設定顯示checkoutHtml END=============================================================================================

//設定顯示orderDataHtml Start=============================================================================================
var showOrderData = function () {

    $(".payHtml").hide("slow", "swing");
    $(".orderDataHtml").show("slow", "swing")
    $(".checkoutHtml").hide("slow", "swing")

}

//設定顯示orderDataHtml END=============================================================================================

//設定感謝訂購 Start=============================================================================================
var OrderFinish = function () {

    window.location.href="../Orders/OrderFinish";

}

//設定感謝訂購 END=============================================================================================

//設定回到購物車 Start=============================================================================================
var LastpageCart = function () {

    window.location.href = "ToCart";

}

//設定回到購物車 END=============================================================================================

//設定初始欄位none Start=============================================================================================
var initErrortext = function () {

    document.getElementById("expiryDateError").style.display = "none";
    document.getElementById("cvvError").style.display = "none";
    document.getElementById("nameError").style.display = "none";   
    document.getElementById("addressError").style.display = "none";
    document.getElementById("phoneError").style.display = "none";
}

//設定初始欄位none END=============================================================================================

//設定金融卡日期顯示錯誤訊息 Start=============================================================================================
var showExpiryDateError = function () {

    document.getElementById("expiryDateError").style.display = "block";
    document.getElementById("cvvError").style.display = "none";
    document.getElementById("nameError").style.display = "none";    
    document.getElementById("addressError").style.display = "none";
    document.getElementById("phoneError").style.display = "none";

}

//設定金融卡日期顯示錯誤訊息 END=============================================================================================

//設定金融卡安全碼顯示錯誤訊息 Start=============================================================================================
var showCvvError = function () {

    document.getElementById("expiryDateError").style.display = "none";
    document.getElementById("cvvError").style.display = "block";
    document.getElementById("nameError").style.display = "none";   
    document.getElementById("addressError").style.display = "none";
    document.getElementById("phoneError").style.display = "none";

}

//設定金融卡安全碼顯示錯誤訊息 END=============================================================================================

//設定姓名顯示錯誤訊息 Start=============================================================================================
var showNameError = function () {

    document.getElementById("expiryDateError").style.display = "none";
    document.getElementById("cvvError").style.display = "none";
    document.getElementById("nameError").style.display = "block";   
    document.getElementById("addressError").style.display = "none";
    document.getElementById("phoneError").style.display = "none";

}

//設定姓名顯示錯誤訊息 END=============================================================================================

//設定信箱顯示錯誤訊息 Start=============================================================================================
var showEmailError = function () {

    document.getElementById("expiryDateError").style.display = "none";
    document.getElementById("cvvError").style.display = "none";
    document.getElementById("nameError").style.display = "none";   
    document.getElementById("addressError").style.display = "none";
    document.getElementById("phoneError").style.display = "none";

}

//設定信箱顯示錯誤訊息 END=============================================================================================

//設定地址顯示錯誤訊息 Start=============================================================================================
var showAddressError = function () {

    document.getElementById("expiryDateError").style.display = "none";
    document.getElementById("cvvError").style.display = "none";
    document.getElementById("nameError").style.display = "none";   
    document.getElementById("addressError").style.display = "block";
    document.getElementById("phoneError").style.display = "none";

}

//設定地址顯示錯誤訊息 END=============================================================================================

//設定電話顯示錯誤訊息 Start=============================================================================================
var showPhoneError = function () {

    document.getElementById("expiryDateError").style.display = "none";
    document.getElementById("cvvError").style.display = "none";
    document.getElementById("nameError").style.display = "none";   
    document.getElementById("addressError").style.display = "none";
    document.getElementById("phoneError").style.display = "block";

}

//設定電話顯示錯誤訊息 END=============================================================================================




