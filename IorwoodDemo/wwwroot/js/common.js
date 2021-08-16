

function commonDelete(localUrl) {
    swal({
        title: "Are you sure",
        text: "Once deleted, you will not be able to recover this elementt!",
        icon: "warning",
        dangerMode: true,
        buttons: true
    }).then((willDelete) => {
        if (willDelete) {
           
            $.ajax({
                url: localUrl,
                datatype: "json",
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                       
                        toastr.success("Poof! Your element has been deleted!");
                        dataTable.ajax.reload();
                    }
                    else {
                       
                        toastr.error("Poof! Your element has not been deleted!");
                    }
                },
                error: function (request, status, errorThrown) {
                    var errM = request.status + " : " + request.statusText + " : " + request.responseText;
                    alert(errM);
                }
            })

        } else {
            toastr.info("Your element is safe");
        }
    });
}

