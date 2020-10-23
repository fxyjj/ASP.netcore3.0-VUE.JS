var CryptoJS = require("crypto-js")

module.exports = function (callback, happyword) {

    var decrypt = function (happyword) {
        var bytes = CryptoJS.AES.decrypt(happyword,"secretkey123");
        var ori = bytes.toString(CryptoJS.enc.Utf8);
        return ori;
    }

    callback(null, decrypt(happyword));
}