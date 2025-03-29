document.addEventListener("DOMContentLoaded", function () {
    var modals = document.querySelectorAll(".modal");

    modals.forEach(function (modal) {
        modal.addEventListener("hidden.bs.modal", function () {
            var iframe = modal.querySelector("iframe");
            if (iframe) {
                var src = iframe.src;
                iframe.src = ""; // Xóa src để dừng video
                iframe.src = src; // Gán lại src để tránh lỗi trắng màn hình
            }
        });
    });
});