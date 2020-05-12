window.jsFunctions = {
    initializeCarousel: function (id) {
        min = 5000;
        max = 10000;
        var time = Math.floor(Math.random() * (max - min)) + min;

        $('#' + id).carousel({ interval: time });
    }
}