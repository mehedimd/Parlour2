$(document).ready(function () {
    $("#date-section").hide();

    const employeeId = $("#EmployeeId").val();

    if (employeeId > 0) {
        _dropdownManager.getLeaveTypeByEmployeGenderSelectListItems(employeeId, "#LeaveTypeId", null, null);
    }
});

$(document.body).on("change", "#EmployeeId", function () {
    const employeeId = $(this).val();

    if (employeeId > 0) {
        _dropdownManager.getLeaveTypeByEmployeGenderSelectListItems(employeeId, "#LeaveTypeId", null, null);
    }

    clearData();
});


$("#ToDateStr").datepicker({
    onSelect: function (d, i) {
        calculateLength();
    }
});

function calculateLength() {
    const startDate = $("#FromDateStr").val();
    const endDate = $("#ToDateStr").val();
    const leaveBalance = $("#leaveBalance").val();
    const remainLeave = $("#remainLeave").val();

    if (!hasAnyError(startDate) && !hasAnyError(endDate)) {
        const result = getDayDiffBetweenTwoDates(startDate, endDate);

        if (leaveBalance > 0 && result > remainLeave) {
            clearData();
            return failedMsg("No Leave Remain..!!");
        }
        $("#Length").val(result);
    }
}

$(document.body).on("change", "#LeaveTypeId", function () {
    const leaveTypeId = $(this).val();
    const employeeId = $("#EmployeeId").val();

    if (leaveTypeId > 0) {
        const url = `${API}EmpLeaveApplication/GetLeaveSetupBalanceById?leaveTypeId=${leaveTypeId}&employeeId=${employeeId}`;

        $.get(url, function (rData) {
            if (rData != null) {
                console.log(rData);
                renderLeaveBalanceCard(rData);
            }
        }).fail(function () {
            failedMsg("Leave Type Information Not Found..!!");
        })

        const dateUrl = `${API}EmpLeaveApplication/GetEmpLeaveDates?empId=${employeeId}&typeId=${leaveTypeId}`;

        $.get(dateUrl, function (rData) {
            if (rData != null) {
                console.log("Leave Dates", rData);
                renderTakenLeaveDates(rData);
            }
        }).fail(function () {
            failedMsg("Leave Date Information Not Found..!!");
        })
    }

    clearData();
});

function clearData() {
    $("#FromDateStr").val('');
    $("#ToDateStr").val('');
    $("#Length").val(0);
}

function renderLeaveBalanceCard(leave) {
    let html = `<tr>
                    <td><b>Total Leave</b></td>
                    <td>
                        <input type='hidden' id='leaveBalance' value='${leave.leaveBalance}'/>
                        ${leave.leaveBalance}
                    </td>
                </tr>
                <tr>
                    <td><b>Leave Taken</b></td>
                    <td>${leave.leaveTaken}</td>
                </tr>
                <tr>
                    <td><b>Leave Remain</b></td>
                    <td>
                        <input type='hidden' id='remainLeave' value='${leave.leaveRemain}'/>
                        ${leave.leaveRemain}
                    </td>
                </tr>`;

    $("#tBodySec").html(html);
}

function renderTakenLeaveDates(leaveDates) {
    if (leaveDates != null && leaveDates.length > 0) {
        $("#DateTbodySec").empty();

        leaveDates.forEach(v => {
            const html = `<tr>
                            <td>
                                ${v.leaveDateStr} - (${v.dayStr})
                            </td>
                          </tr>`;

            $("#DateTbodySec").append(html);
        })

        $("#date-section").show();
    }
}
