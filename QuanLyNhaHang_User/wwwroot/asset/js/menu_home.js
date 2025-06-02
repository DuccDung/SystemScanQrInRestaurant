function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}
const userIdsignalR = getCookie("userId");

const menuHubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/HubServer", {
        withCredentials: true
    })
    .configureLogging(signalR.LogLevel.Information)
    .build();

menuHubConnection.on("UpdateOrderDetailView", function (message) {
    console.log("Received message:", message);
});


// =================================================================================
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

                    if (menuHubConnection.state === signalR.HubConnectionState.Disconnected) {
                        menuHubConnection.start().then(() => {
                            menuHubConnection.invoke("SendMessage", userIdsignalR, `has remove button subtraction & addition ID: ${productId}`)
                        })
                            .catch(err => console.error(" Hub start error:", err));
                    }
                    else {
                        menuHubConnection.invoke("SendMessage", userIdsignalR, `has remove button subtraction & addition ID: ${productId}`)
                    }
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

                    if (menuHubConnection.state === signalR.HubConnectionState.Disconnected) {
                        menuHubConnection.start().then(() => {
                            menuHubConnection.invoke("SendMessage", userIdsignalR, `has decreased quantity product ID: ${productId} xuống ${response.newQuantity}`)
                        })
                            .catch(err => console.error("❌ Hub start error:", err));
                    }
                    else {
                        menuHubConnection.invoke("SendMessage", userIdsignalR, `has decreased quantity product ID: ${productId} xuống ${response.newQuantity}`)
                    }
                }
            },
            error: function (xhr, status, error) {
                console.error('Có lỗi xảy ra:', error);
            }
        });
    }
});
