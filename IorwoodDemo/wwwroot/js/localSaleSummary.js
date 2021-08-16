var totalAmount = 0;
var storedArray;
var postOrderDetailList = [];
$(function () {
   
    storedArray = JSON.parse(sessionStorage.getItem("items"));//no brackets 
    loadOrder();
})

function backToSale() {
 
    sessionStorage.removeItem('items');
}

function loadOrder() {
  
    //var storedArray = JSON.parse(sessionStorage.getItem("items"));

    if (storedArray.length > 0)
    {
        $("#btnPlaceOrder").removeClass("disabled");
    }
    else
        alert("sahin");
    

    var totalAmountTemp = '<li id="liTotalAmoutTemplate" style="display:none" class=" list-group-item d-flex justify-content-between bg-light">' +
        $("#totalAmountTemp").html() + '</li>';

    $("#orderListContainer").append(totalAmountTemp);

   
    $.each(storedArray, function (key, value) {
        var rawOrderLisTemp = $("#orderListTemp").html();
        var dataSP = value.data.split(',');
        var productName = dataSP[2];
    var productPrice = dataSP[1];
        var productQuantity = dataSP[0];

        postOrderDetailList.push(productQuantity + "," + productPrice + "," + productName + "," + value.id);

        rawOrderLisTemp = rawOrderLisTemp.replace("QuantityPlace", productQuantity).
            replace("PricePlace", productPrice).replace("ProductNamePlace", productName);

 var orderListTemp = '<li id="liOrderListTemplate" class="collapse list-group-item d-flex justify-content-between collapse">' +
     rawOrderLisTemp + '</li>';
        $(orderListTemp).insertBefore("#liTotalAmoutTemplate");

        totalAmount += (productPrice * productQuantity);
    })

    $("#totalAmountId").text("₾ " + totalAmount);
   }

var postArray = [];

function placeOrder() {

    if (storedArray.length == 0) {
        return false;

    }

    var FullName = $("#txtName").val() == "" ? "Local Sale Process" : $("#txtName").val();
    var Adress = $("#txtAddress").val() == "" ? "Local Sale Process" : $("#txtAddress").val();
    var TelNo = $("#txtPhoneNumber").val() == "" ? "Local Sale Process" : $("#txtPhoneNumber").val();
    var Comment = $("#txtComment").val() == "" ? "Local Sale Process" : $("#txtComment").val();

    var postOrderString = FullName + "," + Adress + "," + TelNo + "," + Comment + "," + totalAmount;


    var postData = { values: postArray };
    
    $.ajax({
        type: "POST",
        async:false,
        url: "/admin/order/localSaleSubmitted",
        data: { postOrderString: postOrderString, postOrderDetailList: postOrderDetailList },
        traditional: true,
        dataType: "json",

        success: function (data) {
            $("#secondContainer").addClass("collapse");
            $("#orderSubmittedWall").removeClass("collapse");
            $("#orderID").text(data.message);
            sessionStorage.removeItem('items');
        },
        error: function (data) {
            if (data.success == false) {
                return false;
            }
        }
        
    });

   
    
}

