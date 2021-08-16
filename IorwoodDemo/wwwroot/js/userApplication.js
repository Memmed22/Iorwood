var dataTable;

$(function () {
    loadList();
    
});



function loadList() {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/admin/userApplication/getAll",
            "type": "GET",
            "datatype": "json"
        }
        ,
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "email", "width": "25%" },
            { "data": "phoneNumber", "width": "25%" },
            {
                "data": { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockoutEnd = new Date(data.lockoutEnd).getTime();
                    if (today < lockoutEnd) {
                        return `
<div class="text-center">
            <a class="btn btn-danger text-white" styke ="cursor:pointer; width:100px" onclick=LockUnLock('${data.id}')><i class="fas fa-lock-open"></i>  Unlock</a>
</div>`}
                    else {
                        return `
<div class="text-center">
            <a class="btn btn-success text-white" style="cursor:pointer; width:100px;" onclick=LockUnLock('${data.id}')> <i class="fas fa-lock"></i>  Lock</a>
</div>`}
                }
                , "width": "25%"
            }
        ]
    });
}
