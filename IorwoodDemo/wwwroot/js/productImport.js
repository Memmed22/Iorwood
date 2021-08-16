var dataTable;

$(function () {
    loadData();

  
   
});

function loadData() {

    dataTable = $("#DT_Load").DataTable({
        "ajax": {
            "url": "/admin/product/GetAllStock",
            "type": "get",
            "datatype": "json"
        },
        columns: [
            { "data": "name", "width": "20%" },
            { "data": "category.name", "width": "20%" },
            { "data": "price", "width": "10%" },
            { "data": "unit.name", "width": "15%" },
            { "data": "stockQuantity", "width": "15%" },
            
            {
                "data":  "id",
                "render": function (data) {
                   
                        return `<div class="text-center" >
                        <a href="/Admin/product/UpsertProductStock?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:70px;">
                                <i class="fas fa-edit"></i>
                                </a>



        </div>` 

                    


                }, "width": "20%"
            }
        ], "language": { "emptydata": "No data has found." }, "width": "100%",
        "initComplete": function (data) {

        }


    });
}

$(document).on("click", ".btnActivity", function () {
    var command = $(this).attr('command');
    var elId = this.id;

    if (command == 'active') {
        $.ajax({
            url: "/admin/product/active/" + elId,
            type: "get",
            datatype: "json",
            success: function (data) {
                if (data.success) {
                    
                    var currentElement = $("#" + elId);
                    currentElement.removeClass("btn-primary").addClass("btn-warning");
                    currentElement.find("i").removeClass('fa-eye').addClass('fa-eye-slash');
                    currentElement.attr("command", "deactive")
                }
            }
        })
    }

    else {
        $.ajax({
            url: "/admin/product/deactive/" + elId,
            type: "get",
            datatype: "json",
            success: function (data) {

                if (data.success) {
                    var currentElement = $("#" + elId);
                    currentElement.removeClass("btn-warning").addClass("btn-primary");
                    currentElement.find("i").removeClass('fa-eye-slash').addClass('fa-eye');
                    currentElement.attr("command", "active")
                }
            }
        })
    }
});
