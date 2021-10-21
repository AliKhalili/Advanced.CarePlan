BASE_URL = "http://localhost:5000/api/care-plans";
API_HEADER = { "X-API-Key": "API_KEY_SECRET_VALUE" }
$(function () {
    LoadAll();
})
function LoadAll() {
    $Table = $("#care_plan_table tbody");
    $Table.html("")
    $.ajax({
        url: `${BASE_URL}`,
        headers: API_HEADER,
        type: 'GET',
        dataType: 'json',
        success: function (records) {
            $Table.html("")
            $.each(records, function (index, record) {
                $Table.append(`
                <tr>
                    <td>${record.care_plan_id}</td>
                    <td>${record.title}</td>
                    <td>${record.patient_name}</td>
                    <td>${record.create_by_user}</td>
                    <td>${record.start_date}</td>
                    <td>${record.target_date}</td>
                    <td>${record.completed}</td>
                    <td>${ActionBtn(record.care_plan_id)}</td>
                </tr>
                `);
            });
        },
        error: function (request, message, error) {
            handleErrors("Connection error.");
        }
    });
}
function ActionBtn(careId) {
    return `
    <div class="btn-group" role="group" aria-label="Basic example">
        <button type="button" class="btn btn-success" onclick="GetAction(${careId});">Details</button>
        <button type="button" class="btn btn-info" onclick="UpdateAction(${careId});">Update</button>
        <button type="button" class="btn btn-danger" onclick="DeleteAction(${careId});">Delete</button>
    </div>`
}
function GetAction(careId) {
    $("#detailModal .modal-body").html("")
    $("#detailModal .modal-title").html("")
    $.ajax({
        url: `${BASE_URL}/${careId}`,
        headers: API_HEADER,
        type: 'GET',
        dataType: 'json',
        success: function (record) {
            $("#detailModal .modal-body").append(
                `<div class="row"><div class="col">Patient Name: <b>${record.patient_name}</b></div></div>
                <div class="row"><div class="col">Create By User: <b>${record.create_by_user}</b></div></div>
                <div class="row"><div class="col">Start Date: <b>${record.start_date}</b></div></div>
                <div class="row"><div class="col">Target Date: <b>${record.target_date}</b></div></div>
                <div class="row"><div class="col">Reasons: <b>${record.reasons}</b></div></div>
                <div class="row"><div class="col">Actions: <b>${record.actions}</b></div></div>
                <div class="row"><div class="col">Frequency: <b>${record.frequency}</b></div></div>`
            );
            if (record.completed === "Yes") {
                $("#detailModal .modal-body").append(`
                <div class="row"><div class="col">Completed: <b>${record.completed}</b></div></div>
                <div class="row"><div class="col">End Date: <b>${record.end_date}</b></div></div>
                <div class="row"><div class="col">Outcomes: <b>${record.outcomes}</b></div></div>
                `)
                $("#detailModal .modal-title").append("<button type='button' class='btn btn-success btn-xsm'><i class='fa-solid fa-hourglass-end'></i></button> ");
            }
            else {
                $("#detailModal .modal-title").append("<button type='button' class='btn btn-danger btn-xsm'><i class='fa-solid fa-hourglass'></i></button> ");
            }
            $("#detailModal .modal-title").append(`#${record.care_plan_id} - ${record.title}`)
            $("#detailModal").modal("show");
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });

}
function UpdateAction(careId) {
    $("#updateModal .modal-title").html(`Updating ${careId}`);
    $.ajax({
        url: `${BASE_URL}/${careId}`,
        headers: API_HEADER,
        type: 'GET',
        success: function (record) {
            $("#care_plan_id").val(careId)
            $("#care_title").val(record.title);
            $("#care_patient_name").val(record.patient_name);
            $("#care_create_by_user").val(record.create_by_user);
            $("#care_start_date").val(record.start_date);
            $("#care_target_date").val(record.target_date);
            $("#care_reasons").val(record.reasons);
            $("#care_actions").val(record.actions);
            $("#care_frequency").val(record.frequency);
            $("#care_completed").val(record.completed);
            $("#care_end_date").val(record.end_date);
            $("#care_outcomes").val(record.outcomes);

            $("#updateModal").modal("show");
        },
        error: function (request, message, error) {
        }
    });
}
function CreateAction() {
    $("#create_createModal .modal-title").html(`Create New`);
    $("#create_care_title").val('');
    $("#create_care_patient_name").val('');
    $("#create_care_create_by_user").val('');
    $("#create_care_start_date").val('');
    $("#create_care_target_date").val('');
    $("#create_care_reasons").val('');
    $("#create_care_actions").val('');
    $("#create_care_frequency").val('');
    $("#create_care_completed").val('');
    $("#create_care_end_date").val('');
    $("#create_care_outcomes").val('');
    $("#create_btn").click(function () {
        var record = {
            title: $("#create_care_title").val(),
            patient_name: $("#create_care_patient_name").val(),
            create_by_user: $("#create_care_create_by_user").val(),
            start_date: $("#create_care_start_date").val(),
            target_date: $("#create_care_target_date").val(),
            reasons: $("#create_care_reasons").val(),
            actions: $("#create_care_actions").val(),
            frequency: $("#create_care_frequency").val(),
            completed: $("#create_care_completed").val(),
            end_date: $("#create_care_end_date").val(),
            outcomes: $("#create_care_outcomes").val(),
        };
        $.ajax({
            url: `${BASE_URL}`,
            headers: API_HEADER,
            type: 'POST',
            dataType: 'json',
            contentType: "application/json",
            data: JSON.stringify(record),
            success: function (result, textStatus, xhr) {
                if (xhr.status == 201) {
                    $("#createModal").modal("hide");
                }
            },
            error: function (request, message, error) {
                if (request.status == 404) {
                    handleErrors("Not found error.")
                }
                else if (request.status == 400) {
                    var tErrors = []
                    $.each(request.responseJSON.errors, function (index, e) {
                        tErrors.push(e.message)
                    })
                    handleErrors(tErrors)
                }
                else if (request.status == 500) {
                    handleErrors("Internal server error.")
                }
            },
            complete: function (xhr, textStatus) {
                LoadAll();
            }
        });
    });
    $("#createModal").modal("show");
}
function DeleteAction(careId) {
    $("#deleteModal .modal-title").html(`Are you sure to care plan ${careId}`);
    $("#delete_btn").click(function () {
        $.ajax({
            url: `${BASE_URL}/${careId}`,
            headers: API_HEADER,
            type: 'DELETE',
            success: function () {
                $("#deleteModal").modal("hide");
            },
            error: function (request, message, error) {
                $("#deleteModal").modal("hide");
            },
            complete: function (xhr, textStatus) {
                LoadAll();
            }
        });
    });
    $("#deleteModal").modal("show");
}
function handleErrors(errors) {
    $("#errorModal .modal-body").html("");
    $.each(errors, function (index, errorItem) {
        $("#errorModal .modal-body").append(`
            <div class="alert alert-danger" role="alert">${errorItem}</div>
        `);
    });
    $("#errorModal").modal("show");
}

function DoUpdate() {
    var record = {
        title: $("#care_title").val(),
        patient_name: $("#care_patient_name").val(),
        create_by_user: $("#care_create_by_user").val(),
        start_date: $("#care_start_date").val(),
        target_date: $("#care_target_date").val(),
        reasons: $("#care_reasons").val(),
        actions: $("#care_actions").val(),
        frequency: $("#care_frequency").val(),
        completed: $("#care_completed").val(),
        end_date: $("#care_end_date").val(),
        outcomes: $("#care_outcomes").val(),
    };
    $.ajax({
        url: `${BASE_URL}/${$("#care_plan_id").val()}`,
        headers: API_HEADER,
        type: 'PUT',
        dataType: 'json',
        contentType: "application/json",
        data: JSON.stringify(record),
        success: function (result, textStatus, xhr) {
            if (xhr.status == 204) {
                $("#updateModal").modal("hide");
            }
        },
        error: function (request, message, error) {
            if (request.status == 404) {
                handleErrors("Not found error.")
            }
            else if (request.status == 400) {
                var tErrors = []
                $.each(request.responseJSON.errors, function (index, e) {
                    tErrors.push(e.message)
                })
                handleErrors(tErrors)
            }
            else if (request.status == 500) {
                handleErrors("Internal server error.")
            }
        },
        complete: function (xhr, textStatus) {
            LoadAll();
        }
    });
}