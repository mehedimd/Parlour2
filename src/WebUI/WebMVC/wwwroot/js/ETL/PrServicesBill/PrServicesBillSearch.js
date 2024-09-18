$(document).ready(function () {
    search();
})

function search() {
    const searchVm = {};

    if ($.fn.DataTable.isDataTable("#PrBillSearchTable")) {
        const table = $("#PrBillSearchTable").DataTable();
        table.destroy();
    }

    var params = "";
    if (!hasAnyError(searchVm)) {
        params = { SearchModel: searchVm };
    }

    const oTable = $("#PrBillSearchTable").DataTable({
        "aLengthMenu": DataTable.lengthMenu,
        "iDisplayLength": DataTable.displayLength,
        "processing": DataTable.processing,
        "serverSide": DataTable.serverSide,
        "ordering": false,

        "ajax": {
            url: API + "PrServicesBill/Search",
            type: "POST",
            data: params,
        }, error(e) {
            failedMsg(e);
        },

        "columns": [
            { "data": "serialNo" },
            {
                "render": function (data, type, item) {
                    let serviceDate = convertJsonFullDateForView(new Date(item.serviceDate));
                    const serviceInfo = `<div>
                                    <h5>${item.serviceNo}</h5>
                                    <b>${serviceDate}</b>
                                  </div>`;

                    return serviceInfo;
                },
            },
            {
                "render": function (data, type, item) {

                    const customerInfo = `<div>
                                    <h5>${item.customerName}</h5>
                                    <b>${item.customerMobile}</b>
                                  </div>`;

                    return customerInfo;
                },
            },
            { "data": "netAmount" },
            { "data": "billStatusText" },
            { "data": "serviceStatusText" },
            {
                "render": function (data, type, item) {

                    showTotalRowCountSpanInDataTable("PrBillSearchTable", oTable);

                    let viewBtn = `<a style='margin-right: 3px; font-size:18px;' href='${API}PrServicesBill/Details/${item.id}' title='View'><i class="fa fa-search"></i></a>`;
                    let editButton = `<a class='mr-2' href='${API}Designation/Edit/${item.id}' title='Edit'><i class="fa fa-edit"></i></a>`;
                    let deleteButton = '';

                    return `<div style="font-size: 18px;"><div>${viewBtn}</div></div>`;
                }
            }
        ]
    });

    addTotalRowCountSpanInDataTable("PrBillSearchTable");
}

function getSearchObject() {
    const model = {
        BillStatus: $("#BillStatus").val(),
        FormDateStr: $("#FormDateStr").val(),
        ToDateStr: $("#ToDateStr").val(),
        CustomerId: $("#CustomerId").val(),
        ServiceNo: $("#ServiceNo").val(),
        CustomerMobile: $("#CustomerMobile").val()
    };

    return model;
}