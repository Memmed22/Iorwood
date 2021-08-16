var dataTable;

$(function () {
    loadData();
    initialize();
})

function loadData() {
    dataTable = $("#DT_Load").DataTable({
        "ajax": {
            "url": "/admin/currentMovement/GetUnClearedMovement",
            "type": "get",
            "datatype": "json"
        },
        columns: [
            { "data": "user", "width": "10%" },
            { "data": "way", "width": "7%" },
            { "data": "type", "width": "25%" },
            { "data": "description", "with": "36%" },
            { "data": "date", "with": "15%" },
            { "data": "sum", "with": "7%" },
          
        ]
    })
}


function initialize() {
    var netCash = parseInt($('#inputInflow').val()) - parseInt($('#inputOutflow').val());
    $("#spnCashLeave").text(netCash);
}

$('#inputLeaveIn').keyup(function (val) {
    if ($('#inputLeaveIn').val() == "") {
        initialize();
    }
    else {
        var netCash = parseInt($('#inputInflow').val()) - parseInt($('#inputOutflow').val());

        $("#spnCashLeave").text(netCash - parseInt($('#inputLeaveIn').val()));
    }
});

