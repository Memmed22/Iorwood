var dataTable;

$(function () {
    loadData();
});

function loadData() {

    dataTable = $("#DT_Load").DataTable({
        "ajax": {
            "url": "/admin/extra/getAll",
            "type": "get",
            "datatype": "json"
        },
        columns: [
            { "data": "name", "width": "70%" },
           
            {
                "data": "id",
                "render": function (data) {
                    
                    return `<div class="text-center" >
                        <a href="/Admin/extra/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="fas fa-edit"></i> Edit
                                </a>

                            <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=commonDelete('/admin/extra/ondelete/'+${data})>
                                <i class="fas fa-trash"></i> Delete

                                </a>
        </div>`;
                }, "width": "30%"
            }
        ], "language": { "emptydata": "No data has found." }, "width": "100%"


    });
}
