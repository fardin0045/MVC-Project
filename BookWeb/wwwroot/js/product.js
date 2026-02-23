$(document).ready(function () {
    loadDataTable();
});
function Delete(url) {
    if (confirm("Are you sure you want to delete?")) {
        $.ajax({
            url: url,
            type: 'DELETE',
            success: function (data) {
                if (data.success) {
                    dataTable.ajax.reload();
                    alert(data.message);
                }
                else {
                    alert(data.message);
                }
            }
        });
    }
}
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'title', width: "25%" },
            { data: 'isbn', width: "15%" },
            { data: 'listPrice', width: "10%" },
            { data: 'categoryId', width: "15%" },
           {
    data: 'id',
    render: function (data) {
        return `
            <div class="d-felx justify-content-center gap-4">
                <a href="/admin/product/edit/${data}" 
                   class="btn btn-sm btn-primary">Edit</a>

                <button onclick="Delete('/admin/product/delete/${data}')" 
                        class="btn btn-sm btn-danger">
                        Delete
                </button>
            </div>
        `;
    },
    width: "20%"
}
        ]
    });
}
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();

                        Swal.fire(
                            'Deleted!',
                            data.message,
                            'success'
                        );
                    }
                    else {
                        Swal.fire(
                            'Error!',
                            data.message,
                            'error'
                        );
                    }
                }
            });
        }
    });
}