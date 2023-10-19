document.addEventListener("DOMContentLoaded", function () {

    //畫面設置
    setProducts();
    setEditCategorySelect();
    

    //設置編輯商品驗證
    EditProductValidation();
    CreateProductValidation();

    var LowPrice = document.querySelector(".SearchProductLowPrice")

    LowPrice.addEventListener("input", function () {
        LowPrice.value = LowPrice.value.replace(/[^\d]/g, '')
    })

    var HightPrice = document.querySelector(".SearchProductHightPrice")

    HightPrice.addEventListener("input", function () {
        HightPrice.value = HightPrice.value.replace(/[^\d]/g, '')
    })


   

    $(document).on("click", ".ToEdit", function () {

        var ProductId = $(this).attr("ProductId");
        setEditProduct(ProductId);
        
    })

    //關閉按鈕重置
    $(document).on("click", ".EditProductClose", function () {

        var inputTag = [".EditProductName", ".EditProductDes", ".EditProductPrice", ".EditProductS", ".EditProductM", ".EditProductL", ".EditProductXL"];

        var selectTag = ["#EditCategorySelect", "#EditGenderSelect", "#statusSelect"]


        initModelInput(inputTag, selectTag);

        $(".EditProduct").attr("disabled", false);
    })
    $(document).on("click", ".CreateProductClose", function () {

        var inputTag = [".CreateProductName", ".CreateProductDes", ".CreateProductPrice", ".CreateProductS", ".CreateProductM", ".CreateProductL", ".CreateProductXL"];

        var selectTag = ["#CreateCategorySelect", "#CreateGenderSelect", "#CreateStatusSelect"]

        initModelInput(inputTag, selectTag);
    })

    //送出資料
    $(document).on("click", ".EditProduct", function () {
        var data = new FormData();

        var ProductId = $(this).attr("ProductId");
        var ProductName = $(".EditProductName").val();
        var ProductDes = $(".EditProductDes").val();
        var ProductPrice = $(".EditProductPrice").val();
        var ProductS = $(".EditProductS").val();
        var ProductM = $(".EditProductM").val();
        var ProductL = $(".EditProductL").val();
        var ProductXL = $(".EditProductXL").val();
        var CategoryId = $("#EditCategorySelect").val();
        var GenderId = $("#EditGenderSelect").val();
        var Status = $("#statusSelect").val();
        var ProductPhoto = null;

        if ($(".EditProductPhoto").val() != "") {
            ProductPhoto = document.querySelector(".EditProductPhoto").files[0];
        }

        data.append("Id", ProductId);
        data.append("Name", ProductName);
        data.append("Description", ProductDes);
        data.append("Price", ProductPrice);
        data.append("S", ProductS);
        data.append("M", ProductM);
        data.append("L", ProductL);
        data.append("XL", ProductXL);
        data.append("file", ProductPhoto);
        data.append("CategoryId", CategoryId);
        data.append("GenderId", GenderId);
        data.append("Enable", Status);

        console.log(ProductId);


        saveChange(data);

    })
    $(document).on("click", ".CreateProduct", function () {

        console.log("CreateProduct");
        var data = new FormData();

        var ProductName = $(".CreateProductName").val();
        var ProductDes = $(".CreateProductDes").val();
        var ProductPrice = $(".CreateProductPrice").val();
        var ProductS = $(".CreateProductS").val();
        var ProductM = $(".CreateProductM").val();
        var ProductL = $(".CreateProductL").val();
        var ProductXL = $(".CreateProductXL").val();
        var CategoryId = $("#CreateCategorySelect").val();
        var GenderId = $("#CreateGenderSelect").val();
        var Status = $("#CreateStatusSelect").val();
        var ProductPhoto = document.querySelector(".CreateProductPhoto").files[0];

        data.append("Name", ProductName);
        data.append("Description", ProductDes);
        data.append("Price", ProductPrice);
        data.append("S", ProductS);
        data.append("M", ProductM);
        data.append("L", ProductL);
        data.append("XL", ProductXL);
        data.append("file", ProductPhoto);
        data.append("CategoryId", CategoryId);
        data.append("GenderId", GenderId);
        data.append("Enable", Status);

        saveCreate(data);
    })

    //性別分類連動選單
    var genderSelect = ["#Edit", "#Create", "#Search"];
    genderSelect.forEach(select => {

        $(document).on("change", select + "GenderSelect", function () {

            var genderId = $(this).val();
            
            setCategorySelect(genderId, select);

            
        })
    })

    //搜尋商品
    $(document).on("click", ".SearchProduct", function () {

        var ProductName = $(".SearchProductName").val();
        var GenderId = $("#SearchGenderSelect").val();
        var CategoryId = $("#SearchCategorySelect").val();
        var Status = $("#SearchStatus").val();
        var LowPrice = $(".SearchProductLowPrice").val();
        var HightPrice = $(".SearchProductHightPrice").val();

        var Criteria = {
            "Name": ProductName,
            "GenderId": GenderId,
            "CategoryId": CategoryId,
            "Enable": Status,
            "LowPrice": LowPrice,
            "HightPrice": HightPrice
        }

        SearchProduct(Criteria);


    })
})

//設置商品資料 
var setProducts = function () {

    let url = "/api/ProductApi/GetAll";

    fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(function (response) {
        return response.json();
    }).then(function (date) {
        setProductList(date);
    })
}


var setProductList = function (data) {

    var template = getTemplate("ProductTemplate");

    var item = $(template).clone();

    var ProductList = $(".ProductList");

    ProductList.empty();

    data.forEach(element => {

        item.find(".ToEdit").attr("ProductId", element.Id);

        item.find(".photo").attr("src", "../../File/" + element.Image);
        item.find(".price").text(element.Price);
        item.find(".description").text(element.Description);
        item.find(".name").text(element.Name);
        item.find(".category").text(element.Category.Name);
        if(element.Gender.Gender == 1){
            item.find(".gender").text("男裝");
        }
        else{
            item.find(".gender").text("女裝");
        }
        item.find(".s").text(element.Stock.S);
        item.find(".m").text(element.Stock.M);
        item.find(".l").text(element.Stock.L);
        item.find(".xl").text(element.Stock.XL);

        ProductList.append(item.html());
    });

}

//取得模板
var getTemplate = function (name) {

    var templateName = "template." + name;

    var template = $(templateName).html();

    return template;
}

//設置商品編輯資料 
var setEditProduct = function (id) {


   let url = "/api/ProductApi/Get?Id=" + id;

    fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(function (response) {
        return response.json();
    }).then(function (date) {
        setEditProductData(date);
    })

}

//設置編輯商品資料
var setEditProductData = function (data) {

    $(".EditProductName").val(data.Name);
    $(".EditProductPrice").val(data.Price);
    $(".EditProduct").attr("ProductId", data.Id);
    $(".EditProductDes").val(data.Description);
    $(".EditProductS").val(data.Stock.S);
    $(".EditProductM").val(data.Stock.M);
    $(".EditProductL").val(data.Stock.L);
    $(".EditProductXL").val(data.Stock.XL);


    //尋找value == data.Category.Id 的option
    $("#EditCategorySelect option").each(function () {

        $(this).removeAttr("selected");

        if ($(this).val() == data.Category.Id) {
            $(this).attr("selected", "true");
        }
    })

    //尋找value == date.Gender.Id 的option
    $("#EditGenderSelect option").each(function () {

        $(this).removeAttr("selected");

        if ($(this).val() == data.Gender.Id) {
            $(this).attr("selected", "true");
        }
    })

   

    

    //尋找value == date.Enable 的option
    $("#statusSelect option").each(function () {

        $(this).removeAttr("selected");

        if ($(this).val() == data.Enable) {
            $(this).attr("selected", "true");
        }
    })



}


//設置商品分類資料 
var setCategorySelect = function (GenderId, select) {

    if(GenderId == ""){

        var select = $(select + "CategorySelect");

        select.empty();

        select.append("<option value='' selected>請選擇分類</option>");

        return;
    }
    //GetCategoryByGender
    let url = "/api/CategoryApi/GetByGender?GenderId=" + GenderId;

    fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(function (response) {
        return response.json();
    }).then(function (date) {
        setOption(date, select);
    })


}

var setEditCategorySelect = function (){

    let url = "/api/CategoryApi/GetAll";

    fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(function (response) {
        return response.json();
    }).then(function (date) {
        setOption(date, "#Edit");
    })
}

//setSelector
var setOption = function (data,  SelectName) {
    
    var select = $(SelectName + "CategorySelect");  

    select.empty();

    select.append("<option value='' selected>請選擇分類</option>");

    data.forEach(element => {

        var option = $("<option></option>");

        option.attr("value", element.Id);
        option.text(element.Name);

        select.append(option);
    });
}

//編輯商品驗證
var EditProductValidation = function () {

    var textTag = [".EditProductName", ".EditProductDes"];

    var numberTag = [".EditProductPrice", ".EditProductS", ".EditProductM", ".EditProductL", ".EditProductXL"]

    var selectTag = ["#EditCategorySelect", "#EditGenderSelect", "#statusSelect"]

    textBoxValidation(textTag, numberTag, selectTag, "EditProduct");
    selectValidation(textTag, numberTag, selectTag, "EditProduct");

}

//新增商品驗證
var CreateProductValidation = function () {

    var textTag = [".CreateProductName", ".CreateProductDes", ".CreateProductPhoto"];

    var numberTag = [".CreateProductPrice", ".CreateProductS", ".CreateProductM", ".CreateProductL", ".CreateProductXL"]

    var selectTag = ["#CreateCategorySelect", "#CreateGenderSelect", "#CreateStatusSelect"]

    textBoxValidation(textTag, numberTag, selectTag, "CreateProduct");
    selectValidation(textTag, numberTag, selectTag, "CreateProduct");

}

//文字框驗證
var textBoxValidation = function (textTag, numberTag, selectTag, submmit) {

    //文字框驗證
    textTag.forEach(tag => {

        var input = document.querySelector(tag);

        input.addEventListener("input", function () {

            if (input.checkValidity()) {
                input.classList.remove("invalid");
                input.classList.add("valid");
            } else {
                input.classList.remove("valid");
                input.classList.add("invalid");
            }

            checkSubmit(submmit, textTag, numberTag, selectTag);
        })
    })

    //數字框驗證
    numberTag.forEach(tag => {

        var input = document.querySelector(tag);

        input.addEventListener("input", function () {

            input.value = input.value.replace(/[^\d]/g, '')

            if (input.checkValidity()) {
                input.classList.remove("invalid");
                input.classList.add("valid");
            } else {
                input.classList.remove("valid");
                input.classList.add("invalid");
            }

            checkSubmit(submmit, textTag, numberTag, selectTag);
        })
    })

}

//下拉選單驗證
var selectValidation = function (textTag, numberTag, selectTag, submmit) {

    selectTag.forEach(tag => {

        var select = document.querySelector(tag);

        select.addEventListener("change", function () {

            if (select.value != "") {
                select.classList.remove("invalid");
                select.classList.add("valid");
            } else {
                select.classList.remove("valid");
                select.classList.add("invalid");
            }

            checkSubmit(submmit, textTag, numberTag, selectTag);
        })
    })

}

//檢查是否可以送出
var checkSubmit = function (submmit, textTag, numberTag, selectTag) {

    var check = true;

    //檢查文字框
    textTag.forEach(tag => {

        var input = document.querySelector(tag);

        if (!input.checkValidity()) {
            check = false;
        }
    })

    //檢查數字框
    numberTag.forEach(tag => {

        var input = document.querySelector(tag);

        if (!input.checkValidity()) {
            check = false;
        }
    })

    //檢查下拉選單
    selectTag.forEach(tag => {

        var select = document.querySelector(tag);

        if (select.value == "") {
            check = false;
        }
    })

    if (check) {
        $("." + submmit).removeAttr("disabled");
    } else {
        $("." + submmit).attr("disabled", "true");
    }
}

//清除Model資料
var initModelInput = function (inputTag, selectTag) {

    inputTag.forEach(tag => {

        var input = document.querySelector(tag);

        input.value = "";
        input.classList.remove("valid");
        input.classList.remove("invalid");
    })

    selectTag.forEach(tag => {

        var select = document.querySelector(tag);

        $(tag + " option").each(function () {
            $(this).removeAttr("selected");
            if ($(this).val() == "") {
                $(this).attr("selected", "true");
            }
        })

        select.classList.remove("valid");
        select.classList.remove("invalid");
    })

    setEditCategorySelect();


}


//送出資料
//=================================================================================
var saveChange = function (data) {
    
    let url = "/api/ProductApi/Update";

    fetch(url, {
        method: "PUT",
        body: data
    }).then(function (result) {
        setProducts();
    })
}


var saveCreate = function (data) {

    let url = "/api/ProductApi/Create";

    fetch(url, {
        method: "POST",
        body: data
    })
    .then(function (response) {
        if(response.ok == false){
            alert("新增失敗");
        }
    })
    .then(function (result) {
        setProducts();
    })
}



var SearchProduct = function (data) {

    console.log(data);  
    
        let url = "/api/ProductApi/Search";
    
        fetch(url, {
            method: "Post",
            body: JSON.stringify(data),
            headers: {
                "Content-Type": "application/json"
            }
        }).then(function (result) {
            return result.json();
        }).then(function (date) {
            setProductList(date);
        })
}