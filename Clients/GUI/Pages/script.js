var modal = document.getElementById("my_modal");
var btn = document.getElementById("btn_modal_window");
var span = document.getElementsByClassName("close")[0];


btn.onclick = function () {
    modal.style.display = "block";
}

modal.onclick = function () {
    modal.style.display = "none";
}

//клик вокруг модального окна
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}
