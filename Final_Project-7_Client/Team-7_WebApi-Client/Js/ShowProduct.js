document.addEventListener("DOMContentLoaded", function () {

    

    getData();

  

    var stock = null;

    //加入購物車
    $(document).on("click", ".addtocart", function () {

        console.log("addtocart called")

        var url = "/api/CartApi/AddCartItem" 

         //取得商品Id
        const urlParams = new URLSearchParams(window.location.search);
        const Id = urlParams.get('Id');
        
        //取得商品數量
        var Qty = $(".numOfProduct").val()

        //取得商品尺寸
        var Size = $(".currentbtn").text()

       //若是沒有選擇尺寸或數量則不加入購物車
        if(Size == null || Qty == 0){
            $(".alert").text("請選擇尺寸或數量")
            $(".alert").show("fast")
            return;
        }

        //建立商品資料
        var data = {
            "ProductId": Id,
            "Qty": Qty,
            "Size": Size
        }

        //加入購物車
        fetch(url, {
            method: 'POST',
            headers: new Headers({
                'Content-Type': 'application/json'
            }),
            body: JSON.stringify(data)
        }).then (function (response) {
            if(response.ok){
                alert("加入購物車成功")
            }
            else{
                alert("請先登入會員")
                window.location.href = "../Members/Login"
            }                                                  
        })
       

    })
  
   
    //尺寸選擇
    $(document).on("click", ".sizebtn" ,function () {

        $(".sizebtn").removeClass("currentbtn")

        $(this).addClass("currentbtn")

        stock = reSetStock();

    })




    //數量增加
    $(document).on("click", ".plus", function () {
        
        if(stock == null){
            $(".alert").text("請選擇尺寸")
            $(".alert").show("fast")
            return;
        }
        
        var num = $(".numOfProduct").val()
        num++

        if (num > 0) {
            $(".minus").removeClass("disabled")
        }

        if (num > stock) {
            $(".numOfProduct").val(stock)
            $(".plus").addClass("disabled")
            $(".alert").text("庫存不足")
            $(".alert").show("fast")
        }
        else {
            $(".numOfProduct").val(num)
        }
    })

    //數量減少
    $(document).on("click", ".minus", function () {
        var num = $(".numOfProduct").val()

        num--

        if (num < 0) {
            $(".numOfProduct").val(0)
            $(".minus").addClass("disabled")
        }
        else {
            $(".numOfProduct").val(num)
        }

        if (num < stock) {
            $(".plus").removeClass("disabled")
            $(".alert").hide()
        }
    })



});

var getProductStock = function () {

    var num = $(".currentbtn").attr("num")
    console.log(num)
    return num
}



//get product data  =======> To do : get data from server
var getData = function () {



    //取得網址參數
    const urlParams = new URLSearchParams(window.location.search);
    const Id = urlParams.get('Id');

     //取得商品資料
     let url = '/api/ProductApi/Get?Id=' + Id;

     fetch(url, {
         method: 'GET',
         headers: new Headers({
             'Content-Type': 'application/json'
         })
     })
     .then(function (response) {
         return response.json();  
     })
         .then(function (data) {
             console.log(data);
             setProductDetail(data);
     })
     .catch(function (err) {
         console.log(err);
     })



}

//get product show template
var getTemplate = function (name) {

    var templateName = "template." + name;
    var template = $(templateName).html();

    return $(template).clone();
}

//set   product show
var setProductDetail = function (data) {

    console.log("setProductDetai called")

    var template = getTemplate("product_detail")

    template.find(".name").text(data.Name)
    template.find(".price").text("$$" + data.Price)
    template.find(".Description").text(data.Description)
    template.find(".photo").attr("src", "../../Files/" + data.Image)

    $(".show_detail").append(template);

    $(".M").attr("num", data.M)
    $(".S").attr("num", data.S)
    $(".L").attr("num", data.L)
    $(".XL").attr("num", data.XL)

    $(".alert").hide()
}

//reset product choosing
var reSetStock = function () {

    //reset num of product
    $(".numOfProduct").val(0)

    //reset button status
    $(".plus").removeClass("disabled")
    $(".minus").addClass("disabled")

    //reset alert
    $(".alert").hide()

    //reset stock
    return getProductStock()

}
