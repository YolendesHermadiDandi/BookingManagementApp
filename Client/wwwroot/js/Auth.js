function Login() {
    console.log("Oke");
    let auth = new Object();
    auth.email = $("#email").val();
    auth.password = $("#password").val();

    $.ajax({
        type: "post",
        url: "SignIn",
        data: auth,
    }).done((result) => {
        if (result.code == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Login Success',
                showConfirmButton: false,
                timer: 2500
            })
            setTimeout("location.href = '/';", 2500);
            
        } else if (result.code == 404) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Invalid email/password',
                showConfirmButton: false,
                timer: 3500
            })

        }
        /*console.log(result);*/
        //Swal.fire({
        //    icon: 'success',
        //    title: 'Update Success',
        //    showConfirmButton: false,
        //    timer: 1500
        //})
        //$('#tabelEmployee').DataTable().ajax.reload();
    }).fail((error) => {
        //Swal.fire({
        //    icon: 'error',
        //    title: 'Oops...',
        //    text: 'Failed to update data',

        //})
        //$('#tabelEmployee').DataTable().ajax.reload();
    });

    console.log(auth)
}