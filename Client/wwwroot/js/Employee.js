﻿function DateFormat(date) {
    const today = new Date(date);
    const yyyy = today.getFullYear();
    let mm = today.getMonth() + 1; // Months start at 0!
    let dd = today.getDate();

    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;

    return formattedToday = dd + '-' + mm + '-' + yyyy
}


$('#addEmployee').on('click', () => {
    $('div.action-button').html('<button type="submit" id="submitButton" onclick="Insert()" class="btn btn-primary" data-bs-dismiss="modal">Submit</button>')
    document.getElementById("emploteeForm").reset();
})
function getUpdateEmployee(data) {
    $('div.action-button').html('<button type="submit" id="updateButton" onclick="Update()" class="btn btn-primary" data-bs-dismiss="modal">Update</button>');
    $.ajax({
        url: `https://localhost:7100/api/employee/${data}`,
        dataSrc: "data",
        dataType: "JSON"
    }).done((result) => {
        $('#uEmpId').val(`${data}`);
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

function Insert() {
  /*  console.log('Oke');*/
    let employee = new Object();
    employee.firstName = $("#firstName").val();
    employee.lastName = $("#lastName").val();
    employee.birthDate = $("#birthDate").val();
    employee.gender = parseInt($("#genderSelect").find(':selected').val());
    employee.hiringDate = $("#hiringDate").val();
    employee.email = $("#email").val();
    employee.phoneNumber = $("#phoneNumber").val();
    if (employee.birthDate == '' || employee.hiringDate == '') {
        return alert('birth date or hiring date cant null');
    }
    let birthDate = moment(employee.birthDate, "DD/MM/YYYY");
    let hiringDate = moment(employee.hiringDate, "DD/MM/YYYY");
    employee.birthDate = new Date(birthDate).toISOString();
    employee.hiringDate = new Date(hiringDate).toISOString();
    console.log(employee);
    $.ajax({
        type: "post",
        headers: {
            'Accept': 'application/json;charset=utf-8',
            'Content-Type': 'application/json;charset=utf-8'
        },
        //contentType: "application/json;",
        url: "https://localhost:7100/api/employee/insert",
        dataType: "json",
        //async: false,
        data: JSON.stringify(employee),
    }).done((result) => {
        Swal.fire({
            icon: 'success',
            title: 'Insert Success',
            showConfirmButton: false,
            timer: 1500
        });
        $('#tabelEmployee').DataTable().ajax.reload();
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to insert data',

        })
        $('#tabelEmployee').DataTable().ajax.reload();
    });


}

function Update() {
    let employee = new Object();
    employee.guid = $("#uEmpId").val();
    employee.nik = "";
    employee.firstName = $("#firstName").val();
    employee.lastName = $("#lastName").val();
    employee.birthDate = $("#birthDate").val();
    employee.gender = parseInt($("#genderSelect").find(':selected').val());
    employee.hiringDate = $("#hiringDate").val();
    employee.email = $("#email").val();
    employee.phoneNumber = $("#phoneNumber").val();
    if (employee.birthDate == '' || employee.hiringDate == '') {
        return alert('birth date or hiring date cant null');
    }
    let birthDate = moment(employee.birthDate, "DD/MM/YYYY");
    let hiringDate = moment(employee.hiringDate, "DD/MM/YYYY");
    employee.birthDate = new Date(birthDate).toISOString();
    employee.hiringDate = new Date(hiringDate).toISOString();
    //console.log(employee);
    $.ajax({
        type: "put",
        headers: {
            'Accept': 'application/json;charset=utf-8',
            'Content-Type': 'application/json;charset=utf-8'
        },
        url: "https://localhost:7100/api/employee/update",
        dataType: "json",
        //async: false,
        data: JSON.stringify(
            employee
        ),
    }).done((result) => {
        //console.log(result);
        Swal.fire({
            icon: 'success',
            title: 'Update Success',
            showConfirmButton: false,
            timer: 1500
        })
        $('#tabelEmployee').DataTable().ajax.reload();
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to update data',

        })
        $('#tabelEmployee').DataTable().ajax.reload();
    });
}


//Delete
function Delete(data) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `https://localhost:7100/api/employee/${data}`,
                type: "DELETE"
            }).done((result) => {
                console.log(result);
                Swal.fire(
                    'Deleted!',
                    'Your file has been deleted.',
                    'success'
                )
                $('#tabelEmployee').DataTable().ajax.reload();
            }).fail((error) => {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Failed to delete data',

                })
            });

        }
    })
}
$(document).ready(function () {
    let dataEmployee = $("#tabelEmployee").DataTable({
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
                //
                render: function (data, type, row) {
                    return ` <input type="hidden" id="empId" name="empId" value="${row.guid}">
                        <a class="me-3" onclick=getUpdateEmployee('${row.guid}') data-bs-target="#adddUpdateEmployee" data-bs-toggle="modal">
                            <img src="assets/img/icons/edit.svg" alt="img">
                        </a>
                        <a class="me-3 confirm-text"  onclick=Delete('${row.guid}')>
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
    })
})



