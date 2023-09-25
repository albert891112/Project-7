document.addEventListener("DOMContentLoaded", function () {

    //設定初始payHtml
    var initLoadPay = function () {

        showPay();



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
                // cartItems(result);
                console.log(result);
            }).catch(function (err) {
                console.log(err);
            });

        };

        var cartItems = function (data) {
            var cartTemplate = getCartTemplate("cartItem_list");

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

    }

    // getToCartItem();

    //計算總金額
    var subtotalLabel = $(".subtotal");
    var shippingcostLabel = $(".shippingcost");
    var couponcostLabel = $(".couponcost");
    var totalAmountLabel = $(".totalAmount");

    // 下拉列表的更改事件
    $("#paymentMethodSelect, #shippingMethodSelect, #coupon").change(function () {
        // 獲取所選選項的值
        var subtotal = $("#paymentMethodSelect").val();
        var shippingMethodValue = $("#shippingMethodSelect").val();
        var couponValue = $("#coupon").val();

        if (shippingMethodValue === "送到府") {
            shippingMethodValue = 150;
        }
        if (shippingMethodValue === "超商取貨") {
            shippingMethodValue = 60;
        }
        //如果couponcostValue為空值，則設為0
        if (isNaN(couponValue)) {
            couponValue = 0;
        }
        if (isNaN(subtotal)) {
            subtotal = 0;
        }
        if (isNaN(shippingMethodValue)) {
            shippingMethodValue = 0;
        }
        //如果總金額為NAN，則設為0
        if (isNaN(totalAmountLabel.text())) {
            totalAmountLabel.text(0);
        }

        // 進行相應的加法操作
        var subtotalValue = parseFloat(subtotal);
        var shippingcostValue = parseFloat(shippingMethodValue);
        var couponcostValue = parseFloat(couponValue);
        var totalAmount = subtotalValue + shippingcostValue - couponcostValue;

        //如果totalAmount為小於0，則設為0
        if (totalAmount < 0) {
            totalAmount = 0;
            alert("總金額小於0");
        }


        // 更新標籤的文本
        subtotalLabel.text(subtotalValue);
        shippingcostLabel.text(shippingcostValue);
        couponcostLabel.text(couponcostValue);
        totalAmountLabel.text(totalAmount);


    });


    //顯示初始payHtml
    var showPay = function () {

        $(".payHtml").show();
        $(".orderDataHtml").hide()
        $(".checkoutHtml").hide()
    }

    //畫面初始化，取得所有商品
    initLoadPay();

    //paymentmethod======================================================
    // 做出選擇後，顯示注意事項
    var paymentMethodSelect = document.getElementById("paymentMethodSelect");
    var shippingMethodSelect = document.getElementById("shippingMethodSelect");

    //付款方式顯示注意事項
    paymentMethodSelect.addEventListener("change", function () {
        if (paymentMethodSelect.value == "信用卡") {
            document.getElementById("carddesc").style.display = "block";
        } else {
            document.getElementById("carddesc").style.display = "none";
        }
    });

    //運送方式顯示注意事項
    $("#shippingMethodSelect").change(function () {

        if (shippingMethodSelect.value == "送到府") {

            document.getElementById("shippinghomedesc").style.display = "block";
            document.getElementById("pillAddressTable").style.display = "block";
            document.getElementById("shippingsuperstoredesc").style.display = "none";

        } else if (shippingMethodSelect.value == "超商取貨") {
            document.getElementById("shippingsuperstoredesc").style.display = "block";
            document.getElementById("shippinghomedesc").style.display = "none";

        } else {
            document.getElementById("shippingsuperstoredesc").style.display = "none";
            document.getElementById("shippinghomedesc").style.display = "none";
            document.getElementById("pillAddressTable").style.display = "none";

        }


    });



    document.getElementById("btnNextOrderData").addEventListener("click", function () {

        //將付款方式選擇的值存入label
        var paymentMethodselectedOption = paymentMethodSelect.options[paymentMethodSelect.selectedIndex].value;
        var paymentMethodSelectresultLabel = document.getElementById("paymentMethodValue");

        paymentMethodSelectresultLabel.textContent = paymentMethodselectedOption;

        //將運送方式選擇的值存入label
        var shippingMethodselectedOption = shippingMethodSelect.options[shippingMethodSelect.selectedIndex].value;
        var shippingMethodSelectresultLabel = document.getElementById("shippingMethodValue");

        shippingMethodSelectresultLabel.textContent = shippingMethodselectedOption;


        //如選選擇信用卡 顯示填寫信用卡資料
        if (paymentMethodSelect.value == "信用卡") {
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


    $("#btnLastCart").click(function () {

        LastpageCart();

        initLoad();

    });



    // payment end====================================================

    //orderData  begin======================================================
    document.getElementById("btnNextCheckout").addEventListener("click", function () {


        var paymentMethodselectedOption = paymentMethodSelect.options[paymentMethodSelect.selectedIndex].value;
        var paymentMethodSelectresultLabel = document.getElementById("paymentMethodValue1");

        paymentMethodSelectresultLabel.textContent = paymentMethodselectedOption;
        //如選選擇信用卡 顯示填寫信用卡資料
        if (paymentMethodSelect.value == "信用卡") {
            document.getElementById("creditCardFields").style.display = "block";
            document.getElementById("cvvField").style.display = "block";
        } else {
            document.getElementById("creditCardFields").style.display = "none";
            document.getElementById("cvvField").style.display = "none";
        }

        //將姓名input的值存入label
        var inputName = document.getElementById("inputName").value;
        var myselfLabelNameValue = document.getElementById("myselfLabelNameValue");
        myselfLabelNameValue.textContent = inputName;

        //將信箱input的值存入label
        var inputEmail = document.getElementById("inputEmail").value;
        var myselfLabelEmailValue = document.getElementById("myselfLabelEmailValue");
        myselfLabelEmailValue.textContent = inputEmail;

        //將地址textarea的值存入label
        var inputAddress = document.getElementById("inputAddress").value;
        var myselfLabelAddressValue = document.getElementById("myselfLabelAddressValue");
        myselfLabelAddressValue.textContent = inputAddress;

        //將電話input的值存入label
        var inputPhone = document.getElementById("inputPhone").value;
        var myselfLabelPhoneValue = document.getElementById("myselfLabelPhoneValue");
        myselfLabelPhoneValue.textContent = inputPhone;

        //驗證姓名、信箱、地址、電話是否為空值    
        var nameError = document.getElementById("nameError");
        var emailError = document.getElementById("emailError");
        var addressError = document.getElementById("addressError");
        var phoneError = document.getElementById("phoneError");
        var parts = expiryDate.value.split('/');
        var month = parseInt(parts[0], 10);


        if (expiryDate.value === "" && paymentMethodSelect.value === "信用卡") {

            //如果信用卡日期為空值,顯示錯誤訊息

            showExpiryDateError();

            expiryDateError.textContent = "請輸入有效期限";

            return;
        }
        //如果信用卡日期不是MM/YY格式,顯示錯誤訊息
        else if (!/^\d{2}\/\d{2}$/.test(expiryDate.value) && paymentMethodSelect.value === "信用卡") {

            showExpiryDateError();

            expiryDateError.textContent = "請輸入格式為MM/YY";
            return;

        } else if (month < 1 || month > 12 && paymentMethodSelect.value === "信用卡") {
            //如果信用卡日期不是1~12月,顯示錯誤訊息

            showExpiryDateError();

            expiryDateError.textContent = "月份必須在1到12之間";
            return;
        } else if (cvv.value === "" && paymentMethodSelect.value === "信用卡") {
            //如果安全碼為空值,顯示錯誤訊息

            showCvvError();

            cvvError.textContent = "請輸入安全碼";
            return;
        }
        else if (cvv.value.length !== 3 && paymentMethodSelect.value === "信用卡") {
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
        } else if (inputEmail === "") {
            //如果信箱為空值,顯示錯誤訊息

            showEmailError();

            emailError.textContent = "請輸入信箱";
            return;
        } else if (! /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i.test(inputEmail)) {
            //如果信箱不符合格式,顯示錯誤訊息
            showEmailError();
            emailError.textContent = "請輸入對的信箱格式";
            return;
        }

        else if (inputAddress === "" && shippingMethodSelect.value === "送到府") {
            //如果地址為空值,顯示錯誤訊息

            showAddressError();

            addressError.textContent = "請輸入地址";
            return;
        } else if (inputPhone === "") {
            //如果電話為空值,顯示錯誤訊息

            showPhoneError();

            phoneError.textContent = "請輸入手機號碼";
            return;
        } else if (!/^\d+$/.test(inputPhone)) {

            showPhoneError();

            phoneError.textContent = "手機號碼只能是數字";
            return;
        }

        initErrortext();

        showCheckout();

        //將運送方式選擇的值存入label
        var shippingMethodselectedOption = shippingMethodSelect.options[shippingMethodSelect.selectedIndex].value;
        var shippingMethodSelectresultLabel = document.getElementById("shippingMethodValue1");

        shippingMethodSelectresultLabel.textContent = shippingMethodselectedOption;

    });

    //顯示上一頁payHtml
    $("#btnLastPay").click(function () {

        showPay();

    });



    //orderData  end======================================================


    //checkout  begin======================================================

    $("#btnNextOrderFinish").click(function () {

        OrderFinish();
        var postOrderData = function () {

            let url = '/api/CartApi/Post';

            fetch(url, {
                method: 'Post',
                headers: new Headers({
                    'Content-Type': 'application/json'
                })
            }).then(function (response) {
                return response.json();
            }).then(function (result) {
                setPostOrderData(result);
            }).catch(function (err) {
                console.log(err);
            });

        }

        var setPostOrderData = function (data) {

            var orderDataTemplate = getOrderDataTemplate("orderData_list");

            $.each(data, function (index, ele) {

                var orderData = orderDataTemplate.clone();
                orderData.find(".inputName").text(ele.Name);
                orderData.find(".inputEmail").text(ele.Email);
                orderData.find(".inputAddress").text(ele.Address);
                orderData.find(".inputPhone").text(ele.Phone);
                //    orderData.find(".expiryDate").text(ele.Size);
                //     orderData.find(".cvv").text(ele.UnitPrice);

                // $("#orderDataTable").append(orderData);
            });
        }

    });

    $("#btnLastOrderData").click(function () {

        showOrderData();

    });


    //coupon begin======================================================

    //coupon end======================================================   




});

//設定函數庫=============================================================================================
//設定顯示checkoutHtml
var showCheckout = function () {

    $(".payHtml").hide("slow", "swing");
    $(".orderDataHtml").hide("slow", "swing")
    $(".checkoutHtml").show("slow", "swing")

}


//設定顯示orderDataHtml
var showOrderData = function () {

    $(".payHtml").hide("slow", "swing");
    $(".orderDataHtml").show("slow", "swing")
    $(".checkoutHtml").hide("slow", "swing")

}



//設定感謝訂購
var OrderFinish = function () {

    window.location.href = "OrderFinish.html";

}

var LastpageCart = function () {

    window.location.href = "Cart.html";

}

var initErrortext = function () {

    document.getElementById("expiryDateError").style.display = "none";
    document.getElementById("cvvError").style.display = "none";
    document.getElementById("nameError").style.display = "none";
    document.getElementById("emailError").style.display = "none";
    document.getElementById("addressError").style.display = "none";
    document.getElementById("phoneError").style.display = "none";
}

//設定信用卡日期顯示錯誤訊息
var showExpiryDateError = function () {

    document.getElementById("expiryDateError").style.display = "block";
    document.getElementById("cvvError").style.display = "none";
    document.getElementById("nameError").style.display = "none";
    document.getElementById("emailError").style.display = "none";
    document.getElementById("addressError").style.display = "none";
    document.getElementById("phoneError").style.display = "none";

}

//設定信用卡安全碼顯示錯誤訊息
var showCvvError = function () {

    document.getElementById("expiryDateError").style.display = "none";
    document.getElementById("cvvError").style.display = "block";
    document.getElementById("nameError").style.display = "none";
    document.getElementById("emailError").style.display = "none";
    document.getElementById("addressError").style.display = "none";
    document.getElementById("phoneError").style.display = "none";

}

//設定姓名顯示錯誤訊息
var showNameError = function () {

    document.getElementById("expiryDateError").style.display = "none";
    document.getElementById("cvvError").style.display = "none";
    document.getElementById("nameError").style.display = "block";
    document.getElementById("emailError").style.display = "none";
    document.getElementById("addressError").style.display = "none";
    document.getElementById("phoneError").style.display = "none";

}

//設定信箱顯示錯誤訊息
var showEmailError = function () {

    document.getElementById("expiryDateError").style.display = "none";
    document.getElementById("cvvError").style.display = "none";
    document.getElementById("nameError").style.display = "none";
    document.getElementById("emailError").style.display = "block";
    document.getElementById("addressError").style.display = "none";
    document.getElementById("phoneError").style.display = "none";

}

//設定地址顯示錯誤訊息
var showAddressError = function () {

    document.getElementById("expiryDateError").style.display = "none";
    document.getElementById("cvvError").style.display = "none";
    document.getElementById("nameError").style.display = "none";
    document.getElementById("emailError").style.display = "none";
    document.getElementById("addressError").style.display = "block";
    document.getElementById("phoneError").style.display = "none";

}

//設定電話顯示錯誤訊息
var showPhoneError = function () {

    document.getElementById("expiryDateError").style.display = "none";
    document.getElementById("cvvError").style.display = "none";
    document.getElementById("nameError").style.display = "none";
    document.getElementById("emailError").style.display = "none";
    document.getElementById("addressError").style.display = "none";
    document.getElementById("phoneError").style.display = "block";

}




