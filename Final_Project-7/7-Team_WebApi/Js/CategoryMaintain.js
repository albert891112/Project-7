document.addEventListener("DOMContentLoaded", function () {

    //取得商品分類列表
    initLoad();

    //驗證
    setEditCategoryValidation();
    setCreateCategoryValidation();


    //編輯分類按鈕
    $(document).on("click", ".EditCategoryButton", function () {

        var categoryId = $(this).attr("categoryId");

        SearchCategory(categoryId);
    })

    //編輯分類儲存
    $(document).on("click", ".EditCateogry", function () {

        var input = $("#EditCategoryModel").find(".EditCategoryName");
        var id = input.attr("categoryId");
        var name = input.val();

        var Criteria = {
            "Id": id,
            "Name": name,
        }

        saveChange(Criteria);
    })

    //新增分類儲存
    $(document).on("click", ".CreateCategory", function () {

        var input = $("#CreateCategoryModel").find(".CreateCategoryName");
        var inputMen = $("#CreateCategoryModel").find(".CreateMenCheck");
        var inputWomen = $("#CreateCategoryModel").find(".CreateWomenCheck");

        var newCategory = input.val();

        var gender = [];

        if (inputMen.prop("checked")) {
            gender.push(1);
        }

        if (inputWomen.prop("checked")) {
            gender.push(2)
        }

        var Criteria = {
            "Name": newCategory,
            "Gender": gender
        }

        CreateCategory(Criteria);

    })

    //重設新增分類model
    $(document).on("click", ".CreateCategoryClose", function () {

        initCreateModel();

    })
    $(document).on("click", ".CreateCategory", function () {
        initCreateModel();
    })

    //重設編輯分類model
    $(document).on("click", ".EditCateogryClose", function () {

        initEditModel();

    })
    $(document).on("click", ".EditCateogry", function () {
        initEditModel();
    })

    $(document).on("click", ".SaveEdit", function () {
        
        var id = $(this).attr("categoryId");

        var inputMen = $("#"+id).find(".EditMenCheck");
        var inputWomen = $("#"+id).find(".EditWomenCheck");
        var gender = [];

        if (inputMen.prop("checked")) {
            gender.push(1);
        }

        if (inputWomen.prop("checked")) {
            gender.push(2)
        }

        var Criteria = {
            "Id": id,
            "Gender": gender
        }

        console.log(Criteria);

        saveChange(Criteria);
    })

    $(document).on("click", ".DeleteCategoryButton", function () {

        var model = $("#DeleteCategoryModel");

        var id = $(this).attr("categoryId");

        console.log(id);

        model.find(".DeleteCategory").attr("categoryId", id);

    })

    $(document).on("click", ".DeleteCategory", function () {

        var id = $(this).attr("categoryId");

        console.log(id);

        deleteCategory(id);

    })


})

//取得商品分類列表
//==================================================================================================

var initLoad = function () {    
    setMenCategoryList();
    setWomenCategoryList();
    setEditCategoryList();
}


//設置商品分類列表
var setEditCategoryList = function () {
    
    let url = "/api/CategoryApi/GetAll";

        fetch(url , {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        })
        .then(res => res.json())
        .then(data => setEditList(data))
}

//取得女裝分類列表資料 
var setWomenCategoryList = function () {
    let url = "/api/CategoryApi/GetByGender?GenderId=2";

    fetch(url , {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    })
    .then(res => res.json())
    .then(data => setWomenList(data))
   
}

//取得男裝分類列表資料 
var setMenCategoryList = function () {

    let url = "/api/CategoryApi/GetByGender?GenderId=1"
        
    fetch(url , {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    })
    .then(res => res.json())
    .then(data => setMenList(data))

}

//設置男裝分類列表
var setMenList = function (data) {

    var template = getTemplate("GenderCategoryList");

    var men = $(".men");

    men.empty();

    data.forEach(ele => {

        var item = $(template).clone();

        item.text(ele.Name);

        men.append(item);
    })
}

//設置女裝分類列表
var setWomenList = function (data) {

    var template = getTemplate("GenderCategoryList");

    var women = $(".women");

    women.empty();

    data.forEach(ele => {

        var item = $(template).clone();

        item.text(ele.Name);

        women.append(item);
    })
}

//設置編輯分類列表
var setEditList = function (data) {

    var template = getTemplate("categoryList");

    var categories = $(".categories");

    categories.empty();

    data.forEach(ele => {

        var item = $(template).clone();

        item.find(".categoryname").text(ele.Name);
        item.find(".EditMenCheck").attr("categoryId", ele.Id);
        item.find(".EditWomenCheck").attr("categoryId", ele.Id);
        item.find(".EditCategoryButton").attr("categoryId", ele.Id);
        item.find(".SaveEdit").attr("categoryId", ele.Id);
        item.find(".row").attr("id", ele.Id);
        item.find(".DeleteCategoryButton").attr("categoryId", ele.Id);

        ele.GenderCategories.forEach(obj => {
            if (obj.Id == 1) {
                item.find(".EditMenCheck").prop("checked", true);
            }

            if (obj.Id == 2) {
                item.find(".EditWomenCheck").prop("checked", true);
            }
        })

        categories.append(item);
    })


}

//取得模板
var getTemplate = function (name) {

    var templateName = "template." + name;

    var template = $(templateName).html();

    return template;
}



//=================================================================================================
//取得商品分類列表


//編輯分類
//=================================================================================================

//取得分類資料 
var SearchCategory = function (id) {
    
    let url = "/api/CategoryApi/Get?Id=" + id;

    fetch(url , {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    })
    .then(res => res.json())
    .then(data => setEditdata(data))

}

//設置編輯分類資料 
var setEditdata = function (data) {

    var model = $("#EditCategoryModel").find(".EditCategoryName");

    model.val(data.Name);

    model.attr("categoryId", data.Id);
}

//儲存變更 ==============================================> 這邊要改成ajax
var saveChange = function (data) {

    let url = "/api/CategoryApi/Update";

    fetch(url , {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })
    .then(function (res) {
        if(res.ok){
            initLoad();
        }
        else{
            alert("名稱重複");
        }
       
    })
   
}

//=================================================================================================
//編輯分類

//新增分類
//=================================================================================================

//新增分類 
var CreateCategory = function (data) {
    
    let url = "/api/CategoryApi/Create";

    fetch(url , {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })
    .then(function (res) {
        if(res.ok){
            initLoad();
        }
        else{
            alert("名稱重複");
        }
    })
}
//=================================================================================================
//新增分類

//驗證
//=================================================================================================
var setEditCategoryValidation = function () {

    var model = $("#EditCategoryModel");

    var name = document.querySelector(".EditCategoryName");

    name.addEventListener("input", function () {

        if (name.checkValidity()) {
            $(this).removeClass("invalid");
            $(this).addClass("valid");
            $(".EditCateogry").prop("disabled", false);

        }
        else {
            $(this).removeClass("valid");
            $(this).addClass("invalid");
            $(".EditCateogry").prop("disabled", true);
        }

    })
}

var setCreateCategoryValidation = function () {

    var model = $("#CreateCategoryModel");

    var name = document.querySelector(".CreateCategoryName");

    var NameIsVaild = false;
    var checkboxIsVaild = false;

    name.addEventListener("input", function () {

        NameIsVaild = name.checkValidity();

        if (NameIsVaild) {
            $(this).removeClass("invalid");
            $(this).addClass("valid");


        }
        else {
            $(this).removeClass("valid");
            $(this).addClass("invalid");
        }

        console.log(NameIsVaild);
        checkSummit(NameIsVaild, checkboxIsVaild);
    })

    var inputMen = $("#CreateCategoryModel").find(".CreateMenCheck");
    var inputWomen = $("#CreateCategoryModel").find(".CreateWomenCheck");

    inputMen.change(function () {


        if (inputMen.prop("checked") || inputWomen.prop("checked")) {
            checkboxIsVaild = true;
        }
        else {
            checkboxIsVaild = false;
        }
        console.log(checkboxIsVaild);

        checkSummit(NameIsVaild, checkboxIsVaild);
    })


    inputWomen.change(function () {


        if (inputMen.prop("checked") || inputWomen.prop("checked")) {
            checkboxIsVaild = true;
        }
        else {
            checkboxIsVaild = false;
        }

        console.log(checkboxIsVaild);

        checkSummit(NameIsVaild, checkboxIsVaild);
    })



}

var checkSummit = function (NameIsVaild, checkboxIsVaild) {

    if (NameIsVaild && checkboxIsVaild) {
        $(".CreateCategory").prop("disabled", false);
    }
    else {
        $(".CreateCategory").prop("disabled", true);
    }
}

//=================================================================================================
//驗證


var initEditModel = function () {
    var input = $("#EditCategoryModel").find(".EditCategoryName");

    input.removeClass("valid");
    input.removeClass("invalid");

    $(".EditCateogry").attr("disabled", false);
}

var initCreateModel = function () {
    var inputName = $("#CreateCategoryModel").find(".CreateCategoryName");

    inputName.val("");
    inputName.removeClass("valid");
    inputName.removeClass("invalid");

    var inputMen = $("#CreateCategoryModel").find(".CreateMenCheck");
    var inputWomen = $("#CreateCategoryModel").find(".CreateWomenCheck");

    inputMen.prop("checked", false);
    inputWomen.prop("checked", false);

    $(".CreateCategory").attr("disabled", true);
}


var deleteCategory = function (id) {
    let url = "/api/CategoryApi/Delete?id=" + id;

        fetch(url , {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json"
            }
        })
        .then(function (res) {
            if(res.ok){
                initLoad();
            }
            else{
                alert("刪除失敗,仍有商品屬於此分類");
            }
        })
}