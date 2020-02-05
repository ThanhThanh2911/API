var category = category || {};

category.drawTable = function () {
    categorytable = $("#tbCategory").DataTable({
        "processing": true, // for show progress bar  
        "serverSide": true, // for process server side  
        "filter": true, // this is for disable filter (search box)  
        "orderMulti": false, // for disable multiple column at once  
        "ajax": {
            "url": "/category/GetData",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "id",
                "name": "ID",
                "autoWidth": true,
                "title": "ID"
            },
            {
                "data": "categoryName",
                "name": "CategoryName",
                "autoWidth": true,
                "title": "Category Name"
            },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a href='javascript:void(0);' onclick=category.getDetail('" + data.id + "')><i class='fas fa-edit'></i></a> " +
                           "<a href='javascript:void(0);' asp-area='' asp-controller='Brand' asp-action='GetAllBrandsForCategory' asp-route-Id='" + data.id + "'>Danh sách Brand</i></a> " +
                           "<a href='javascript:void(0);' onclick=category.delete('" + data.id + "')><i class='far fa-trash-alt'></i></a> ";
                },
                "sortable": false
            },
        ]

    });
};

category.openAddEditModal = function () {
    category.resetForm();
    $('#addEditCategory').modal('show');
};

category.initParens = function () {
    $.ajax({
        url: '/category/GetParens',
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.status === 1) {
                var response = data.response;
                $.each(response, function (index, value) {
                    $('#ClassName').append(
                        "<option value = '" + value.id + "'>" + value.name + "</option>"
                    );
                });
            }
        }
    });
};

category.save = function () {
    var categoryObj = {};
    categoryObj.ID = $('#ID').val();
    categoryObj.categoryName = $('#CategoryName').val();

    $.ajax({
        url: '/category/SaveCategory',
        method: 'POST',
        data: JSON.stringify(categoryObj),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.status === 1) {
                $('#addEditCategory').modal('hide');
                category.reloadTable();
                bootbox.alert(data.message);
            }
        }
    });
};


category.getDetail = function (id) {
    $.ajax({
        url: '/category/GetCategoryById/' + id,
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.status === 1) {
                var response = data.response;

                $('#ID').val(response.id);
                $('#CategoryName').val(response.categoryName);
            }
            $('#addEditCategory').find('.modal-title').text('Update Category');
            $('#addEditCategory').modal('show');
        }
    });
};

category.delete = function (id) {
    bootbox.confirm({
        message: "Are you sure to delete?",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: '/category/DeleteCategory/' + id,
                    method: 'GET',
                    dataType: 'json',
                    contentType: 'application/json',
                    success: function (data) {
                        if (data.status === 1) {
                            category.reloadTable();
                        }
                    }
                });
            }
        }
    });

};

category.reloadTable = function () {
    categorytable.ajax.reload(null, false);
};

category.resetForm = function () {
    $('#ID').val('0');
    $('#CategoryName').val('');

    $('#addEditCategory').find('.modal-title').val('Create Category');
};

category.init = function () {
    category.drawTable();
    category.resetForm();
};

$(document).ready(function () {
    category.init();
});