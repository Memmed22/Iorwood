var dataTable;

$(function () {
    var pathname = window.location.href;
    var action;
    if (pathname.includes('complated'))
        action = "complated";
    else if (pathname.includes('cancelled'))
        action = "cancelled";
    else if (pathname.includes('inprocess'))
        action = "inprocess";
    else
        action = "all";
    loadData(action);
});


function loadData(action) {
   

    dataTable = $("#DT_Load").DataTable({
        "ajax": {
            "url": "/admin/order/getAll?status=" + action ,
            "type": "get",
            "datatype": "json"
        },
        columns: [
            { "data": "id", "width": "10%" },
            { "data": "fullName", "width": "20%" },
            { "data": "orderDate", "width": "20%" },
            { "data": "status", "width": "20%" },
            { "data": "totalAmount", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    
                    return `<div class="text-center" >
                        <a href="/Admin/order/details/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="fas fa-info-circle"></i>  Details
                                </a>

                           
        </div>`;
                }, "width": "20%"
            }
        ], "language": { "emptydata": "No data has found." }, "width": "100%"


    });
}
