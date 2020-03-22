window.CookieManager = {
    // Writes data to cookie which expires after 'minutes' minutes
    WriteCookie: function (cookieName, cookieValue, minutes) {
        var expires;
        if (minutes) {
            var date = new Date();
            date.setTime(date.getTime() + (minutes * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        }
        else {
            expires = "";
        }
        document.cookie = cookieName + "=" + cookieValue + expires + "; path=/";
    },

    // Reads data from cookie
    ReadCookie: function (cookieName) {
        var cookiestring = RegExp(cookieName + "=[^;]+").exec(document.cookie);
        return decodeURIComponent(!!cookiestring ? cookiestring.toString().replace(/^[^=]+./, "") : "");
    }
}
