document.addEventListener("DOMContentLoaded", function(){

    //畫面初始化，取得所有商品
    initLoad();
  
  
    //當點擊男裝按鈕時，顯示男裝
    $(".ToMen").click(function(){
  
      //建立商品搜尋參數
      var Criteria = {
        "CategoryId" : null,
        "Gender" : 1,
        "Name" : null
      }
        
  
        showMen();

        //搜尋商品
        setTimeout(function () {
            SearchProduct(Criteria);
        }, 300)
  
      //等0.5秒後，執行categoryLoad(1)
      setTimeout(function(){
        categoryLoad(1);
      },300)
  
     
    })
  
    //當點擊女裝按鈕時，顯示女裝
    $(".ToWomen").click(function(){
  
       //建立商品搜尋參數
       var Criteria = {
        "CategoryId" : null,
        "Gender" : 0,
        "Name" : null
      }
      
  
        showWomen();

        //搜尋商品
        setTimeout(function () {
            SearchProduct(Criteria);
        }, 500)
  
      //等0.3秒後，執行categoryLoad(0)
      setTimeout(function(){
        categoryLoad(0);
      },300)
  
    })
  
    //當點擊首頁按鈕時，顯示首頁
    $(".ToHome").click(function(){
      initLoad();
    })
  
      
  
  });
  
  
  //註冊按鈕
  var registerButton = function(){
      //分類選擇
    $(".category").click(function(){
  
      $(".category").removeClass("currentbtn")
  
      var currentbtn = $(this);
  
      currentbtn.addClass("currentbtn")
  
      var Criteria = {
        "CategoryId" : currentbtn.attr("categoryId"),
        "Gender" : currentbtn.attr("gender"),
        "Name" : null
      }
  
      setSearchResult(Criteria);

    })
    
  }
  

  var setSearchResult = function(Criteria){
    
    $(".product_show_list").hide("slow" , "swing");

    setTimeout(function(){
      SearchProduct(Criteria);
    },300)

    $(".product_show_list").show("slow" , "swing");

  }
  
  //get product show template
  var getTemplate = function(name){
  
      var templateName = "template." +  name ;
      var template = $(templateName).html();
  
      return $(template).clone();
  }
  
  
  //get product data ======> To do : get data from server
  var getAllProduct = function(){
    
    const bestNumOfProduct = 4;

    let url = '/api/ProductApi/GetSalesRank?numOfPrduct=' + bestNumOfProduct;

    fetch(url, {
        method: 'GET',
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    }).then(function (response) {
      return response.json();
    }).then(function (result) {
      setProduct(result).then(function(){
        showHome();
      });
     
    }).catch(function (err) {
      console.log(err);
    });

    return new Promise((resolve, reject) => {
      resolve();
    });
    
  }
  
  
  //取得分類 =======> To do : get data from server
  var getCategory = function(Gender){
  
      let url = '/api/CategoryApi/Get?Gender=' + Gender;

      fetch(url, {
          method: 'GET',
          headers: new Headers({
              'Content-Type': 'application/json'
          })
        })
        .then(function (response) {
            return response.json();
        })
        .then(function (result) {
          setCategory(result , Gender);
        })
        .catch(function (err) {
          console.log(err);
        })
      
    }
  
  
  //搜尋商品 =======> To do : get data from server
  var SearchProduct = function(Criteria){
    
    let url = '/api/ProductApi/Search';
    

    fetch(url, {
        method: 'POST',
        body: JSON.stringify(Criteria),
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    }).then(function (response) {
      return response.json();
    }).then(function (result) {
       setProduct(result);
    }).catch(function (err) {
        console.log(err);
    });
  
    return new Promise((resolve, reject) => {
      resolve();
    });
  }
  
  //建立商品
  var setProduct = function(data){
  
      
      var template = getTemplate("product_list");
  
      $(".product_show_list").empty();
  
      $.each(data  , function(key , ele){
          var item = template.clone();
          item.find(".product").attr("href" , "/ShowProduct/Show?Id="+ ele.Id);
          item.find(".photo").attr("src" , "/Files/" + ele.Image);
          item.find(".name").text(ele.Name);
          item.find(".price").text("$" + ele.Price);
          $(".product_show_list").append(item);
      });

      return new Promise((resolve, reject) => {
        resolve();
      });
  }
  
  //建立分類
  var setCategory = function(data , Gender ){
    
      var template = getTemplate("Category_button");
  
      $(".categories").empty();
  
      var item = template.clone(); 
      item.text("全部");
      item.attr("gender" , null);
      item.attr("categoryId" , null);
      item.addClass("currentbtn")
      $(".categories").append(item);
    
      $.each(data , function(key , ele){
        var item = template.clone(); 
  
        
        item.text(ele.Name);
        item.attr("gender" , Gender);
        item.attr("categoryId" , ele.Id);
  
        $(".categories").append(item);
      })
  
      //註冊按鈕
      registerButton();
  }
  
  //畫面初始化
  var initLoad = function(){
  
    getAllProduct()

  }
  
  //分類初始化
  var categoryLoad = function(Gender){
  
    var data = getCategory(Gender);
  
  }
  
  //顯示女裝
  var showWomen = function(){
  
    $(".home").hide("slow" , "swing");
    $(".men").hide("slow" , "swing")
    $(".women").show("slow" , "swing")
    
    
  
  
  }
  
  //顯示男裝
  var showMen = function(){
  
    $(".home").hide("slow" , "swing");
    $(".women").hide("slow" , "swing");
    $(".men").show("slow" , "swing")
  }
  
  //顯示首頁
  var showHome = function(){

    $(".men").hide();
    $(".women").hide();
    $(".home").hide();
    $(".home").show("slow" , "swing");  
  
  }
  
  
  
  
  