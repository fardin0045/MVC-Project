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
            { data: 'title', width: "15%" },
            { data: 'isbn', width: "15%" },
            { data: 'listPrice', width: "15%" },
            { data: 'categoryId', width: "15%" },
            {
                data: 'id',
                render: function (data) {
                    return `
                        <a href="/admin/product/edit/${data}" class="btn btn-sm btn-primary">Edit</a>
                        <a href="/admin/product/delete/${data}" class="btn btn-sm btn-danger">Delete</a>
                    `;
                },
                width: "20%"
            }
        ]
    });
}
