let dateHelper = {
    // 判斷 date 是否為 Date 物件, 若沒傳入引數, undefined, null, 不合理的 Date 都會回傳 false
    isDate: function (date) {
        return (date instanceof Date) && !isNaN(date);
    },

    // 傳回 date 增加 days 天數後的新 Date 物件,不會異動 date 原本的值
    addDays: function (date, days) {
        if (!this.isDate(date)) throw new Error('date 引數值不是正確的 Date 物件');
        if (!numberHelper.isInt(days)) throw new Error('days 引數值不是整數');

        let newDate = new Date(date); // 新建一個 Date 物件, 避免修改到原本的 date 物件
        return new Date(newDate.setDate(newDate.getDate() + days));
    },

    // 傳回 date 增加 months 月數後的新 Date 物件,不會異動 date 原本的值
    addMonths: function (date, months) {
        if (!this.isDate(date)) throw new Error('date 引數值不是正確的 Date 物件');
        if (!numberHelper.isInt(months)) throw new Error('months 引數值不是整數');

        let newDate = new Date(date); // 新建一個 Date 物件, 避免修改到原本的 date 物件
        let result = new Date(newDate.setMonth(newDate.getMonth() + months));
        // 檢查新日期是否超過當月天數
        if (date.getDate() !== result.getDate()) {
            // 退回到前一個月的最後一天
            result.setDate(0);
        }
        return result;
    },

    // 傳回日期的 yyyy/MM/dd 格式字串, 若date不是正確 Date 物件, 丟出異常
    toYMD: function (date) {
        if (!this.isDate(date)) throw new Error('date 引數值不是正確的 Date 物件');

        let formatter = Intl.DateTimeFormat('zh-TW', {year: 'numeric', month: '2-digit', day: '2-digit'});
        let result = formatter.format(date);
        return result;
    },

    // 傳回日期的 yyyy/MM/dd HH:mm:ss 格式字串, 若date不是正確 Date 物件, 丟出異常
    toYMDHMS: function (date) {
        if (!this.isDate(date)) throw new Error('date 引數值不是正確的 Date 物件');

        let formatter = Intl.DateTimeFormat('zh-TW', {
            year: 'numeric', month: '2-digit', day: '2-digit',
            hour: '2-digit', minute: '2-digit', second: '2-digit'
        });
        let result = formatter.format(date);
        return result;
    },

}

let numberHelper = {
    // 判斷 num 是不是整數, 若沒傳入引數, undefined, null, float, NaN 都會回傳 false
    isInt: function (num) {
        if (num instanceof Number) {
            return num !== NaN && Number.isInteger(num.valueOf());

        }

        return Number.isInteger(num);
    },

    // 判斷 num 是不是 float(int也算true) 或者是合理的 Number 物件; 若沒傳入引數, undefined, null, float, NaN 都會回傳 false            
    isFloat: function (num) {
        // 如果是 Number 物件, 且不是 NaN, 則視為 float
        if (num instanceof Number && num !== NaN) {
            return true;
        }

        return Number.isFinite(num); // ref https://www.apiref.com/javascript-zh/Reference/Global_Objects/Number/isFinite.htm
    },

    // 將 num 格式化成千分位, 並顯示指定小數位數,預設是0位  
    toLocalString: function (num, digits = 0) {
        if (!this.isFloat(num)) throw new Error('num 引數值不是正確的 數值或 Number 物件');
        if (!this.isInt(digits)) throw new Error('digits 引數值不是整數');
        if (digits < 0) throw new Error('digits 引數值不可小於 0');

        // 首先，取得整數部分
        const integerPart = Math.trunc(num).toLocaleString(); // 整數轉成有千分位的字串

        // 接著，取得小數部分
        const decimalPart = (num % 1).toFixed(digits).substring(1);

        // 最後，合併這兩部分
        return integerPart + decimalPart;
    },


};

let stringHelper = {
    // 判斷 str 是不是空字串, 若沒傳入引數, undefined, null, '' 都會回傳 true
    isNullOrEmpty: function (str) {
        return str === undefined || str === null || str === '';
    },

    // 判斷 str 是不是空白字串, 若沒傳入引數, undefined, null, '  ' 都會回傳 true
    isNullOrWhiltSpace: function (str) {
        return str === undefined || str === null || str.toString().trim() === '';
    },

    // 嘗試將字串轉換成 number, 傳回一個物件,內含屬性: isValid, value, error
    tryParseFloat: function (str) {
        let result = {
            isValid: false,
            value: 0,
            error: ''
        };

        if (this.isNullOrWhiltSpace(str)) {
            result.error = '引數值不可為空白, null, undefined';
            return result;
        }

        let num = Number(str); // 若轉型失敗, 傳回 NaN

        // 判斷 num 是否為 NaN
        let b = isNaN(num);

        if (isNaN(num)) {
            result.value = num;
            error = '無法順利轉型為 number';

            return result;
        }

        result.isValid = true;
        result.value = num;

        return result;
    },

    // 嘗試將字串轉換成 整數, 傳回一個物件,內含屬性: isValid, value, error
    tryParseInt: function (str) {
        let result = {
            isValid: false,
            value: 0,
            error: ''
        };

        let floatResult = this.tryParseFloat(str);
        if (!floatResult.isValid) return result;

        if (!numberHelper.isInt(floatResult.value)) {
            result.error = '無法順利轉型為整數';
            return result;
        }

        result.isValid = true;
        result.value = floatResult.value;
        return result;
    },

    // 嘗試將字串轉換成 Date, 傳回一個物件,內含屬性: isValid, value, error
    // 傳回值只會有日期, 時間都會是 00:00:00
    tryParseDate: function (str) {
        let result = {
            isValid: false,
            value: null,
            error: ''
        };

        if (this.isNullOrWhiltSpace(str)) {
            result.error = '引數值不可為空白, null, undefined';
            return result;
        }

        let date = new Date(str); // 若轉型失敗, 傳回 不合理的 Date 物件, 可以用 isNaN(date) 判斷

        // 判斷 date 是否為 NaN
        if (isNaN(date)) {
            result.value = date;
            error = '無法順利轉型為 Date';

            return result;
        }

        result.isValid = true;
        result.value = new Date(date.setHours(0, 0, 0, 0));

        return result;
    },

    // toBool : function(str){
    //     if(this.isNullOrWhiltSpace(str)) return false;

    //     if (typeof str === 'boolean') {
    //         return str;
    //     }

    //     const normalizedStr = String(str).trim().toLowerCase();

    //     return normalizedStr === '1' || normalizedStr === 'true';
    // },


    // 只有在傳入 1, new Number(1), true, "true"(不拘大小寫)時, 才傳回 true, 其餘都傳回 false -由 copilot 產生
    toBool: function (str) {
        return (str === 1 || str === new Number(1) || str === true || str.toLowerCase() === "true");
    },

    // 將字串右側多餘的文字截斷
    // str 若是 undefined, null, 空字串, 傳回空字串
    truncate: function (str, maxLength) {
        if (this.isNullOrEmpty(str)) return '';

        if (!numberHelper.isInt(maxLength)) throw new Error('maxLength 引數值不是整數');

        if (maxLength < 0) throw new Error('maxLength 引數值不可小於 0');

        if (str.length <= maxLength) return str;

        return str.substr(0, maxLength);
    },
};

let formHelper = {
    /**
     * 取得 radio button list 的值
     * @param {string} elemName
     * @returns {string|null}
     */
    getRadioButtonsSelectedValueByName : function (elemName)
    {
        let elems = document.getElementsByName(elemName); // 用 name 取得所有的 radio button
        if(elems.length == 0) throw new Error(`找不到 name=${genderElemName} 的 radio button`);
        
        for (let i = 0; i < elems.length; i++) {
            if (elems[i].checked) {
                return elems[i].value;
            }
        }
        return null;
    },

    /**
     * 設定 radio button list 的值
     * @param {string} elemName
     * @param {string} value
     */
    setRadioButtonsValueByName :function(genderElemName, value){
        let elems = document.getElementsByName(genderElemName); // NodeList 用 name 取得所有的 radio button
        if(elems.length == 0) throw new Error(`找不到 name=${genderElemName} 的 radio button`);
        
        // 如果 value 是 null, 則全部取消勾選
        if(value === null){
            Array.from(elems).forEach(elem => elem.checked = false);
            return;
        }
        
        for(let i = 0; i < elems.length; i++){
            if(elems[i].value == value){
                elems[i].checked = true;
                return;
            }
        }
    },

    /**
     * 清空下拉選單的內容
     * @param {HtmlEelement} ddl
     */
    emptyDDL : function (ddl) {
        // 判斷 ddl 是 select 元素
        if (ddl.tagName != "SELECT") throw new Error("ddl 必需是 select 元素");
        ddl.innerHTML = "";
    },
    
    /**
     * 繫結陣列資料到 select 元素, 不包含設定值
     * @param ddl
     * @param datasource
     * @param valueMember
     * @param textMember
     */
    bindDDL : function (ddl, datasource, valueMember, textMember) {
        // 判斷 ddl 是 select 元素
        if (ddl.tagName != "SELECT") throw new Error("ddl 必需是 select 元素");
        // 判斷 datasource 是陣列
        if (!Array.isArray(datasource)) throw new Error("datasource 必需是陣列");
        // valueMember, textMember 必需是字串,且不能是null
        if (typeof valueMember != "string" || valueMember == null || typeof valueMember === 'undefined') throw new Error("valueMember 必需是字串");
        if (typeof textMember != "string" || textMember == null || typeof valueMember === 'undefined') throw new Error("textMember 必需是字串");
    
        // 先清空 ddl 的內容
        ddl.innerHTML = "";
    
        datasource.forEach(function (item) {
            let option = document.createElement("option");
            option.value = item[valueMember];
            option.innerText = item[textMember];
            ddl.appendChild(option);
        });
    },
};


let TemplateHelper = {

    // 取得 template 的內容, 並回傳一個 jQuery 物件
    getTemplate : function(name){

        var templateName = "template." +  name ;

        var template = $(templateName).html();

        return $(template).clone();
    }
};