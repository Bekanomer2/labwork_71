$(document).ready(function () {
    console.log("FoodOrders JavaScript подключен");
    
    $(document).on("click", ".add-to-cart", function (e) {
        e.preventDefault();
        const dishId = $(this).attr("data-dish-id") || $(this).data("dish-id") || $(this).data("dishId");

        if (!dishId) return;

        $.ajax({
            url: "/Cart/Add",
            type: "POST",
            data: { dishId: dishId },
            success: function (response) {
                if (response.success) {
                    if (response.totalCount !== undefined) {
                        $("#cartBadge").text(response.totalCount);
                    }
                    updateCart(true); 
                }
            },
            error: function (request) {
                const response = request.responseJSON;
                if (response && response.message) {
                    alert(response.message);
                } else {
                    alert("Не удалось добавить блюдо в корзину");
                }
            }
        });
    });
    
    $(document).on("click", ".remove-from-cart", function (e) {
        e.preventDefault();
        const dishId = $(this).attr("data-dish-id") || $(this).data("dish-id") || $(this).data("dishId");

        if (!dishId) return;

        $.ajax({
            url: "/Cart/Remove",
            type: "POST",
            data: { dishId: dishId },
            success: function (response) {
                if (response.success) {
                    if (response.totalCount !== undefined) {
                        $("#cartBadge").text(response.totalCount);
                    }
                    updateCart(false);
                }
            },
            error: function (request) {
                console.log("Не удалось удалить блюдо", request.responseText);
            }
        });
    });
    
    function updateCart(openSidebar = false) {
        $.ajax({
            url: "/Cart/GetCart",
            type: "GET",
            success: function (html) {
                $("#cartContainer").html(html);

                if (openSidebar) {
                    const offcanvasEl = document.getElementById("cartOffcanvas");
                    if (offcanvasEl && typeof bootstrap !== "undefined") {
                        const bsOffcanvas = bootstrap.Offcanvas.getOrCreateInstance(offcanvasEl);
                        bsOffcanvas.show();
                    }
                }
            },
            error: function () {
                console.log("Не удалось обновить представление корзины");
            }
        });
    }
});
