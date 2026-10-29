// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function(){
    console.log("JavaScript подключен");
    
    $(document).on("click", ".add-to-cart", function(){
        const dishId= $(this).data("dishId");
        
        $.ajax({
            url:"Cart/Add",
            type:"POST",
            data:{dishId:dishId},
            success:function(response){
                if(response.success){
                    updateCart();
                };
            },
            error:function(request){
                const response=request.responseJSON;
                
                if(response && response.message){
                    alert(response.message);
                } else{
                    alert("Не удалось добавить блюдо");
                }                               
            }
        });
    });
    function updateCart(){
        $.ajax({
            url:"Cart/GetCart",
            type:"GET",
            success:function(html){
                $("#cartContainer").html(html);
            },
            error:function(){
                console.log("Не удалось обновить корзину");
            }
        });
    }
    $(document).on("click", ".remove-from-cart", function(){
        const dishId= $(this).data("dishId");
        $.ajax({
            url:"Cart/Remove",
            type:"POST",
            data:{dishId:dishId},
            
            success:function(response){
                updateCart();
            },
            error:function(request){
                console.log("Не удалось удалить блюдо");
                console.log(request.responseText);
            }
        });
    });

});