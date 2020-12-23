var CryptoJS = require("crypto-js")

module.exports = function (callback, happyword) {

    var decrypt = function (happyword) {
        var bytes = CryptoJS.AES.decrypt(happyword,"secretkey1234567")
        var ori = bytes.toString(CryptoJS.enc.Utf8);
        return ori;
    }

    callback(null, decrypt(happyword));
}