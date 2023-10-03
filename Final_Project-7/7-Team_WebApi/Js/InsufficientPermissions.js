document.addEventListener("DOMContentLoaded", function () {

    //倒數10秒跳轉網頁
    var count = 10;
    var redirect = "/Home/Index";

    countDown();

    
    function countDown() {
        var timer = document.getElementById("timer");
        if (count > 0) {
            count--;
            timer.innerHTML = "將在 " + count + " 秒後跳轉至首頁";
            setTimeout(countDown, 1000);
        } else {
            window.location.href = redirect;
        }
    }


});

