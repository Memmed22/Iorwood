var dataTable;

$(function () {
    loadData();

  
   
});

function loadData() {

    dataTable = $("#DT_Load").DataTable({
        "ajax": {
            "url": "/admin/product/getAll",
            "type": "get",
            "datatype": "json"
        },
        columns: [
            { "data": "name", "width": "20%" },
            { "data": "category.name", "width": "20%" },
            { "data": "unit.name", "width": "15%" },
            { "data": "price", "width": "15%" },
            
            {
                "data": { id: "id", active: "active" },
                "render": function (data) {
                    if (data.active) {
                        return `<div class="text-center" >
                        <a href="/Admin/product/upsert?id=${data.id}" class="btn btn-success text-white" style="cursor:pointer; width:70px;">
                                <i class="fas fa-edit"></i>
                                </a>
<a class="btn btn-warning text-white btnActivity" id='${data.id}' style="cursor:pointer; width:70px;" command="deactive" >
                               <i class="fas fa-eye-slash iconActivity"></i>
</a> <a class="btn btn-danger text-white" style="cursor:pointer; width:70px;" onclick=commonDelete('/admin/product/ondelete/'+${data.id})>
                                <i class="fas fa-trash"></i>
                                </a>


        </div>` }

                    return `<div class="text-center" >
                        <a href="/Admin/product/upsert?id=${data.id}" class="btn btn-success text-white" style="cursor:pointer; width:70px;">
                                <i class="fas fa-edit"></i>
                                </a>
<a class="btn btn-primary text-white btnActivity"  id='${data.id}' style="cursor:pointer; width:70px;" command="active" >
                               <i class="fas fa-eye iconActivity"></i>
</a> <a class="btn btn-danger text-white " style="cursor:pointer; width:70px;" onclick=commonDelete('/admin/product/ondelete/'+${data.id})>
                                <i class="fas fa-trash"></i>
                                </a>


        </div>`



                }, "width": "30%"
            }
        ], "language": { "emptydata": "No data has found." }, "width": "100%",
        "initComplete": function (data) {

            // $("#iconActivity").removeClass("fas fa-ban");
            //$("#iconActivity").addClass("fas fa-external-link-square-alt");
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
