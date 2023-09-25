//DOM載入區
//====================================================================================================
document.addEventListener('DOMContentLoaded', function () {

    //設置職位下拉清單
    setRolesData();

    //選擇職位,取得該職位的使用者和權限
    $(document).on("change", "#rolesSelect", function () {

        var id = $(this).val();

        //如果沒有選擇職位，清空選項
        if (id == "") {

            clearChoose();
            return;
        }

        chooseLoad(id);
    })

    //使用者變更
    $(document).on("change", ".user", function () {

        var User = $(this);

        changeUser(User);
    })

    //權限變更
    $(document).on("change", ".permission", function () {

        var Permission = $(this);

        changePermission(Permission);
    })

});
//====================================================================================================
//DOM載入區


//資料載入區
//====================================================================================================


//取得職位 =================> 串接api取得資料
var setRolesData = function () {

    //使用fetch串接api取得資料
    var url = "/api/RoleApi/GetAll";
    fetch(url , {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(function (response) {
        return response.json();
    })
    .then(function (data) {
        setRolesSelect(data);
    })
}

//取得使用者 =================> 串接api取得資料
var setUserData = function (id) {

    //使用fetch串接api取得資料
    var url = "/api/RoleApi/GetRolesUser?RoleId=" + id ;
    fetch(url , {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(function (response) {
        return response.json();
    })
    .then(function (data) {
        setUserChoose(data);
    })

    
}

//取得權限 =================> 串接api取得資料
var setPermissionData = function (id) {

     //使用fetch串接api取得資料
     var url = "/api/RoleApi/GetRolesPermission?RoleId=" + id ;
     fetch(url , {
         method: 'GET',
         headers: {
             'Content-Type': 'application/json'
         }
     })
     .then(function (response) {
         return response.json();
     })
     .then(function (data) {
        setPermissionChoose(data);
     })

    
}

//跟據職位取得使用者和權限
var chooseLoad = function (id) {

    setUserData(id);

    setPermissionData(id);
}

//設定職位下拉清單
var setRolesSelect = function (data) {

    //新增下拉清單選項
    var select = document.querySelector("#rolesSelect");

    $.each(data, function (i, item) {
        //建立option
        var option = document.createElement("option");

        option.value = item.Id;
        option.text = item.Name;

        select.appendChild(option);
    })
}


//設定使用者按鈕 ===========>設定際有和不既有
var setUserChoose = function (data) {

    //取得模板
    var template = getTemplate("userCheckboxTemplate");

    //取得放置按鈕的ul
    var ul = $(".UserChoose");

    //清空ul
    ul.empty();

    //設定既有選項
    $.each(data.InGroup, function (i, item) {
        //複製模板
        var clone = $(template).clone();

        //設定id
        clone.find(".form-check-input").attr("id", item.Account);
        clone.find(".form-check-input").attr("UserId", item.Id);

        //設定文字
        clone.find(".form-check-label").attr("for", item.Account);
        clone.find(".form-check-label").text(item.Account);

        //增加勾選
        clone.find(".form-check-input").prop("checked", true);

        //放置按鈕
        ul.append(clone);
    })

    //設定不既有選項
    $.each(data.OutOfGroup, function (i, item) {
        //複製模板
        var clone = $(template).clone();

        //設定id
        clone.find(".form-check-input").attr("id", item.Account);
        clone.find(".form-check-input").attr("UserId", item.Id);

        //設定文字
        clone.find(".form-check-label").attr("for", item.Account);
        clone.find(".form-check-label").text(item.Account);

        //放置按鈕
        ul.append(clone);
    })
}

//設定權限按鈕 ===========>設定際有和不既有
var setPermissionChoose = function (data) {

    //取得模板
    var template = getTemplate("permissionCheckboxTemplate");

    //取得放置按鈕的ul
    var ul = $(".PermissionChoose");

    //清空ul
    ul.empty();

    //設定既有選項
    $.each(data.InGroup, function (i, item) {
        //複製模板
        var clone = $(template).clone();

        //設定id
        clone.find(".form-check-input").attr("id", item.Name);
        clone.find(".form-check-input").attr("PermissionId", item.Id);

        //設定文字
        clone.find(".form-check-label").attr("for", item.Name);
        clone.find(".form-check-label").text("["+ item.Name+"]" + "  :  " + item.Description);

        //增加勾選
        clone.find(".form-check-input").prop("checked", true);

        //放置按鈕
        ul.append(clone);
    })

    //設定不既有選項
    $.each(data.OutOfGroup, function (i, item) {
        //複製模板
        var clone = $(template).clone();

        //設定id
        clone.find(".form-check-input").attr("id", item.Name);
        clone.find(".form-check-input").attr("PermissionId", item.Id);

        //設定文字
        clone.find(".form-check-label").attr("for", item.Name);
        clone.find(".form-check-label").text("["+ item.Name+"]"+ "  :  " + item.Description);

        //放置按鈕
        ul.append(clone);
    })
}


//取得模板
var getTemplate = function (name) {

    var templateName = "template." + name;

    var template = $(templateName).html();

    return template;
}


//清空選項
var clearChoose = function () {

    //清空使用者選項
    $(".UserChoose").empty();

    //清空權限選項
    $(".PermissionChoose").empty();
}


//====================================================================================================
//資料載入區


//取得變更資料區
//====================================================================================================

//使用者變更
var changeUser = function (User) {

    //取得當前職位Id
    var RoleId = $("#rolesSelect").val();

    //取得使用者Id
    var UserId = User.attr("UserId");

    //判斷是否勾選
    if (User.prop("checked")) {

        //若是勾選，將使用者加入職位
        AddUserInRole(RoleId, UserId);
    }
    else {

        //若是取消勾選，將使用者從職位中移除
        DeleteUserFromRole(RoleId, UserId);
    }

}

//權限變更
var changePermission = function (Permission) {

    //取得當前職位Id
    var RoleId = $("#rolesSelect").val();

    //取得權限Id
    var PermissionId = Permission.attr("PermissionId");

    //判斷是否勾選
    if (Permission.prop("checked")) {

        //若是勾選，將權限加入職位
        AddPermissionInRole(RoleId, PermissionId);
    }
    else {
        //若是取消勾選，將權限從職位中移除
        DeletePermissionFromRole(RoleId, PermissionId);
    }
}

//====================================================================================================
//取得變更資料區

//回傳資料區
//====================================================================================================

//將使用者從職位中移除
var DeleteUserFromRole = function (RoleId, UserId) {

    //建立傳遞資料
    var criteria = {
        RoleId: RoleId,
        UpdateId: UserId
    };

    //取得api路徑
    let url = "/api/RoleApi/RemoveUserFromRole";

    //使用fetch串接api編輯資料
    fetch(url , {

        method: 'POST',
        body: JSON.stringify(criteria),
        headers: {
            'Content-Type': 'application/json'
        }
    })
}

//將使用者加入職位
var AddUserInRole = function (RoleId, UserId) {
    //建立傳遞資料
    var criteria = {
        RoleId: RoleId,
        UpdateId: UserId
    };

    //取得api路徑
    let url = "/api/RoleApi/AddUserToRole";

    //使用fetch串接api編輯資料
    fetch(url , {

        method: 'POST',
        body: JSON.stringify(criteria),
        headers: {
            'Content-Type': 'application/json'
        }
    })
}

//將權限從職位中移除
var DeletePermissionFromRole = function (RoleId, PermissionId) {

    //建立傳遞資料
    var criteria = {
        RoleId: RoleId,
        UpdateId: PermissionId
    };

    //取得api路徑
    let url = "/api/RoleApi/RemovePermissionFromRole";

    //使用fetch串接api編輯資料
    fetch(url , {

        method: 'POST',
        body: JSON.stringify(criteria),
        headers: {
            'Content-Type': 'application/json'
        }
    })
}

//將權限加入職位
var AddPermissionInRole = function (RoleId, PermissionId) {
    //建立傳遞資料
    var criteria = {
        RoleId: RoleId,
        UpdateId: PermissionId
    };

    //取得api路徑
    let url = "/api/RoleApi/AddPermissionToRole";

    //使用fetch串接api編輯資料
    fetch(url , {

        method: 'POST',
        body: JSON.stringify(criteria),
        headers: {
            'Content-Type': 'application/json'
        }
    }) 
}
        

        
