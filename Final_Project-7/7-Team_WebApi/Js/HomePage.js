document.addEventListener("DOMContentLoaded", function () {


    setProductRank();
    setOrderData()
    getUserData();




    $(document).on("click", ".checkDetail", function () {
        var orderId = $(this).attr("orderId");
        console.log(orderId);
        getOrderDetail(orderId);
    })


    $(document).on("click", ".Send", function () {
        var orderId = $(this).attr("orderId");
        console.log(orderId);
        sendOrder(orderId);
    })
})

//設定商品排行 
var setProductRank = function () {

    let url = "/api/ProductApi/GetSalesRank";

    fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(function (response) {
        return response.json();
    }).then(function (data) {
        console.log(data);
        setProductList(data);
    })

}

var setProductList = function (data) {

    var productTemplate = getTemplate("RankTemplate");

    var rankList = $(".RankList");

    var count = 1;


    data.forEach(element => {
        var item = productTemplate.clone();

        item.find(".rank").text(count);
        item.find(".Total").text(element.TotalQty);
        item.find(".Name").text(element.Name);
        item.find(".Price").text(element.Price);
        item.find(".S").text(element.S);
        item.find(".M").text(element.M);
        item.find(".L").text(element.L);
        item.find(".XL").text(element.XL);

        count += 1;


        rankList.append(item);
    });




}

//設定訂單資料 
var setOrderData = function () {

    let url = "/api/OrderApi/GetByStatus?StatusId=2";

    fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(function (response) {
        return response.json();
    }).then(function (data) {
        console.log(data);
        setOrderList(data).then(function () {
            $('table').scrollTableBody({
                rowsToDisplay: 5,
                fixedColumns: 1
            });
        });
    })

   

}

var setOrderList = function (data) {

    var orderTemplate = getTemplate("OrderTemplate");

    var orderList = $(".OrderList");

    orderList.empty();

    data.forEach(element => {
        var item = orderTemplate.clone();

        item.find(".Status").text(element.OrderStatus.Status);
        item.find(".Name").text(element.Member.FirstName);
        item.find(".Address").text(element.Address);
        item.find(".Ship").text(element.Shipping.ShippingMethod);
        item.find(".TotalPrice").text(element.Total);
        item.find(".OrderTime").text(element.OrderTime);
        item.find(".checkDetail").attr("orderId", element.Id);
        item.find(".Send").attr("orderId", element.Id);

        orderList.append(item);
    })

    return new Promise(function (resolve, reject) {
        resolve();
    })
}

//設定訂單明細 
var getOrderDetail = function (OrderId) {
   
    let url = "/api/OrderApi/GetOrderItem?OrderId=" + OrderId;

    fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(function (response) {
        return response.json();
    }).then(function (data) {
        setOrderDetail(data);
    })

}

var setOrderDetail = function (data) {

    var orderItemTemplate = getTemplate("OrderItemTemplate");

    var orderItemList = $(".OrderItemList");

    data.forEach(element => {
        var item = orderItemTemplate.clone();

        item.find(".Id").text(element.Product.Id);
        item.find(".Name").text(element.ProductName);
        item.find(".Size").text(element.Size);
        item.find(".Qty").text(element.Qty);

        orderItemList.append(item);
    })
}

//出貨確認 ====================>串接API
var sendOrder = function (OrderId) {
    
    var Criteria = {
        "Id": OrderId,
        "StatusId": 3
    }

    let url = "/api/OrderApi/UpdateStatus";

    fetch(url, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(Criteria)
    }).then(function (data) {
        alert("已出貨");
        window.location.reload();
    })

}

//設定使用者資料 
var getUserData = function () {

    let url = "/api/UserApi/GetUserRoles";

    fetch(url , {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(function (response) {
        return response.json();
    }).then(function (data) {
        console.log(data);
        setUserData(data);
    })
}

var setUserData = function (data) {

    var userData = $(".User");

    userData.find(".Account").text(data.Account);
    userData.find(".Role").text(data.Role);
    userData.find(".loginTime").text(data.LoginTime);
}

//get product show template
var getTemplate = function (name) {

    var templateName = "template." + name;
    var template = $(templateName).html();

    return $(template).clone();
}



