//$(document).on('click', '#button-plus', function (event) {
//    let price = $("#order-detail-price").val();
//    let quantity = $("#quantity").val();
//    let totalPrice = price * quantity;
//    $()("#total-price").text("Add to order " + totalPrice.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }));
//})
//$(document).on('click', '#button-minus', function (event) {
//    let price = $("#order-detail-price").val();
//    let quantity = $("#quantity").val();
//    let totalPrice = price * quantity;
//    $()("#total-price").text("Add to order " +totalPrice.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }));
//})
$(document).on('click', '#button-plus', function (event) {
    let price = parseFloat($("#order-detail-price").val());  // cần ép kiểu nếu là input
    let quantity = parseInt($("#quantity").val());
    let totalPrice = price * quantity;

    $("#total-price").text(
        "Add to order " + totalPrice.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })
    );
});

$(document).on('click', '#button-minus', function (event) {
    let price = parseFloat($("#order-detail-price").val());
    let quantity = parseInt($("#quantity").val());
    let totalPrice = price * quantity;

    $("#total-price").text(
        "Add to order " + totalPrice.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })
    );
});
