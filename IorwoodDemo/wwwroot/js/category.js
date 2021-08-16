var dataTable;

$(function () {
  

    loadData();
    //loadAllData();
});

//new Version
function loadAllData() {
    $.ajax({
        url: "/admin/category/get",
        type: "get",
        datatype: "json",
        success: function (data) {
            loadMyTable(data);
        }
    })
    
    //$.getJSON("/admin/category/get").done(function (data) {
    //    loadMyTable(data.data);

    //}).fail(function () { alert("error")})
}

function loadMyTable(dt) {
    dataTable = $("#DT_Load").DataTable({
        data: dt,
        columns: [
            { data: "name", "width": "35%" },
            { data: "orderList", "width": "35%" },
            {
                data: "id", "width": "30%", "render": function (id) {
                    return `<div class="text-center" >
                        <a href="/Admin/category/upsert?id=${id}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="fas fa-edit"></i> Edit
                                </a>

                            <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=Delete1('/admin/category/ondelete/'+${id})>
                                <i class="fas fa-trash"></i> Delete
                                </a>
        </div>`;} }
        ], "language": { "emptydata": "Veri yok" }, "width": "100%"
    });
}

//OldVersion
function loadData() {

    dataTable = $("#DT_Load").DataTable({
        "ajax": {
            "url": "/admin/category/get",
            "type": "get",
            "datatype": "json"
        },
        columns: [
            { "data": "name", "width": "35%" },
            { "data": "orderList", "width": "35%" },
            {
                "data": "id",
                "render": function (data) {
                    
                    return `<div class="text-center" >
                        <a href="/Admin/category/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="fas fa-edit"></i> Edit
                                </a>

                            <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=commonDelete('/admin/category/ondelete/'+${data})>
                                <i class="fas fa-trash"></i> Delete

                                </a>
        </div>`;
                }, "width": "30%"
            }
        ], "language": { "emptydata": "No data has found." }, "width": "100%"


    });
}

