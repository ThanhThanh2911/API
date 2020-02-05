var product = product || {};

product.drawTable = function () {
    producttable = $("#tbProduct").DataTable({
        "processing": true, // for show progress bar  
        "serverSide": true, // for process server side  
        "filter": true, // this is for disable filter (search box)  
        "orderMulti": false, // for disable multiple column at once  
        "ajax": {
            "url": "/Product/GetProducts",
            "type": "POST",
            "datatype": "json"
        },
        //"columnDefs":
        //    [{
        //        "targets": [0],
        //        "visible": false,
        //        "searchable": false
        //    }],
        "columns": [
            {
                "data": "productName",
                "name": "ProductName",
                "autoWidth": true,
                "title": "Tên sản phẩm"

            },
            {
                "data": "price",
                "name": "Price",
                "autoWidth": true,
                "title": "Giá cả"
            },
            {
                "data": "brandName",
                "name": "BrandName",
                "autoWidth": true,
                "title": "Nhãn hiệu"
            },
            {
                "data": "categoryName",
                "name": "CategoryName",
                "autoWidth": true,
                "title": "Loại sản phẩm"
            },
            //{
            //    "render": function (data, type, full, meta) { return '<a class="btn btn-info" href="/DemoGrid/Edit/' + full.CustomerID + '">Edit</a>'; }
            //},
            {
                data: "id",
                render: function (data, type, row) {
                    return "<a href='javascript:void(0);' class='btn btn-outline-dark' onclick=product.getDetail('" + data + "')>Sửa</a>" +
                        "<a href='javascript:void(0);' class='btn btn-outline-danger' onclick=product.delete('" + data + "')>Xóa</a>"
                },
                "sortable": false
            },
        ]

    });
};


product.openAddEditModal = function () {
    $('#addEditModel').modal('show');
}

//product.initBrand = function () {

//};

product.save = function () {
    //if ($('#formAddEditProduct').valid()) {
    var productObj = {};
    productObj.ProductName = $('#ProductName').val();
    productObj.Price = $('#Price').val();
    productObj.CategoryID = $('#CategoryName').val();
    productObj.BrandID = $('#BrandName').val();
    productObj.ID = $('#ID').val();

    $.ajax({
        url: '/Product/SaveProduct',
        method: 'POST',
        data: JSON.stringify(productObj),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.status === 1) {
                $('#addEditModel').modal('hide');
                bootbox.alert(data.message);
                window.location.href = "/Product/Index";
                //student.reloadTable();
            }
        }
    });
    //}
};

product.initCategory = function () {
    $.ajax({
        url: '/Product/ListCategory',
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.status === 1) {
                var response = data.response;
                $.each(response, function (index, value) {
                    $('#CategoryName').append(
                        "<option value = '" + value.id + "'>" + value.categoryName + "</option>"
                    );
                });
            }
        }
    });
};

product.initBrand = function () {
    $.ajax({
        url: '/Product/ListBrand',
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.status === 1) {
                var response = data.response;
                $.each(response, function (index, value) {
                    $('#BrandName').append(
                        "<option value = '" + value.id + "'>" + value.brandName + "</option>"
                    );
                });
            }
        }
    });
};

product.getDetail = function (id) {
    $.ajax({
        url: '/Product/GetProductById/' + id,
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.status === 1) {
                var response = data.response;

                $('#ProductName').val(response.productName);
                $('#Price').val(response.price);
                $('#CategoryName').val(response.categoryID);
                $('#BrandName').val(response.brandID);
                $('#ID').val(response.id);
            }
            $('#addEditModel').find('.modal-title').text('Chỉnh sửa sản phẩm');
            $('#addEditModel').modal('show');
        }
    });
};

product.delete = function (id) {
    bootbox.confirm("Are you sure to delete?", function (result) {
        if (result) {
            $.ajax({
                url: '/Product/DeleteProduct/' + id,
                method: 'GET',
                dataType: 'json',
                contentType: 'application/json',
                success: function (data) {
                    if (data.status === 1) {
                        product.reloadTable();
                    }
                }
            });
        }
    });
};

product.reloadTable = function () {
    producttable.ajax.reload(null, false)
};

//product.resetForm = function () {
//    $('#ProductName').val('');
//    $('#Price').val('');
//    $('#CategoryName').val('1');
//    $('#BrandID').val('1');
//    $('#ID').val('0');
//    $('#addEditModel').find('.modal-title').text('Tạo sản phẩm mới');
//    $('#formAddEditProduct').validate().resetForm();
//}

product.init = function () {
    product.drawTable();
    product.initCategory();
    product.initBrand();
};

$(document).ready(function () {
    product.init();
});