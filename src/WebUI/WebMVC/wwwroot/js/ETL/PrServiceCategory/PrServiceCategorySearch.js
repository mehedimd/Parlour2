$(document).ready(function () {
    search();

    // for edit/update
    $("#updateBtn").hide();
    $("#saveBtn").show();
})

function search() {
    const searchVm = {};

    if ($.fn.DataTable.isDataTable("#ServiceCategorySearchTable")) {
        const table = $("#ServiceCategorySearchTable").DataTable();
        table.destroy();
    }

    var params = "";
    if (!hasAnyError(searchVm)) {
        params = { SearchModel: searchVm };
    }

    const oTable = $("#ServiceCategorySearchTable").DataTable({
        "aLengthMenu": DataTable.lengthMenu,
        "iDisplayLength": DataTable.displayLength,
        "processing": DataTable.processing,
        "serverSide": DataTable.serverSide,
        "ordering": false,

        "ajax": {
            url: API + "PrServiceCategory/Search",
            type: "POST",
            data: params,
        }, error(e) {
            failedMsg(e);
        },

        "columns": [
            { "data": "serialNo" },
            { "data": "categoryName" },
            { "data": "categoryCode" },
            { "data": "description" },
            { "data": "isEnable" },
            {
                "render": function (data, type, item) {

                    showTotalRowCountSpanInDataTable("ServiceCategorySearchTable", oTable);

                    let editButton = `<a class='text-success' style="cursor:pointer" onclick="Edit(${item.id})"><i class="fa fa-edit"></i></a>`;
                    let deleteButton = `<a class='text-danger ms-3' style="cursor:pointer" onclick="DeleteCategory(${item.id})" title='Delete'><i class="fa fa-trash"></i></a>`;

                    return `<div style="font-size: 18px;"><div>` + editButton + deleteButton+ `</div></div>`;
                }
            }
        ]
    });

    addTotalRowCountSpanInDataTable("ServiceCategorySearchTable");
}

// Create (post)
function CreateNew() {
    console.log(ModelGet())
    if (ValidateField()) {
        $.ajax({
            method: 'post',
            url: API + "PrServiceCategory/Create",
            data: ModelGet(),
            success: function (res) {
                search();
                ClearAllField()
                successMsg("Service Category Successfully Created ");
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
        url: API + "PrServiceCategory/Edit/" + id,
        success: function (res) {
            console.log(res)
            $("#Id").val(res.id);
            $("#CategoryName").val(res.categoryName);
            $("#CategoryCode").val(res.categoryCode);
            $("#Description").val(res.description);
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
function UpdateServiceCategory() {

    if (ValidateField()) {
        $.ajax({
            method: 'post',
            url: API + "PrServiceCategory/Edit",
            data: ModelGet(),
            success: function (res) {
                search();
                ClearAllField()
                successMsg("Service Category Successfully Updated")
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
function DeleteCategory(id) {
    $.ajax({
        method: 'get',
        url: API + "PrServiceCategory/Delete/" + id,
        success: function (res) {
            search();
            successMsg("Service Category Successfully Deleted")
        },
        error: e => {
            failedMsg(e);
        }
    })
}

// get model
function ModelGet() {
    let catId = $("#Id").val();
    let catName = $("#CategoryName").val().trim();
    let catCode = $("#CategoryCode").val().trim();
    let catDesc = $("#Description").val().trim();
    let catIsEnable = $("#IsEnable").val();
    const model = {
        id: catId,
        categoryName: catName,
        categoryCode: catCode,
        description: catDesc,
        isEnable: catIsEnable
    }
    return model;
}

// for validation
let codeValidation = false;
let nameValidation = false;

function DefaultErrorMessage() {
    $("#CategoryName-customError").text('');
    $("#CategoryCode-customError").text('');
}
function ValidateField() {

    DefaultErrorMessage();

    let catName = $("#CategoryName").val();
    let catCode = $("#CategoryCode").val();

    let isValid = true;
    if (catName == '') {
        $("#CategoryName-customError").text('This field is required');
        isValid = false;
    }
    if (nameValidation == true) {
        $("#CategoryName-customError").text('This name already exists!');
        isValid = false;
    }
    if (catCode == '') {
        $("#CategoryCode-customError").text('This field is required');
        isValid = false;
    }
    if (codeValidation == true) {
        $("#CategoryCode-customError").text('This code already exists!');
        isValid = false;
    }
    return isValid;
}

// CategoryeName validation
$("#CategoryName").on("change", function () {

    let name = $(this).val().trim();
    let id = $("#Id").val();

    if (name != "") {
        const url = API + "PrServiceCategory/IsNameExists";
        const params = { name: name, id: id };

        $.post(url, params, function (rData) {
            if (rData == true) {
                $("#CategoryName-customError").text('This Name Already Exists!');
                nameValidation = true;
            } else {
                $("#CategoryName-customError").text('');
                nameValidation = false;
            }
        });
    }
});

// code validation
$("#CategoryCode").on("change", function () {

    let code = $(this).val().trim();
    let id = $("#Id").val();

    if (code != "") {
        const url = API + "PrServiceCategory/IsCodeExists";
        const params = { code: code, id: id };

        $.post(url, params, function (rData) {
            if (rData == true) {
                $("#CategoryCode-customError").text('This Code Already Exists!');
                codeValidation = true;
            } else {
                $("#CategoryCode-customError").text('');
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
    DefaultErrorMessage();
    $("#Id").val('0');
    $("#CategoryName").val('');
    $("#CategoryCode").val('');
    $("#Description").val('');
    $("#IsEnable").val(false);
    $("#IsEnable").prop("checked", false);
    // for edit/update
    $("#updateBtn").hide();
    $("#saveBtn").show();
}
