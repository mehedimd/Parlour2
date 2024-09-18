$(document).ready(function () {
    search();
})

function search() {
    const searchVm = {};

    if ($.fn.DataTable.isDataTable("#EmpLeaveAppSearchTable")) {
        const table = $("#EmpLeaveAppSearchTable").DataTable();
        table.destroy();
    }

    var params = "";
    if (!hasAnyError(searchVm)) {
        params = { SearchModel: searchVm };
    }

    const oTable = $("#EmpLeaveAppSearchTable").DataTable({
        "aLengthMenu": DataTable.lengthMenu,
        "iDisplayLength": DataTable.displayLength,
        "processing": DataTable.processing,
        "serverSide": DataTable.serverSide,
        "ordering": false,

        "ajax": {
            url: API + "EmpLeaveApplication/Search",
            type: "POST",
            data: params,
        }, error(e) {
            failedMsg(e);
        },

        "columns": [
            { "data": "serialNo" },
            { "data": "employeeName" },
            { "data": "leaveTypeName" },
            {
                "render": function (data, type, item) {
                    const submitDate = convertJsonFullDateForView(item.submitDate);
                    return submitDate;
                }
            },
            {
                "render": function (data, type, item) {
                    const fromDate = convertJsonFullDateForView(item.fromDate);
                    return fromDate;
                }
            },
            {
                "render": function (data, type, item) {
                    const toDate = convertJsonFullDateForView(item.toDate);
                    return toDate;
                }
            },
            {
                "render": function (data, type, item) {
                    let status = "";

                    if (item.status == AppStatusEnum().Pending) {
                        status = `<div style='font-size: 12px' class='badge badge-warning'>Pending</div>`;
                    } else if (item.status == AppStatusEnum().Approve) {
                        status = `<div style='font-size: 12px' class='badge badge-primary'>Approved</div>`;
                    } else if (item.status == AppStatusEnum().Reject) {
                        status = `<div style='font-size: 12px' class='badge badge-danger'>Reject</div>`;
                    } else if (item.status == AppStatusEnum().Cancel) {
                        status = `<div style='font-size: 12px' class='badge badge-secondary'>Cancel</div>`;
                    } else {
                        status = "";
                    }

                    return `<div class='text-center'>${status}</div>`;
                }
            },
            {
                "render": function (data, type, item) {
                    const file = `<a href='${item.fileDoc}' class='text-center' style='font-size:x-large;' title='Leave Application PDF' target='_blank'><i class="fa fa-file-pdf-o"></i></a>`;
                    return file;
                }
            },
            {
                "render": function (data, type, item) {

                    showTotalRowCountSpanInDataTable("EmpLeaveAppSearchTable", oTable);

                    let viewBtn = `<a class='m-2' href='${API}EmpLeaveApplication/Details/${item.id}' title='View'><i class="fa fa-search"></i></a>`;
                    let editButton = `<a class='m-2' href='${API}EmpLeaveApplication/Edit/${item.id}' title='Edit'><i class="fa fa-edit"></i></a>`;
                    /*let deleteButton = `<a class='m-2 delconfirm' data-id='${item.id}' href='#' title='Delete'><i class="fa fa-trash"></i></a>`;*/
                    return `<div style='font-size: 20px;text-align: center;'>${viewBtn} ${editButton}</div>`;
                }
            }
        ]
    });

    addTotalRowCountSpanInDataTable("EmpLeaveAppSearchTable");
}

$(document.body).on("click", ".delconfirm", function () {
    swal({
        title: "Delete Confirmation",
        text: "Are you sure to delete this item?",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((result) => {
            if (result) {
                var id = $(this).attr("data-id");
                const url = `${API}EmpLeaveApplication/Delete/${id}`;

                $.get(url, function (rData) {
                    if (rData) {
                        successMsg("Deleted Successfully");
                    } else {
                        failedMsg("Deleted Failed...!")
                    }
                    search();
                })
            }
        })
})


function AppStatusEnum() {
    const model = {
        Pending: 0,
        Approve: 1,
        Reject: 2,
        Cancel: 3
    }

    return model;
}