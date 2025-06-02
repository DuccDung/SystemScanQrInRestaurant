    // Khôi phục vị trí scroll khi quay lại
    document.addEventListener('DOMContentLoaded', function () {
        const scrollY = localStorage.getItem('menuScrollY');
    if (scrollY !== null) {
        window.scrollTo(0, parseInt(scrollY));
    localStorage.removeItem('menuScrollY');
        }
    });

    // Lưu vị trí scroll trước khi rời trang
    window.addEventListener('beforeunload', function () {
        localStorage.setItem('menuScrollY', window.scrollY);
    });
