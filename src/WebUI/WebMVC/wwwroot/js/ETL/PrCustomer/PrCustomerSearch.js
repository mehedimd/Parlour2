$(document).ready(() => {
    search();
})

function search() {
    const searchVm = {};
    if ($.fn.DataTable.isDataTable("#CustomerSearchTable")) {
        const table = $("#CustomerSearchTable").DataTable();
        table.distroy();
    }
    var params = "";

    if (!hasAnyError(searchVm)) {
        params = { SearchModel: searchVm };
    }

    const oTable = $("#CustomerSearchTable").DataTable({
        "aLengthMenu": DataTable.lengthMenu,
        "iDisplayLength": DataTable.displayLength,
        "processing": DataTable.processing,
        "serverSide": DataTable.serverSide,
        "ordering": false,

        "ajax": {
            url: API + "PrCustomer/Search",
            type: "POST",
            data: params,
        },
        error(e) {
            failedMsg(e);
        },
        "columns": [
            { "data": "serialNo" },
            { "data": "registrationNo" },
            { "data": "customerName" },
            { "data": "mobile" },
            { "data": "email" },
            { "data": "address" },
            { "data": "registrationDate"},
            { "data": "branchName" },
            {
                "render": function (data, type, item) {

                    showTotalRowCountSpanInDataTable("CustomerSearchTable", oTable);

                    let editButton = `<a class='text-success' style="cursor:pointer" href='${API}PrCustomer/Edit/${item.id}' ><i class="fa fa-edit"></i></a>`;
                    let deleteButton = `<a class='text-danger ms-3' style="cursor:pointer" href='${API}PrCustomer/Delete/${item.id}' title='Delete'><i class="fa fa-trash"></i></a>`;
                    let detailsButton = `<a class='text-success ms-3' style="cursor:pointer" href='${API}PrCustomer/Details/${item.id}' title='Details'><i class="icofont icofont-user-search"></i></a>`;

                    return `<div style="font-size: 18px;"><div>` + editButton + deleteButton + detailsButton + `</div></div>`;
                }
            }
        ]
    })
    addTotalRowCountSpanInDataTable("CustomerSearchTable")
}