document.addEventListener("DOMContentLoaded", function () {



    // var getcarItems = function(Criteria){
    
    //     let url = '/api/CartApi/Cart';
        
    
    //     fetch(url, {
    //         method: 'POST',
    //         body: JSON.stringify(Criteria),
    //         headers: new Headers({
    //             'Content-Type': 'application/json'
    //         })
    //     }).then(function (response) {
    //       return response.json();
    //     }).then(function (result) {
    //        setProduct(result);
    //     }).catch(function (err) {
    //         console.log(err);
    //     });
      
        
    //   }

   // 使用 JSON 字串模擬購物車數據
   var cartDataJSON = `
    [
        {"id": 1, "image": "product1.jpg", "name": "商品1", "unitPrice": 10.99, "quantity": 2, "subtotal": 21.98},
        {"id": 2, "image": "product2.jpg", "name": "商品2", "unitPrice": 19.99, "quantity": 1, "subtotal": 19.99},
        {"id": 3, "image": "product3.jpg", "name": "商品3", "unitPrice": 9.99, "quantity": 3, "subtotal": 29.97},
        {"id": 4, "image": "product4.jpg", "name": "商品4", "unitPrice": 15.99, "quantity": 2, "subtotal": 31.98},
        {"id": 5, "image": "product5.jpg", "name": "商品5", "unitPrice": 14.49, "quantity": 4, "subtotal": 57.96}
    ]`;
// 解析 JSON 字串為 JavaScript 物件
var cartData = JSON.parse(cartDataJSON);

// 購物車排序方式（預設為單價排序）
let sortBy = 'unitprice';

// 取得購物車數據的函數
function getCartData() {
    // 根據排序方式重新排序購物車數據
    const sortedData = sortBy === 'unitprice' ?
        cartData.slice().sort((a, b) => a.unitPrice - b.unitPrice) :
        cartData.slice().sort((a, b) => a.subtotal - b.subtotal);

    return Promise.resolve(sortedData);
}

// 更新購物車數量的函數
function updateCartQuantity(productId, newQuantity) {
    // 在實際應用中，你可以在這裡更新購物車數據
    // 這裡只是一個示例，不會真正地修改購物車數據
    const updatedCartData = cartData.map(item => {
        if (item.id === productId) {
            if (newQuantity <= 0) {
                // 如果新數量小於等於0，則將商品從購物車數據中移除
                return null;
            }
            item.quantity = newQuantity;
            item.subtotal = item.unitPrice * newQuantity;
        }
        return item;
    });

    // 移除為null的項目
    const filteredCartData = updatedCartData.filter(item => item !== null);

    // 更新購物車數據
    cartData.length = 0; // 清空原始數據
    filteredCartData.forEach(item => cartData.push(item)); // 更新為新數據

    // 重新載入購物車數據
    loadCartData();
}

// 更新總金額函數
function updateTotalAmount(data) {
    const totalAmountElement = document.getElementById('totalAmount');
    const totalAmount = data.reduce((sum, item) => sum + item.subtotal, 0);
    totalAmountElement.textContent = totalAmount.toFixed(2); // 顯示小數點兩位
}

// 載入購物車數據並更新 HTML 表格
function loadCartData() {
    const cartTable = document.getElementById('cartTable');

    // 清空表格
    while (cartTable.tBodies[0].firstChild) {
        cartTable.tBodies[0].removeChild(cartTable.tBodies[0].firstChild);
    }

    // 取得購物車數據
    getCartData().then(data => {
        // 遍歷數據並將其添加到表格中
        data.forEach(item => {
            const row = cartTable.tBodies[0].insertRow();
            row.innerHTML = `
                <td><img src="${item.image}" alt="${item.name}" width="50"></td>
                <td>${item.name}</td>
                <td class="text-end ps-4 pe-4">${item.unitPrice.toFixed(2)}</td>
                <td class="text-end ps-4 pe-4">${item.quantity}</td>
                <td class="text-end ps-4 pe-4">${item.subtotal.toFixed(2)}</td>
                <td>
                    <button class="btn btn-success" onclick="updateCartQuantity(${item.id}, ${item.quantity + 1})">+1</button>
                    <button class="btn btn-danger" onclick="updateCartQuantity(${item.id}, ${item.quantity - 1})">- 1</button>
                </td>
            `;
        });

        // 更新總金額
        updateTotalAmount(data);
    });
}

// 處理下拉清單選擇
function handleSortChange() {
    const sortSelect = document.getElementById('sort');
    sortBy = sortSelect.value;
    loadCartData();
}

// 監聽下拉清單變更事件
const sortSelect = document.getElementById('sort');
sortSelect.addEventListener('change', handleSortChange);

// 當頁面載入完成後，載入購物車數據
window.onload = loadCartData;

$("#btnpay").click(function(){

    btnpay();    
        
}); 

var btnpay = function(){
  
    window.location.href = "Ordermerge.html"; 
  
  }

  $("#btnCheckoutHome").click(function(){

    btnCheckoutHome();    
        
});  

var btnCheckoutHome = function(){
  
    window.location.href = "HomePhage.html"; 
  
  }
});
