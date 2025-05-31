// handle addition and subtraction order detail in page menu
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
    if (quantity <= 1) {
        $.ajax({
            url: '/Home/GetProductPartial',
            type: 'POST',
            data: {
                productId: productId
            },
            success: function (response) {
                if (response.error) {
                    console.error(response.error);
                } else {
                    $(`#product-${productId}`).html(response);
                }
            },
            error: function (xhr, status, error) {
                console.error('Có lỗi xảy ra:', error);
            }
        });
    }
    else {
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
    }
});
