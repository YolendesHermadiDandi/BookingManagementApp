$("#tabelEmployee").DataTable({
    ajax: {
        url: "https://localhost:7100/api/employee",
        dataSrc: "data",
        dataType: "JSON"
    },
    columns: [
        {
            data: "",
            render: function (data, type, row, meta) {
                return meta.row + 1;
            }
        },
        { data: "nik" },
        { data: "firstName" },
        { data: "lastName" },
        {
            data: "birthDate",
            render: function (data, type, row) {
                return DateFormat(row.birthDate);
            }
        },
        {
            data: "gender",
            render: function (data, type, row) {
                return row.gender == "0" ? "Female" : "Male";
            }
        },
        {
            data: "hiringDate",
            render: function (data, type, row) {
                return DateFormat(row.hiringDate);
            }
        },
        { data: "email" },
        { data: "phoneNumber" },
        {
            data: "guid",
            render: function (data, type, row) {
                return ` <input type="hidden" id="empId" name="empId" value="${row.guid}">
                        <a class="me-3" id="empuUpdate" onclick=GetUpdateEmployee('${row.guid}') data-bs-target="#adddUpdateEmployee" data-bs-toggle="modal">
                            <img src="assets/img/icons/edit.svg" alt="img">
                        </a>
                        <a class="me-3 confirm-text"  >
                            <img src="assets/img/icons/delete.svg" alt="img">
                        </a>`;
            }
        }
    ],
    dom: 'Bftp',
    buttons: [
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: ':visible'
            },
            className: 'btn btn-outline-success',
            titleAttr: 'excel',
            text: '<i class="fa-solid fa-file-excel"></i>',
        },
        {
            extend: 'pdfHtml5',
            exportOptions: {
                columns: ':visible',
                columns: [1, 2, 3, 5, 7, 8]
            },
            className: 'btn btn-outline-danger',
            titleAttr: 'pdf',
            text: '<i class="fa-solid fa-file-pdf"></i>',
            //orientation: 'landscape',
            //pageSize: 'LEGAL'
        },
        {
            extend: 'colvis',
            className: 'btn btn-outline-info text-black',
        }
    ],
});

$('.dt-buttons').removeClass('dt-buttons');
$('button#addEmployee').on('click', (e) => {
    $('button#submitButton').removeAttr('hidden');
});

//Create employee
$(document).ready((e) => {
    $('#submitButton').bind('click', function (e) {
        e.preventDefault();
        let employee = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
        employee.firstName = $("#firstName").val();
        employee.lastName = $("#lastName").val();
        employee.birthDate = $("#birthDate").val();
        employee.gender = parseInt($("#genderSelect").find(':selected').val());
        employee.hiringDate = $("#hiringDate").val();
        employee.email = $("#email").val();
        employee.phoneNumber = $("#phoneNumber").val();
        let birthDate = moment(employee.birthDate, "DD/MM/YYYY");
        let hiringDate = moment(employee.hiringDate, "DD/MM/YYYY")
        employee.birthDate = new Date(birthDate).toISOString();
        employee.hiringDate = new Date(hiringDate).toISOString();

        $.ajax({
            url: "https://localhost:7100/api/employee/insert",
            type: "POST",
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(employee),
            //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
        }).done((result) => {
            alert("Success");
            location.reload();
        }).fail((error) => {
            console.log(error)
            alert("Failed");
        });
    });
});

// Get Update employee
function GetUpdateEmployee(data) {
    $('button#submitButton').attr('hidden');
    $('#updateButton').removeAttr('hidden');
    let empId = $('#empId').val();
    //console.log(empId);
    $.ajax({
        url: `https://localhost:7100/api/employee/${data}`,
        dataSrc: "data",
        dataType: "JSON"

    }).done((result) => {
        let birthDate = new Date(result.data.birthDate);
        $('#uEmpId').val(`${empId}`);
        $("#firstName").val(`${result.data.firstName}`);
        $("#lastName").val(`${result.data.lastName}`);
        $("#birthDate").val(`${DateFormat(result.data.birthDate)}`);
        $("#genderSelect").val(`${result.data.gender}`);
        $("#hiringDate").val(`${DateFormat(result.data.hiringDate)}`);
        $("#email").val(`${result.data.email}`);
        $("#phoneNumber").val(`${result.data.phoneNumber}`);
    }).fail((error) => {
    });
}




//Update Employee
$(document).ready((e) => {
    $('#updateButton').on('click', function (e) {
        e.preventDefault();
        let employee = new Object();
        employee.guid = $("#empId").val();
        employee.nik = "";
        employee.firstName = $("#firstName").val();
        employee.lastName = $("#lastName").val();
        employee.birthDate = $("#birthDate").val();
        employee.gender = parseInt($("#genderSelect").find(':selected').val());
        employee.hiringDate = $("#hiringDate").val();
        employee.email = $("#email").val();
        employee.phoneNumber = $("#phoneNumber").val();
        let birthDate = moment(employee.birthDate, "DD/MM/YYYY");
        let hiringDate = moment(employee.hiringDate, "DD/MM/YYYY")
        employee.birthDate = new Date(birthDate).toISOString();
        employee.hiringDate = new Date(hiringDate).toISOString();
        console.log(JSON.stringify(employee));
        $.ajax({
            url: "https://localhost:7100/api/employee/update",
            type: "PUT",
            dataType: "json",
            headers: {
                `Accept`: 'application/json;charset=utf-8',
                `contentType`: 'application/json;charset=utf-8',

            },
            data: JSON.stringify(employee),

        }).done((result) => {
            console.log(result);
            alert("Success");
            alert(result);
            location.reload();
        }).fail((error) => {
            alert("Failed");
            alert(error.responseJSON.error);
        });

    });
});










//function Update() {
//    console.log("hah");
//    let employee = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
//    employee.guid = $("#empId").val();
//    employee.nik = "";
//    employee.firstName = $("#firstName").val();
//    employee.lastName = $("#lastName").val();
//    employee.birthDate = $("#birthDate").val();
//    employee.gender = parseInt($("#genderSelect").find(':selected').val());
//    employee.hiringDate = $("#hiringDate").val();
//    employee.email = $("#email").val();
//    employee.phoneNumber = $("#phoneNumber").val();
//    let birthDate = moment(employee.birthDate, "DD/MM/YYYY");
//    let hiringDate = moment(employee.hiringDate, "DD/MM/YYYY")
//    employee.birthDate = new Date(birthDate).toISOString();
//    employee.hiringDate = new Date(hiringDate).toISOString();
//    console.log(employee.email);
//    alert(employee);
//    $.ajax({
//        url: "https://localhost:7100/api/employee/update",
//        type: "PUT",
//        dataType: "json",
//        async: false,
//        contentType: "application/json;charset=utf-8",
//        data: JSON.stringify(employee),
//        //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
//    }).done((result) => {
//        alert("Success");
//        location.reload();
//    }).fail((error) => {
//        console.log(error)
//        alert("Failed"+ error.respnseJSON.error);
//    });

//}




function DateFormat(date) {
    const today = new Date(date);
    const yyyy = today.getFullYear();
    let mm = today.getMonth() + 1; // Months start at 0!
    let dd = today.getDate();

    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;

    return formattedToday = dd + '-' + mm + '-' + yyyy
}