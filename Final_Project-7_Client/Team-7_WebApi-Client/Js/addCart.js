
   $(document).ready(function () {

        $("#add2cart").each(function () {
            $(this).on("click", function () {
                var self = $(this);

                var productId = self.attr("data-id");

                $.get("/Cart/AddItem?productId=" + productId,
                    null,
                    function (result) {
                        alert("已加入購物車");
                    });
            });
        });
   })
