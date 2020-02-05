var brand = brand || {};

brand.drawTable = function () {
    brandtable = $("#tbBrand").DataTable({
        "processing": true, // for show progress bar  
        "serverSide": true, // for process server side  
        "filter": true, // this is for disable filter (search box)  
        "orderMulti": false, // for disable multiple column at once  
        "ajax": {
            "url": "/brand/GetData",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "brandName",
                "name": "BrandName",
                "autoWidth": true,
                "title": "Brand Name"
            },
            {
                "data": "parenID",
                "name": "ParenID",
                "autoWidth": true,
                "title": "Paren ID"
            },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a href='javascript:void(0);' onclick=brand.getDetail('" + data.id + "')><i class='fas fa-edit'></i></a> " +
                        "<a href='javascript:void(0);' onclick=brand.delete('" + data.id + "')><i class='far fa-trash-alt'></i></a> ";
                },
                "sortable": false
            },
        ]

    });
};

brand.openAddEditModal = function () {
    brand.resetForm();
    $('#addEditBrand').modal('show');
};

brand.initClasses = function () {
    $.ajax({
        url: '/brand/GetClasses',
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

brand.save = function () {
    var brandObj = {};
    brandObj.ID = $('#ID').val();
    brandObj.BrandName = $('#BrandName').val();
    brandObj.ParenID = $('#ParenID').val();

    $.ajax({
        url: '/brand/SaveBrand',
        method: 'POST',
        data: JSON.stringify(brandObj),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.status === 1) {
                $('#addEditBrand').modal('hide');
                brand.reloadTable();
                bootbox.alert(data.message);
            }
        }
    });
};


brand.getDetail = function (id) {
    $.ajax({
        url: '/brand/GetBrandById/' + id,
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.status === 1) {
                var response = data.response;

                $('#ID').val(response.id);
                $('#BrandName').val(response.brandName);
                $('#ParenID').val(response.parenID);
            }
            $('#addEditBrand').find('.modal-title').text('Update Brand');
            $('#addEditBrand').modal('show');
        }
    });
};

brand.delete = function (id) {
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
                    url: '/brand/DeleteBrand/' + id,
                    method: 'GET',
                    dataType: 'json',
                    contentType: 'application/json',
                    success: function (data) {
                        if (data.status === 1) {
                            brand.reloadTable();
                        }
                    }
                });
            }
        }
    });

};

brand.reloadTable = function () {
    brandtable.ajax.reload(null, false);
};

brand.resetForm = function () {
    $('#ID').val('0');
    $('#BrandName').val('');
    $('#ParenID').val('1');

    $('#addEditBrand').find('.modal-title').text('Create Brand');
};

brand.init = function () {
    brand.drawTable();
    brand.resetForm();
};

$(document).ready(function () {
    brand.init();
});