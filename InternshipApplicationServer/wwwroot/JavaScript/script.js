function ShowDialog()
{
    $('#confirmDelete').modal('show');
}

function HideDialog() {
    $('#confirmDelete').modal('hide');
}

window.ToastShow = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation completed!");
    }
    if (type === "error") {
        toastr.error(message, "Operation failed!")
    }
}