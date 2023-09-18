document.addEventListener("DOMContentLoaded", function () {

    //畫面初始化，取得所有商品
    initLoad();

    //商品查詢資料
    var category = null;
    var gender = null;
    var name = null;

    var Criteria = {
        "CategoryId": category,
        "Gender": gender,
        "Name": name
    }



    //當點擊男裝按鈕時，顯示男裝
    $(".ToMen").click(function () {

        //建立商品搜尋參數
        var Criteria = {
            "CategoryId": category,
            "Gender": 1,
            "Name": name
        }

        //搜尋商品
        SearchProduct(Criteria);

        showMen();

        //等0.5秒後，執行categoryLoad(1)
        setTimeout(function () {
            categoryLoad(1);
        }, 300)


    })

    //當點擊女裝按鈕時，顯示女裝
    $(".ToWomen").click(function () {

        //建立商品搜尋參數
        var Criteria = {
            "CategoryId": category,
            "Gender": 0,
            "Name": name
        }

        //搜尋商品
        SearchProduct(Criteria);

        showWomen();

        //等0.3秒後，執行categoryLoad(0)
        setTimeout(function () {
            categoryLoad(0);
        }, 300)

    })

    //當點擊首頁按鈕時，顯示首頁
    $(".ToHome").click(function () {
        initLoad();
        showHome();
    })



});


//註冊按鈕
var registerButton = function () {
    //分類選擇
    $(".category").click(function () {

        $(".category").removeClass("currentbtn")

        var currentbtn = $(this);

        currentbtn.addClass("currentbtn")

        var Criteria = {
            "CategoryId": currentbtn.attr("categoryId"),
            "Gender": currentbtn.attr("gender"),
            "Name": null
        }

        SearchProduct(Criteria);

        console.log(Criteria)
    })

}


//get product show template
var getTemplate = function (name) {

    var templateName = "template." + name;
    var template = $(templateName).html();

    return $(template).clone();
}


//get product data ======> To do : get data from server
var getAllProduct = function () {

    var products = [
        { name: "高腰牛仔短褲", price: 865, src: "https://tw.906studio.kr/web/product/big/202106/48428bb00743bc5ed42b869240bf7048.jpg", cart: false, follow: false },
        { name: "綁帶A字長裙", price: 840, src: "https://tw.906studio.kr/web/product/big/202105/fc9aee31a0c05b6ef536216c8566cd69.jpg", cart: false, follow: false },
        { name: "短版針織外套", price: 599, src: "https://tw.906studio.kr/web/product/big/202106/1cd6fbcde8a5b8df1984c992ef86e73b.jpg", cart: false, follow: false },
        { name: "風衣長洋裝", price: 2205, src: "https://tw.906studio.kr/web/product/big/202105/6c5e6c93b6342932d639143da1fb1652.jpg", cart: false, follow: false },
    ];

    return products;
}


//取得分類 =======> To do : get data from server
var getCategory = function (Gender) {

    if (Gender === 1) {

        return category = [
            { name: "長袖", id: "1" },
            { name: "短袖", id: "2" },
            { name: "長褲", id: "3" },
            { name: "短褲", id: "4" },
        ]
    }
    else {
        return category = [
            { name: "長袖", id: "1" },
            { name: "短袖", id: "2" },
            { name: "長褲", id: "3" },
            { name: "短褲", id: "4" },
            { name: "裙子", id: "5" }
        ]
    }
}

//搜尋商品 =======> To do : get data from server
var SearchProduct = function (Criteria) {
    //todo
    var data = getAllProduct();

    setProduct(data);
}

//建立商品
var setProduct = function (data) {


    var template = getTemplate("product_list");

    $(".product_show_list").empty();

    $.each(data, function (key, ele) {
        var item = template.clone();
        item.find(".product").attr("href", "ShowProduct.html?data=" + ele.name);
        item.find(".photo").attr("src", ele.src);
        item.find(".name").text(ele.name);
        item.find(".price").text("$" + ele.price);
        $(".product_show_list").append(item);
    });
}

//建立分類
var setCategory = function (data, Gender) {

    var template = getTemplate("Category_button");

    $(".categories").empty();

    var item = template.clone();
    item.text("全部");
    item.attr("gender", null);
    item.attr("categoryId", null);
    item.addClass("currentbtn")
    $(".categories").append(item);

    $.each(data, function (key, ele) {
        var item = template.clone();


        item.text(ele.name);
        item.attr("gender", Gender);
        item.attr("categoryId", ele.id);

        $(".categories").append(item);
    })

    //註冊按鈕
    registerButton();
}

//畫面初始化
var initLoad = function () {

    var data = getAllProduct();
    setProduct(data);

    $(".men").hide("slow", "swing");
    $(".women").hide("slow", "swing");
    $(".home").hide();
    $(".home").show("slow", "swing");

}

//分類初始化
var categoryLoad = function (Gender) {

    var data = getCategory(Gender);

    setCategory(data, Gender);

}

//顯示女裝
var showWomen = function () {

    $(".home").hide("slow", "swing");
    $(".men").hide("slow", "swing")
    $(".women").show("slow", "swing")




}

//顯示男裝
var showMen = function () {

    $(".home").hide("slow", "swing");
    $(".women").hide("slow", "swing");
    $(".men").show("slow", "swing")
}

//顯示首頁
var showHome = function () {

    $(".women").hide("slow", "swing");
    $(".men").hide("slow", "swing")
    $(".home").show("slow", "swing")

}
