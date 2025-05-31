
//$(document).ready(function () {
//    // Bắt sự kiện khi lướt trang
//    $(window).on("scroll", function () {
//        $('.container__menu').each(function () {
//            var category = $(this);
//            var offsetTop = category.offset().top;
//            var scrollPosition = $(window).scrollTop();
//            var categoryName = category.find('.label-container').text().trim();

//            // Kiểm tra nếu danh mục này ở vị trí gần trên cùng màn hình
//            if (scrollPosition >= offsetTop - 50) {
//                $('.category-item').removeClass('active');
//                $('.category-item').each(function () {
//                    if ($(this).text().trim() === categoryName) {
//                        $(this).addClass('active');
//                        // Di chuyển danh mục lên đầu
//                        var categoryNav = $('.menu__category-nav');
//                        $(this).prependTo(categoryNav);
//                    }
//                });
//            }
//        });
//    });
//});

$(".increase-btn").on('click', function (event) {
    event.preventDefault();

    var quantityInput = $(this).closest('form').find('.quantityInput');
    var quantity = parseInt(quantityInput.val());
    var productId = $(this).data('product-id');
    var orderId = $(this).closest('form').find('input[name="orderId"]').val();

    $.ajax({
        url: '/Home/Increase',
        type: 'POST',
        data: {
            quantity: quantity,
            productId: productId,
            orderId: orderId
        },
        success: function (response) {
            if (response.error) {
                console.error(response.error);
            } else {
                quantityInput.val(response.newQuantity);
            }
        },
        error: function (xhr, status, error) {
            console.error('Có lỗi xảy ra:', error);
        }
    });
});

$(".decrease-btn").on('click', function (event) {
    event.preventDefault();

    var quantityInput = $(this).closest('form').find('.quantityInput');
    var quantity = parseInt(quantityInput.val());
    var productId = $(this).data('product-id');
    var orderId = $(this).closest('form').find('input[name="orderId"]').val();

    $.ajax({
        url: '/Home/Reduce',
        type: 'POST',
        data: {
            quantity: quantity,
            productId: productId,
            orderId: orderId
        },
        success: function (response) {
            if (response.error) {
                console.error(response.error);
            } else {
                quantityInput.val(response.newQuantity);
            }
        },
        error: function (xhr, status, error) {
            console.error('Có lỗi xảy ra:', error);
        }
    });
});
