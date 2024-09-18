$(document).ready(function () {
    search();

    // for edit/update
    $("#updateBtn").hide();
    $("#saveBtn").show();
})

function search() {
    const searchVm = {};

    if ($.fn.DataTable.isDataTable("#ServiceInfoSearchTable")) {
        const table = $("#ServiceInfoSearchTable").DataTable();
        table.destroy();
    }

    var params = "";
    if (!hasAnyError(searchVm)) {
        params = { SearchModel: searchVm };
    }

    const oTable = $("#ServiceInfoSearchTable").DataTable({
        "aLengthMenu": DataTable.lengthMenu,
        "iDisplayLength": DataTable.displayLength,
        "processing": DataTable.processing,
        "serverSide": DataTable.serverSide,
        "ordering": false,

        "ajax": {
            url: API + "PrServiceInfo/Search",
            type: "POST",
            data: params,
        }, error(e) {
            failedMsg(e);
        },

        "columns": [
            { "data": "serialNo" },
            { "data": "photoUrl" },
            { "data": "serviceName" },
            { "data": "serviceCode" },
            { "data": "description" },
            { "data": "rate" },
            { "data": "categoryName" },
            { "data": "isEnable" },
            {
                "render": function (data, type, item) {

                    showTotalRowCountSpanInDataTable("ServiceInfoSearchTable", oTable);

                    let editButton = `<a class='text-success' style="cursor:pointer" onclick="Edit(${item.id})"><i class="fa fa-edit"></i></a>`;
                    let deleteButton = `<a class='text-danger ms-3' style="cursor:pointer" onclick="DeleteService(${item.id})" title='Delete'><i class="fa fa-trash"></i></a>`;

                    return `<div style="font-size: 18px;"><div>` + editButton + deleteButton + `</div></div>`;
                }
            }
        ]
    });

    addTotalRowCountSpanInDataTable("ServiceInfoSearchTable");
}

// Create (post)
function CreateNew() {
    console.log(ModelGet())
    if (ValidateField()) {
        $.ajax({
            method: 'post',
            url: API + "PrServiceInfo/Create",
            data: ModelGet(),
            success: function (res) {
                search();
                ClearAllField()
                successMsg("Service Successfully Created")
            },
            error: e => {
                failedMsg(e);
            }
        })
    }
}


// Edit (get)
function Edit(id) {
    $.ajax({
        method: 'get',
        url: API + "PrServiceInfo/Edit/" + id,
        success: function (res) {

            $("#Id").val(res.id);
            $("#ServiceName").val(res.serviceName);
            $("#ServiceCode").val(res.serviceCode);
            $("#Description").val(res.description);
            $("#Rate").val(res.rate);
            $("#CategoryId").val(res.categoryId).trigger(update);
            $("#IsEnable").val(res.isEnable);
            $("#IsEnable").prop("checked", res.isEnable);

            DefaultErrorMessage();
            $("#updateBtn").show();
            $("#saveBtn").hide();
        },
        error: e => {
            failedMsg(e);
        }
    })
}
// Update (post)
function UpdateService() {

    if (ValidateField()) {
        $.ajax({
            method: 'post',
            url: API + "PrServiceInfo/Edit",
            data: ModelGet(),
            success: function (res) {

                search();
                ClearAllField()
                successMsg("Service Successfully Updated")
                // for edit/update
                $("#updateBtn").hide();
                $("#saveBtn").show();
            },
            error: e => {
                failedMsg(e);
            }
        })
    }
}

// Delete
function DeleteService(id) {
    $.ajax({
        method: 'get',
        url: API + "PrServiceInfo/Delete/" + id,
        success: function (res) {
            search();
            successMsg("Service Successfully Deleted")
        },
        error: e => {
            failedMsg(e);
        }
    })
}

// get model
function ModelGet() {
    let modelId = $("#Id").val();
    let serviceName = $("#ServiceName").val().trim();
    let serviceCode = $("#ServiceCode").val().trim();
    let rate = $("#Rate").val();
    let categoryId = $("#CategoryId").val();
    let desc = $("#Description").val().trim();
    let isEnable = $("#IsEnable").val();

    const model = {
        id: modelId,
        serviceName: serviceName,
        serviceCode: serviceCode,
        categoryId: categoryId,
        description: desc,
        rate: rate,
        isEnable: isEnable
    }
    return model;
}

// for validation
let codeValidation = false;
let nameValidation = false;

function DefaultErrorMessage() {

    $("#ServiceName-customError").text('');
    $("#ServiceCode-customError").text('');
    $("#Rate-customError").text('');
    $("#CategoryId-customError").text('');
}


function ValidateField() {
    let serviceName = $("#ServiceName").val().trim();
    let serviceCode = $("#ServiceCode").val().trim();
    let rate = $("#Rate").val();
    let categoryId = $("#CategoryId").val();

    DefaultErrorMessage();

    let isValid = true;

    if (serviceName == '') {
        $("#ServiceName-customError").text('This field is required');
        isValid = false;
    }
    if (nameValidation == true) {
        $("#ServiceName-customError").text('This Name Already Exists!');
        isValid = false;
    }

    if (serviceCode == '') {
        $("#ServiceCode-customError").text('This field is required');
        isValid = false;
    }

    if (codeValidation == true) {
        $("#ServiceCode-customError").text('This Code Already in use!');
        isValid = false;
    }

    if (rate == 0) {
        $("#Rate-customError").text('This field is required');
        isValid = false;
    }
    if (rate < 0) {
        $("#Rate-customError").text('Please enter a positive value');
        isValid = false;
    }
    if (categoryId == '') {
        $("#CategoryId-customError").text('This field is required');
        isValid = false;
    }

    return isValid;
}

// ServiceName validation
$("#ServiceName").on("change", function () {

    let name = $(this).val().trim();
    let id = $("#Id").val();

    if (name != "") {
        const url = API + "PrServiceInfo/IsNameExists";
        const params = { name : name, id : id};

        $.post(url, params, function (rData) {
            if (rData == true) {
                $("#ServiceName-customError").text('This Name Already Exists!');
                nameValidation = true;
            } else {
                $("#ServiceName-customError").text('');
                nameValidation = false;
            }
        });
    }
});

// code validation
$("#ServiceCode").on("change", function () {

    let code = $(this).val().trim();
    let id = $("#Id").val();

    if (code != "") {
        const url = API + "PrServiceInfo/IsCodeExists";
        const params = { code: code, id: id };

        $.post(url, params, function (rData) {
            if (rData == true) {
                $("#ServiceCode-customError").text('This Code Already in use!');
                codeValidation = true;
            } else {
                $("#ServiceCode-customError").text('');
                codeValidation = false;
            }
        });
    }
});

// for enable switch
$("#IsEnable").on("click", function () {
    var isChecked = $(this).is(":checked");
    if (isChecked) {
        $(this).val(true);
        $(this).prop("checked", true);
    } else {
        $(this).val(false);
        $(this).prop("checked", false);
    }
});

// clear all field
function ClearAllField() {
    $("#Id").val('0');
    $("#ServiceName").val('');
    $("#ServiceCode").val('');
    $("#Description").val('');
    $("#Rate").val(0);
    $("#CategoryId").val('').trigger(update);
    $("#IsEnable").val(false);
    $("#IsEnable").prop("checked", false);
    // for edit/update
    $("#updateBtn").hide();
    $("#saveBtn").show();

    // clear error msg
    DefaultErrorMessage();
}