$(function () {

    $(".btnDelete").on("click", DeleteProduct);

    function DeleteProduct()
    {
        var id = this.id;
       
        $.ajax({
            method: "POST",
            url: "/Product/DeleteProduct",
            data: {
                Id: id
            }
        }).done(function () {
            $("#" + id).remove();
        });
    }

});


