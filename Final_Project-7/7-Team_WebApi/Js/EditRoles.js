document.addEventListener("DOMContentLoaded", function () {

    //建立欄位驗證
    setRoleInputValidation();
    setUserInputValidation();
    setPermissionValidation();
    setPermissionCreateValidation();
    setUserCreateValidation();
    setRoleCreateValidation();



    //設置職位資料
    setRoleData();

    //設置使用者資料
    setUserDate();

    //設置權限資料
    setPermissionData();

    //設置職位編輯資料按鈕
    $(document).on("click", ".Rolebtn", function () {

        var RoleId = $(this).attr("RoleId")


        SearchRoleData(RoleId);


    })

    //設置使用者編輯資料按鈕
    $(document).on("click", ".Userbtn", function () {

        var UserId = $(this).attr("UserId");

        SearchUserData(UserId);


    })

    //設置權限編輯資料按鈕
    $(document).on("click", ".Permissionbtn", function () {

        var PermissionId = $(this).attr("PermissionId");

        SearchPermissionData(PermissionId);


    })

    //設置職位新增資料按鈕
    $(document).on("click", ".CreateRole", function () {

        var RoleName = $(".CreateRoleName").val();

        var data = { "Name": RoleName };

        saveRoleCreate(data);

    })

    //設置使用者新增資料按鈕
    $(document).on("click", ".CreateUser", function () {

        var UserName = $(".CreateUserName").val();
        var UserAccount = $(".CreateUserAccount").val();
        var UserPassword = $(".CreateUserPassword").val();

        var data = { "Name": UserName, "Account": UserAccount, "Password": UserPassword };

        saveUserCreate(data);

    })


    //設置權限新增資料按鈕
    $(document).on("click", ".CreatePermission", function () {

        var PermissionName = $(".CreatePermissionName").val();
        var PermissionDescription = $(".CreatePermissionDescription").val();

        var data = { "Name": PermissionName, "Description": PermissionDescription };

        savePermissionCreate(data);

    })

    //提交職位變更
    roleSummit();

    //提交使用者變更
    userSummit();

    //提交權限變更
    permissionSummit();

    //初始化編輯視窗
    initEditRoles();
    initEditUsers();
    initEditPermissions();







})

var initEditRoles = function () {
    $(document).on("click", ".EditRoleClose", function () {
        $(".EditRole").attr("disabled", false);
        $(".EditRoleName").removeClass("valid");
        $(".EditRoleName").removeClass("invalid");
    })
}

var initEditUsers = function () {

    $(document).on("click", ".EditUserClose", function () {
        $(".EditUser").attr("disabled", false);
        $(".EditUserName").removeClass("valid");
        $(".EditUserAccount").removeClass("valid");
        $(".EditUserName").removeClass("invalid");
        $(".EditUserAccount").removeClass("invalid");
        $(".EditUserPassword").val("");
    })
}

var initEditPermissions = function () {

    $(document).on("click", ".EditPermissionClose", function () {
        $(".EditPermission").attr("disabled", false);
        $(".EditPermissionName").removeClass("valid");
        $(".EditPermissionDescription").removeClass("valid");
        $(".EditPermissionName").removeClass("invalid");
        $(".EditPermissionDescription").removeClass("invalid");
    })
}

//建立Role欄位驗證
var setRoleInputValidation = function () {

    var RoleName = document.querySelector(".EditRoleName");

    RoleName.addEventListener("input", function () {

        var RoleNameIsVaild = RoleName.checkValidity();

        if (RoleNameIsVaild) {

            RoleName.classList.add("valid");
            RoleName.classList.remove("invalid");
            $(".EditRole").attr("disabled", false);
        }
        else {

            RoleName.classList.add("invalid");
            RoleName.classList.remove("valid");
            $(".EditRole").attr("disabled", true);
        }

    })

}

//建立User欄位驗證
var setUserInputValidation = function () {

    var UserNameIsValid = true;
    var UserAccountIsValid = true;

    //建立UserName欄位驗證
    var UserName = document.querySelector(".EditUserName");

    UserName.addEventListener("input", function () {

        UserNameIsValid = UserName.checkValidity();

        if (UserNameIsValid) {

            UserName.classList.add("valid");
            UserName.classList.remove("invalid");

        }
        else {

            UserName.classList.add("invalid");
            UserName.classList.remove("valid");

        }

        checkUserSummit(UserNameIsValid, UserAccountIsValid);

    })

    //建立UserAccount欄位驗證
    var UserAccount = document.querySelector(".EditUserAccount");


    UserAccount.addEventListener("input", function () {

        UserAccountIsValid = UserAccount.checkValidity();

        if (UserAccountIsValid) {

            UserAccount.classList.add("valid");
            UserAccount.classList.remove("invalid");

        }
        else {

            UserAccount.classList.add("invalid");
            UserAccount.classList.remove("valid");
        }

        checkUserSummit(UserNameIsValid, UserAccountIsValid);

    })

}

//確認是否可以提交
var checkUserSummit = function (UserNameIsValid, UserAccountIsValid) {

    if (UserNameIsValid && UserAccountIsValid) {

        $(".EditUser").attr("disabled", false);
    }
    else {
        $(".EditUser").attr("disabled", true);
    }
}

//建立Permission欄位驗證
var setPermissionValidation = function () {

    var PermissionNameIsVaild = true;
    var PermissionDescriptionIsVaild = true;

    //建立PermissionName欄位驗證
    var PermissionName = document.querySelector(".EditPermissionName");

    PermissionName.addEventListener("input", function () {

        PermissionNameIsVaild = PermissionName.checkValidity();

        if (PermissionNameIsVaild) {

            PermissionName.classList.add("valid");
            PermissionName.classList.remove("invalid");

        }
        else {

            PermissionName.classList.add("invalid");
            PermissionName.classList.remove("valid");

        }

        checkPermissionSummit(PermissionNameIsVaild, PermissionDescriptionIsVaild);

    })

    //建立PermissionDescription欄位驗證
    var PermissionDescription = document.querySelector(".EditPermissionDescription");


    PermissionDescription.addEventListener("input", function () {

        PermissionDescriptionIsVaild = PermissionDescription.checkValidity();

        if (PermissionDescriptionIsVaild) {

            PermissionDescription.classList.add("valid");
            PermissionDescription.classList.remove("invalid");

        }
        else {

            PermissionDescription.classList.add("invalid");
            PermissionDescription.classList.remove("valid");
        }

        checkPermissionSummit(PermissionNameIsVaild, PermissionDescriptionIsVaild);

    })

}

//確認是否可以提交
var checkPermissionSummit = function (PermissionNameIsVaild, PermissionDescriptionIsVaild) {

    if (PermissionNameIsVaild && PermissionDescriptionIsVaild) {

        $(".EditPermission").attr("disabled", false);
    }
    else {
        $(".EditPermission").attr("disabled", true);
    }
}
//取得資料區
//==================================================================================================

//取得職位資料 ==============>串接API
var setRoleData = function () {

    let url = "/api/RoleApi/GetAll";
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'cache-control': 'no-cache'
        }
    })
        .then(function (response) {
            return response.json();
        })
        .then(function (data) {
            setRoleChoose(data);
        })

}

//取得使用者資料 ==============>串接API
var setUserDate = function () {

    let url = "/api/UserApi/GetAll";

    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'cache-control': 'no-cache'
        }
    })
        .then(function (response) {
            return response.json();
        })
        .then(function (data) {
            setUserChoose(data);
        })


}


//取得權限資料 ==============>串接API
var setPermissionData = function () {

    let url = "/api/PermissionApi/GetAll";

    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'cache-control': 'no-cache'
        }
    })
        .then(function (response) {
            return response.json();
        })
        .then(function (data) {
            setPermissionChoose(data);
        })


}

//設置職位按鈕
var setRoleChoose = function (data) {

    var template = getTemplate("RoleBotton");

    var RoleChoose = $(".RoleChoose");

    RoleChoose.empty();

    data.forEach(element => {

        var RoleBotton = $(template).clone();

        RoleBotton.attr("RoleId", element.Id);
        RoleBotton.text(element.Name);

        RoleChoose.append(RoleBotton);
    });

}

//設置使用者按鈕
var setUserChoose = function (data) {

    var template = getTemplate("UserBotton");

    var UserChoose = $(".UserChoose");

    UserChoose.empty();

    data.forEach(element => {

        var UserBotton = $(template).clone();

        UserBotton.attr("UserId", element.Id);
        UserBotton.text(element.Name);

        UserChoose.append(UserBotton);
    });
}

//設置權限按鈕
var setPermissionChoose = function (data) {

    var template = getTemplate("PermissionBotton");

    var PermissionChoose = $(".PermissionChoose");

    PermissionChoose.empty();

    data.forEach(element => {

        var PermissionBotton = $(template).clone();

        PermissionBotton.attr("PermissionId", element.Id);
        PermissionBotton.text(element.Name);

        PermissionChoose.append(PermissionBotton);
    });
}


//取得模板
var getTemplate = function (name) {

    var templateName = "template." + name;

    var template = $(templateName).html();

    return template;
}



//==================================================================================================    
//取得資料區

//編輯資料區
//==================================================================================================



//搜尋職位資料 ==============>串接API
var SearchRoleData = function (RoleId) {

    let url = "/api/RoleApi/Get?RoleId=" + RoleId;

    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(function (response) {
            return response.json();
        })
        .then(function (data) {
            setRoleModel(data);
        })

}


//搜尋使用者資料 =================>串接API
var SearchUserData = function (UserId) {

    let url = "/api/UserApi/Get?UserId=" + UserId;

    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(function (response) {
            return response.json();
        })
        .then(function (data) {
            setUserModel(data);
        })

}

//搜尋權限資料 =================>串接API
var SearchPermissionData = function (PermissionId) {

    let url = "/api/PermissionApi/Get?PermissionId=" + PermissionId;

    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(function (response) {
            return response.json();
        })
        .then(function (data) {
            setPermissionModel(data);
        })

}

//設置RoleModel資料
var setRoleModel = function (data) {

    var RoleModel = $("#RoleModel");

    RoleModel.find(".EditRoleName").val(data.Name);
    RoleModel.find(".EditRole").attr("RoleId", data.Id);

}


//設置UserModel資料
var setUserModel = function (data) {

    var UserModel = $("#UserModel");

    UserModel.find(".EditUserName").val(data.Name);
    UserModel.find(".EditUserAccount").val(data.Account);

    UserModel.find(".EditUser").attr("UserId", data.Id);

}

//設置PermissionModel資料
var setPermissionModel = function (data) {

    var PermissionModel = $("#PermissionModel");

    PermissionModel.find(".EditPermissionName").val(data.Name);
    PermissionModel.find(".EditPermissionDescription").val(data.Description);

    PermissionModel.find(".EditPermission").attr("PermissionId", data.Id);
}


//==================================================================================================
//編輯資料區


//儲存資料區
//==================================================================================================

//提交職位變更
var roleSummit = function () {

    $(".EditRole").click(function () {

        var RoleId = $(this).attr("RoleId");

        var RoleName = $("#RoleModel").find(".EditRoleName").val();

        var data = { "Id": RoleId, "Name": RoleName };

        saveRole(data);
    })
}


//提交使用者變更
var userSummit = function () {

    $(".EditUser").click(function () {

        var UserId = $(this).attr("UserId");

        var UserName = $("#UserModel").find(".EditUserName").val();
        var UserAccount = $("#UserModel").find(".EditUserAccount").val();
        var UserPassword = $("#UserModel").find(".EditUserPassword").val();
        $("#UserModel").find(".EditUserPassword").val("")

        var data = { "Id": UserId, "Name": UserName, "Account": UserAccount, "Password": UserPassword };

        saveUser(data);
    })
}

//提交權限變更
var permissionSummit = function () {

    $(".EditPermission").click(function () {

        var PermissionId = $(this).attr("PermissionId");

        var PermissionName = $("#PermissionModel").find(".EditPermissionName").val();
        var PermissionDescription = $("#PermissionModel").find(".EditPermissionDescription").val();

        var data = { "Id": PermissionId, "Name": PermissionName, "Description": PermissionDescription };

        savePermission(data);
    })
}

//儲存職位變更 ==========>串接API
var saveRole = function (data) {

    let url = "/api/RoleApi/Update";

    fetch(url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }).then(function (response) {

    })

    setTimeout(() => {
        setRoleData();
    }, 300);


}

//儲存使用者變更 ==========>串接API
var saveUser = function (data) {

    let url = "/api/UserApi/Update";

    fetch(url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',

        },
        body: JSON.stringify(data)
    }).then(function (response) {
        setUserDate();
    })

   


    // window.location.reload(true);
}

//儲存權限變更 ==========>串接API
var savePermission = function (data) {

    let url = "/api/PermissionApi/Update";

    fetch(url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }).then(function (response) {
        setPermissionData();
    })

  


    //window.location.reload(true);
}






//CreateModel
//==================================================================================================

//RoleCreateModel欄位驗證
var setRoleCreateValidation = function () {

    var RoleName = document.querySelector(".CreateRoleName");

    RoleName.addEventListener("input", function () {

        var RoleNameIsVaild = RoleName.checkValidity();

        if (RoleNameIsVaild) {

            RoleName.classList.add("valid");
            RoleName.classList.remove("invalid");
            $(".CreateRole").attr("disabled", false);
        }
        else {

            RoleName.classList.add("invalid");
            RoleName.classList.remove("valid");
            $(".CreateRole").attr("disabled", true);
        }

    })
}

//UserCreateModel欄位驗證
var setUserCreateValidation = function () {

    var UserNameIsVaild = false;
    var UserAccountIsVaild = false;
    var UserPasswordIsVaild = false;

    //建立UserName欄位驗證
    var UserName = document.querySelector(".CreateUserName");

    UserName.addEventListener("input", function () {

        UserNameIsVaild = UserName.checkValidity();

        if (UserNameIsVaild) {

            UserName.classList.add("valid");
            UserName.classList.remove("invalid");

        }
        else {

            UserName.classList.add("invalid");
            UserName.classList.remove("valid");

        }

        checkUserCreateSummit(UserNameIsVaild, UserAccountIsVaild, UserPasswordIsVaild);

    })


    //建立UserAccount欄位驗證
    var UserAccount = document.querySelector(".CreateUserAccount");

    UserAccount.addEventListener("input", function () {

        UserAccountIsVaild = UserAccount.checkValidity();

        if (UserAccountIsVaild) {

            UserAccount.classList.add("valid");
            UserAccount.classList.remove("invalid");

        }
        else {

            UserAccount.classList.add("invalid");
            UserAccount.classList.remove("valid");
        }

        checkUserCreateSummit(UserNameIsVaild, UserAccountIsVaild, UserPasswordIsVaild);

    })

    //建立UserPassword欄位驗證
    var UserPassword = document.querySelector(".CreateUserPassword");

    UserPassword.addEventListener("input", function () {

        UserPasswordIsVaild = UserPassword.checkValidity();

        if (UserPasswordIsVaild) {

            UserPassword.classList.add("valid");
            UserPassword.classList.remove("invalid");
            $(".CreateUser").attr("disabled", false);
        }
        else {

            UserPassword.classList.add("invalid");
            UserPassword.classList.remove("valid");
            $(".CreateUser").attr("disabled", true);
        }

        checkUserCreateSummit(UserNameIsVaild, UserAccountIsVaild, UserPasswordIsVaild);
    })

}

//確認是否可以提交
var checkUserCreateSummit = function (UserNameIsVaild, UserAccountIsVaild, UserPasswordIsVaild) {

    if (UserNameIsVaild && UserAccountIsVaild && UserPasswordIsVaild) {

        $(".CreateUser").attr("disabled", false);
    }
    else {
        $(".CreateUser").attr("disabled", true);
    }
}

//PermissionCreateModel欄位驗證
var setPermissionCreateValidation = function () {

    var PermissionNameIsVaild = false;
    var PermissionDescriptionIsVaild = false;

    //建立PermissionName欄位驗證
    var PermissionName = document.querySelector(".CreatePermissionName");

    PermissionName.addEventListener("input", function () {

        PermissionNameIsVaild = PermissionName.checkValidity();

        if (PermissionNameIsVaild) {

            PermissionName.classList.add("valid");
            PermissionName.classList.remove("invalid");

        }
        else {

            PermissionName.classList.add("invalid");
            PermissionName.classList.remove("valid");

        }

        checkPermissionCreateSummit(PermissionNameIsVaild, PermissionDescriptionIsVaild);

    })

    //建立PermissionDescription欄位驗證
    var PermissionDescription = document.querySelector(".CreatePermissionDescription");

    PermissionDescription.addEventListener("input", function () {

        PermissionDescriptionIsVaild = PermissionDescription.checkValidity();

        if (PermissionDescriptionIsVaild) {

            PermissionDescription.classList.add("valid");
            PermissionDescription.classList.remove("invalid");

        }
        else {

            PermissionDescription.classList.add("invalid");
            PermissionDescription.classList.remove("valid");
        }

        checkPermissionCreateSummit(PermissionNameIsVaild, PermissionDescriptionIsVaild);

    })
}

//確認是否可以提交
var checkPermissionCreateSummit = function (PermissionNameIsVaild, PermissionDescriptionIsVaild) {

    if (PermissionNameIsVaild && PermissionDescriptionIsVaild) {

        $(".CreatePermission").attr("disabled", false);
    }
    else {
        $(".CreatePermission").attr("disabled", true);
    }
}


var saveRoleCreate = function (data) {

    let url = "/api/RoleApi/Create";

    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)

    })
    .then(function (response) {
        if(!response.ok)
        {
            alert("職位名稱重複");
        }
        else{
            setRoleData();
        }
    })



}

var saveUserCreate = function (data) {

    let url = "/api/UserApi/Create";

    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
    .then(function (response) {
        if(!response.ok)
        {
            alert("帳號重複");
        }
    })

    setTimeout(() => {
        setUserDate();
    }, 300);
}

var savePermissionCreate = function (data) {

    let url = "/api/PermissionApi/Create";

    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
.then(function (response) {
    if(!response.ok)
    {
        alert("權限名稱重複");

    }
    else{
        setPermissionData();
    }
})


}