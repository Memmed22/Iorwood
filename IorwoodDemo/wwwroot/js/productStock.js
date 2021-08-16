var dataTable;

$(function () {
    var pathname = window.location.href;
    var isImport = false;
    if (pathname.includes("isImport")) {
        isImport = true;
    }
    loadData(isImport);
});

function loadData(isImport) {
    
    dataTable = $("#DT_Load").DataTable({
        "ajax": {
            "url": "/admin/product/getAll",
            "type": "get",
            "datatype": "json"
        },
        columns: [
            { "data": "name", "width": "20%" },
            { "data": "category.name", "width": "20%" },
            { "data": "unit.name", "width": "15%", " background-color":"#fccfcf" },
            { "data": "stockQuantity", "width": "15%" },
            { "data": "stockMinQuantity", "width": "10%" },
            {
                "data":  "id",
                "render": function (data) {
                    if (!isImport) {
                        return `<div class="text-center" >
                        <a href = "/Admin/product/UpsertProductStock?id=${data}"  class="btn btnDene btn-success text-white" style="cursor:pointer; width:70px;">
                                <i class="fas fa-edit"></i>
                                </a>   </div>` }

                   else {
                        return `<div class="text-center" >
                        <a onclick=importPopUp(${data}) class="btn btnDene btn-success text-white" style="cursor:pointer; width:70px;">
                                <i class="fas fa-edit"></i>
                                </a>   </div>` }
                   

                    


                }, "width": "20%"
            }
        ], "language": { "emptydata": "No data has found." }, "width": "100%",
        "initComplete": function (data) {

        }


    });




}


function importPopUp(id) {
   
    swal('Import quantity:', {
        content: 'input',
    })
        .then((value) => {
            if (Number.isInteger(parseInt(value))) {
               
                $.ajax({
                    url: '/admin/product/importStock',
                    data: { id: id, quantity: value },
                    datatype: 'json',
                    type: 'post',
                    success: function (data) {
                        if (data.success) {
                            dataTable.ajax.reload();
                        }
                        else {
                            swal(data.message);
                        }
                    }
                })

            }
            else {
                swal(`Please input numeric value: ${value}`);
                return false;
                
            }
            
        });
}

