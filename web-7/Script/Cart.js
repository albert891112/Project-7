
/* 購物車明細 begin */
function CartItem(timeStamp, productId, productName, photo, price, qty) {
    this.timeStamp = timeStamp;
    this.productId = productId;
    this.productName = productName;
    this.photo = photo;
    this.price = price;
    this.qty = qty;
}
CartItem.prototype.subTotal = function () {
    return this.price * this.qty;
}
/* 購物車明細 begin */

/* 購物車主檔 begin */
let cart = {
    items: [],
    removeItem: function (productId) {
        let index = this.items.findIndex(function (item) {
            return item.productId == productId;
        });
        if (index != -1) {
            this.items.splice(index, 1);
        }
    },

    getTotal : function(){
        let total = 0;
        this.items.forEach(function(item){
            total += item.subTotal();
        });
        return total;
    },

};
/* 購物車主檔 end */


// 最後一項之後不能有逗號
const datasourceStr = `[
                        {"timeStamp" :1, "productId" : 6, "productName" : "prod name 6", "price" : 1000, "qty" : 1, "photo" : "pic01.JPG"},
                        {"timeStamp" :2, "productId" : 2, "productName" : "prod name 2", "price" : 6200, "qty" : 1, "photo" : "pic-login.jpg" }                        
                        ]`;

// 解析 json 字串為 json 物件
let cartItems = JSON.parse(datasourceStr).map( item => new CartItem(item.timeStamp, item.productId, item.productName, item.photo, item.price, item.qty) );
cart.items = cartItems;
console.log("cart total = " + cart.getTotal());
console.log("cart items = ", cart.items.length);

document.addEventListener("DOMContentLoaded", function(){
    renderCartInfo();
})

/**
 * 顯示購物車內容
 */
function renderCartInfo(){
    // 判斷 cart 必需是 array
    if (!Array.isArray(cart.items)) throw new Error("目前無法正確讀取 購物車資訊");

    let template = document.querySelector('#templateContainer > tbody > tr');

    let container = document.querySelector('#cartTable > tbody');
    // 每次都先清空 container
    container.innerHTML = "";

    let counter =0;
    let datasource  = cart.items;
    datasource.forEach(function (item) {
        counter++;

        let newItem = template.cloneNode(true);
        // todo init attributes
        newItem.querySelector("#productImg").src = "/images/" + item.photo;
        newItem.querySelector("#productName").innerText = item.productName;
        newItem.querySelector("#price").innerText = numberHelper.toLocalString( item.price,  0); // 顯示千分位
        newItem.querySelector("#qty").value = item.qty;
        newItem.querySelector("#subTotal").innerText = numberHelper.toLocalString( item.subTotal(),  0); // 顯示千分位

        newItem.querySelector("#decrement").dataset.productId = item.productId;
        newItem.querySelector("#increment").dataset.productId = item.productId;

        container.insertAdjacentElement('beforeend', newItem); // 在目標元素的內部，放在其所有子元素之後
    });

    // 顯示總計
    document.querySelector("#totalPanel").innerText =
                "總計: " + numberHelper.toLocalString( cart.getTotal(),  0); // 顯示千分位

    if(cart.items.length ==0){
        document.getElementById("btnCheckout").disabled = true;
    }else{
        document.getElementById("btnCheckout").disabled = false;
    }

}

// 更新數量, 並重算總額, 若數量為零, 表示刪除
document.addEventListener("click", function (event) {
    console.info(event.target);
    console.log("event.target.matches(\"#increment\")", event.target.matches("#increment"));

    // event.target: 這是觸發事件的 DOM 元素, 型別是 Element, 它有 matches method用於檢查元素是否匹配給定的 CSS 選擇器字串
    if (event.target.matches("#increment") || event.target.matches("#decrement")) {
        // 判斷是按了加或減, 決定要異動數量是 1 or -1
        let deltaQty = event.target.matches("#increment") ? 1 : -1;

        let productId = event.target.dataset.productId;
        let item = cart.items.find(function (item) {
            return item.productId == productId;
        });

        // console.log("item.qty before=", item.qty);
        item.qty += deltaQty;
        // console.log("item.qty after=", item.qty);
        console.info(cart.items);

        if(item.qty <= 0){
            cart.removeItem(productId);
        }

        renderCartInfo();
    }

});

// 實作排序功能
document.getElementById("sort").addEventListener("change", function(event){
    let sortKey = event.target.value; // 取得選取的值
    console.log("排序規則= " , sortKey);
    if(sortKey == ""){
        cart.items.sort(function(a,b){
            return a.timeStamp - b.timeStamp;
        });
    }else if(sortKey == "unitprice"){
        cart.items.sort(function(a,b){
            return a.price - b.price;
        });
    }else if(sortKey == "qty"){
        cart.items.sort(function(a,b){
            return b.qty - a.qty || a.price - b.price;
        });
    }else if(sortKey == "subtotal"){
        cart.items.sort(function(a,b){
            return b.subTotal() - a.subTotal(); // 遞減排序
        });
    }

    renderCartInfo();

});
