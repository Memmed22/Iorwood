var dataTable;
var orderList = [];
var jsonFile = null;
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
            { "data": "name", "width": "25%" },
            { "data": "category.name", "width": "25%"},
            { "data": "price", "width": "20%" },
            { "data": "stockQuantity", "with": "20%" },
            {
                "data": "id", "render": function (data) {
                    return `<div class="text-center" >
                        <a onclick=appendToOrder(${data}) class="btn text-black" style="cursor:pointer; width:50px;">
                                <i class="fas fa-shopping-cart"></i>
                                </a>  </div>`;
                }, "width": "10%"
            }
        ]
    })
}

function calculateTotalAmount() {
    var totalAmount = 0;
    $.each(orderList, function (index, value) {
        var res = value.data.split(",");
        var itemAmount = res[0] * res[1];
        totalAmount += itemAmount;
        
        
    });
    $("#spnOrderTotal").text(totalAmount);
}


function showPopup(data) {
    
    $("#localSaleOrderPopup").dialog({
        title: data.name,
        dialogClass: "no-close",
        buttons: [
            {
                "text": "Cancel",
                click: function () {
                    $(this).dialog("close");
                }
            },
            {
                text: "OK",
               
                click: function () {
                    var quantityLast = document.getElementById("inputQuantity").value;
                    var priceLast = document.getElementById("inputPrice").value;


                    orderList.push({
                        id: data.id,
                        data: quantityLast.toString() + "," + priceLast.toString() + "," + data.name.toString()
                    });
                    JSON.stringify(orderList);


                    $(this).dialog("close");

                    var orderListTemplate = $("#orderTemplate").html().replace("collapse", "");
                    $(".card-body").removeClass("collapse")
                    $(".dv-Summarize").removeClass("collapse");


                    orderListTemplate = orderListTemplate.replace("productNamePlace", data.name);
                    orderListTemplate = orderListTemplate.replace("imagePlace", data.image);
                    orderListTemplate = orderListTemplate.replace("pricePlace", priceLast);
                    orderListTemplate = orderListTemplate.replace("quantityPlace", quantityLast);
                    orderListTemplate = orderListTemplate.replace("sumPlace", (quantityLast * priceLast));
                    orderListTemplate = orderListTemplate.replace("element", data.id);

                

         
                    calculateTotalAmount();

                    $(orderListTemplate).insertBefore($(".card-body"));


                    $("#spnProductName").attr("id", "spnProductName-" + data.id);
                    $("#priceId").attr("id", "priceId-" + data.id);
                    $("#quantityId").attr("id", "quantityId-" + data.id);
                    $("#itemSumId").attr("id", "itemSumId-" + data.id);
                }

            }
           
        ]
    });
}


function appendToOrder(id) {
    var price = null;
    var productName = null;
    $.ajax({
        url: "/admin/product/getById/"+id,
        type: "get",
        dataType: "json",
        success: function (data) {
            price = data.data.price;
            productName = data.data.name;
            document.getElementById("inputPrice").value = price;
            document.getElementById("inputQuantity").value = "";
            showPopup(data.data);
        }
    });

 
    
    
}


function editOrderItem(id) {

    var currentPrice = $("#priceId-" + id).text();
    var currentQuantity = $("#quantityId-" + id).text();
    var currentProductName = $("#spnProductName-" + id).text();

    document.getElementById("inputPrice").value = currentPrice;
    $("#inputQuantity").val(parseInt(currentQuantity))
    
    $("#localSaleOrderPopup").dialog({
        title: currentProductName,
        dialogClass: "no-close",
        buttons: [
            {
                "text": "Cancel",
                click: function () {
                    $(this).dialog("close");
                }
            },
            {
                text: "OK",

                click: function () {
                    currentPrice = document.getElementById("inputPrice").value 
                    currentQuantity = document.getElementById("inputQuantity").value 
                    
                    $("#priceId-" + id).text(currentPrice);
                    $("#quantityId-" + id).text(currentQuantity);

                    $("#itemSumId-" + id).text(currentQuantity * currentPrice);

                    var objIndex = orderList.findIndex(p => p.id == id);

                    if (objIndex != -1) {

                     
                        var orderListText = orderList[objIndex];
                       
                        var orderListArr = orderListText.data.split(',');
                        var productNameArr = orderListArr[2];
                        orderList[objIndex].data = currentQuantity + "," + currentPrice + "," + productNameArr;
                    }
                 
                    calculateTotalAmount();

                    $(this).dialog("close");
                }

            }

        ]
    });

    return false;
}

function deleteOrderItem() {
    //var storedArray = JSON.parse(sessionStorage.getItem("items"));//no brackets
    //alert(storedArray[0].data);




    calculateTotalAmount();

    return false;
}

function summary() {
    
    window.sessionStorage.setItem("items", JSON.stringify(orderList));
    return true
}