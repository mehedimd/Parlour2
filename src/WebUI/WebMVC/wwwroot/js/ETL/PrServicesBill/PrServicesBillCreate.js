
$(document).ready(() => {
    categoryWiseServiceData();
})

let LiaServiceList = [];

$(document.body).on("change", "#PrCategoryId", function () {
    categoryWiseServiceData();
});

function categoryWiseServiceData() {
    const categoryId = $("#PrCategoryId").val();

    const url = `${API}PrServicesBill/GetServiceByCategoryId?categoryId=${categoryId}`;
    $.get(url, function (rData) {
        if (rData) {
            console.log("service-list: ", rData);
            LiaServiceList = rData;

            loadDummyServiceData();
        } else {
            console.log("No Service List Found...!");
        }
    })
}


// load all service
function loadDummyServiceData() {

    let dummyDataDiv = ""
    $.each(LiaServiceList, (index, value) => {
        dummyDataDiv += `<div class="col-md-2 mb-1 p-0" onclick="AddLiaService(${value.id})">
                            <div class="card" style="background-color:darksalmon;min-height:120px">
                                <div class="card-body text-center">
                                    <h5>${value.serviceName}</h5>
                                    <p>${value.rate} Tk.</p>
                                </div>
                            </div>
                        </div>`

    })
    $("#liasServiceId").html(dummyDataDiv);
}

$(document.body).on("change", "#CustomerMobile", function () {
    getCustomerInfo();
});

function getCustomerInfo() {
    const customerMobile = $("#CustomerMobile").val();

    const url = `${API}PrCustomer/GetCustomerByMobile?mobile=${customerMobile}`;
    $.get(url, function (rData) {
        if (rData != null) {
            console.log("customer-info: ", rData);
            setCustomerInfo(rData);
        } else {
            console.log("customer-info not found");
        }
    })
}

function setCustomerInfo(data) {
    $("#CustomerId").val(data.id);
    $("#CustomerName").val(data.customerName);
    $("#CustomerMobile").val(data.mobile);
    $("#CustomerEmail").val(data.email);
    $("#CustomerAddress").val(data.address);
}

// all selected item array
const allSelectedItemArray = [];

// add new service item 
function AddLiaService(id) {

    let selectedItem = LiaServiceList.find((element) => element.id == id);

    let isExisted = allSelectedItemArray.find((item) => item.serviceId == id);


    if (isExisted != null) {
        let qty = "#quantity_" + id;
        $(qty).val(Number($(qty).val()) + 1)
        updateAmount(id);
    }
    else {
        let PrServiceBillDetailsObj = {
            serviceId: id,
            serviceName: selectedItem.serviceName,
            rate: selectedItem.rate,
            quantity: 1,
            amount: selectedItem.rate
        }
        console.log(PrServiceBillDetailsObj)
        allSelectedItemArray.push(PrServiceBillDetailsObj);

    }
    let tr = "";
    $.each(allSelectedItemArray, (index, value) => {
        tr += `<tr>
                <td>
                    <input type='hidden' name='PrServicesBillDetails[${index}].ServiceId' value='${value.serviceId}' />
                    <b>${value.serviceName}</b>
                </td>
                <td>
                    <input class='form-control text-center' type='number' min='1' name='PrServicesBillDetails[${index}].Qty' value='${value.quantity}' id='quantity_${value.serviceId}' oninput='updateAmount(${value.serviceId})' />
                </td>
                <td>
                    <input class="form-control text-end" type="number" name='PrServicesBillDetails[${index}].Rate' value="${value.rate}" id="rate_${value.serviceId}" oninput = "updateAmount(${value.serviceId})" />
                </td>
                <td>
                    <input class="form-control text-end" type="number" name='PrServicesBillDetails[${index}].Amount' value="${value.amount}" id="amount_${value.serviceId}" readonly />
                </td>
                <td class="text-center">
                    <a style="font-size:18px;" href="#" title='Remove' onclick="RemoveServiceItem(this, ${value.serviceId})"><i class="fa fa-times"></i></a>
                </td>
            </tr>`
        $("#serviceItemTableId").html(tr);
    })

    // update total amount
    updateTotalAmount()

    console.log(allSelectedItemArray)
}

// item amount update

function updateAmount(id) {
    let qty = "#quantity_" + id;
    let rate = "#rate_" + id;
    let amount = Number($(qty).val()) * Number($(rate).val());

    let updatableItem = allSelectedItemArray.find((value, index) => value.serviceId == id);
    updatableItem.quantity = $(qty).val();
    updatableItem.rate = $(rate).val();
    updatableItem.amount = amount;

    let amountId = "#amount_" + id;
    $(amountId).val(amount);

    // update total amount
    updateTotalAmount()
    console.log(allSelectedItemArray)
}


// remove service item

function RemoveServiceItem(e, id) {
    $(e).closest('tr').remove()
    allSelectedItemArray.forEach((item, index) => {
        if (item.serviceId == id) {
            allSelectedItemArray.splice(index, 1);
        }
    })

    // update total amount
    updateTotalAmount()
    console.log(allSelectedItemArray)
}

// Total Amount Update
function updateTotalAmount() {
    let totalAmount = allSelectedItemArray.reduce((prev, curr) => {
        return prev + curr.amount;
    }, 0);

    $("#TotalBill").val(totalAmount);

    calculateNetAmount();
}

$(document.body).on("change", "#VAT", function () {
    calculateNetAmount();
});

$(document.body).on("change", "#Tax", function () {
    calculateNetAmount();
});

$(document.body).on("change", "#discount-percent", function () {
    calculateDiscount();
});

// Calculate Discount
function calculateDiscount() {
    const totalBill = parseFloat($("#TotalBill").val());
    const discountPercent = parseFloat($("#discount-percent").val());

    if (discountPercent > 0) {
        const discountAmount = calculatePercentage(totalBill, discountPercent);
        $("#Discount").val(discountAmount);
    }

    calculateNetAmount();
}

function calculatePercentage(value, percentage) {
    return (value * percentage) / 100;
}

function calculateNetAmount() {
    const totalAmount = parseFloat($("#TotalBill").val());
    const vat = parseFloat($("#VAT").val());
    const tax = parseFloat($("#Tax").val());
    const discount = parseFloat($("#Discount").val());

    const netTotal = (totalAmount + vat + tax) - (discount);

    $("#NetAmount").val(netTotal);
}


$(document.body).on("click", "#BillSubmitBtn", function () {

    const customerMobile = $("#CustomerMobile").val();
    const customerName = $("#CustomerName").val();

    if (hasAnyError(customerMobile)) {
        return failedMsg("Please Enter Customer Mobile..!");
    }

    if (hasAnyError(customerName)) {
        return failedMsg("Please Enter Customer Name..!");
    }

    if (!(allSelectedItemArray.length > 0)) {
        return failedMsg("No Item Found To Service..!");
    }

    if (!hasAnyError(customerMobile) && !hasAnyError(customerName)) {
        $("#ServiceBillForm").submit();
    } else {
        failedMsg("Somthing Went Wrong...!");
    }
});