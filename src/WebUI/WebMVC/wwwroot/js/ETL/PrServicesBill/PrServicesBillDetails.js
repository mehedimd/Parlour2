$(document.body).on("change", "#MPrCategoryId", function () {
    var categoryId = $(this).val();

    if (categoryId > 0) {
        _dropdownManager.getServiceByCategoryId(categoryId, "#MPrServiceId", null, null);
    }
});

$(document.body).on("change", "#MPrServiceId", function () {
    var serviceId = $(this).val();

    if (serviceId > 0) {
        const url = `${API}PrServiceInfo/GetServiceInfoById/${serviceId}`;
        $.get(url, function (rData) {
            if (rData) {
                console.log("service-info: ", rData);
                $("#MQty").val(1);
                $("#MRate").val(rData.rate);

                var amount = parseFloat($("#MRate").val()) * parseFloat($("#MQty").val());
                $("#MAmount").val(amount);

            } else {
                console.log("No Service Info Found...!");
            }
        })
    }
});

$(document.body).on("click", "#ServiceAddBtn", function () {
    const serviceBillId = parseInt($("#MPrServiceBillId").val());
    const serviceId = parseInt($("#MPrServiceId").val());
    const rate = parseFloat($("#MRate").val());
    const qty = parseFloat($("#MQty").val());
    const amount = parseFloat($("#MAmount").val());


    if (serviceBillId > 0 && serviceId > 0 && rate > 0 && qty > 0) {
        const url = API + "PrServicesBill/BillServiceAdd";

        const params = {
            serviceBillId: serviceBillId,
            serviceId: serviceId,
            rate: rate,
            qty: qty,
            amount: amount
        };

        $.post(url, params, function (rData) {
            if (rData == true) {
                successMsg("Service Add Successful");
                $("#serviceAddModal").modal('hide');
                clearServiceForm();

                setTimeout(() => {
                    window.location.href = API + "PrServicesBill/Details/" + serviceBillId;
                }, 3000);
            } else {
                failedMsg("Service Entry Failed");
            }
        }).fail(function () {
            failedMsg("Service Entry Failed");
        })
    } else {
        failedMsg("Information Is Not Correct..!");
    }
})

function clearServiceForm() {
    $("#MPrServiceId").val("").trigger("change");
    $("#MRate").val("");
    $("#MQty").val("");
    $("#MAmount").val("");
}


// Payment Add

$(document.body).on("click", "#PaymentAddBtn", function () {
    const serviceBillId = parseInt($("#P_ServiceBillId").val());
    const paidDateStr = $("#PaidDateStr").val();
    const payMode = $("#P_PayMode").val();
    const amount = parseFloat($("#P_Amount").val());
    const remarks = $("#P_Remarkst").val();


    if (serviceBillId > 0 && amount > 0) {
        const url = API + "PrServicesBill/BillPaymentEntry";

        const params = {
            serviceBillId: serviceBillId,
            tranDateStr: paidDateStr,
            payMode: payMode,
            amount: amount,
            remarks: remarks
        };

        $.post(url, params, function (rData) {
            if (rData == true) {
                successMsg("Payment Entry Successful");
                $("#paymentAddModal").modal('hide');
                clearPaymentForm();

                setTimeout(() => {
                    window.location.href = API + "PrServicesBill/Details/" + serviceBillId;
                }, 2000);
            } else {
                failedMsg("Payment Entry Failed");
            }
        }).fail(function () {
            failedMsg("Payment Entry Failed");
        })
    } else {
        failedMsg("Payment Information Is Not Correct..!");
    }
})

function clearPaymentForm() {
    $("#P_PayMode").val("").trigger("change");
    $("#P_Amount").val("");
    $("#P_Remarkst").val("");
}