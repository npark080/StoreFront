const successStyle = 'alert alert-success text-center'
const failStyle = 'alert alert-danger text-center'


// #region AJAX DELETE
function deleteConfirmed(response) {
    // Target the row
    let rowId = '#category-' + response.id;
    console.log(rowId);
    // Target the whole table and remove the row
    $('#categoriesTable').find(rowId).remove();
    // Display a status message
    $('#messageContent').removeClass().addClass(successStyle).text(response.message);
}
function deleteFailed() {
    $('#messageContent').removeClass().addClass(failStyle).text('Delete unsuccessful');
}
// #endregion

// #region AJAX DETAILS
$('.detailsLink').on('click', function () {
    let catId = $(this).attr('id');
    $('#detailsBody').load(`/Categories/Details/${catId}`);
});
// #endregion

// #region AJAX CREATE
$('#categoryCreate').hide();
$('#toggleCategoryCreate').on('click', function () {
    $('#categoryCreate').toggle();
});

// AJAX Process and submit
$('#createForm').on('submit', function (e) {
    e.preventDefault();
    let formData = $(this).serializeArray();

    $.ajax({
        url: '/Categories/Create',
        type: 'POST',
        data: formData,
        dataType: 'json',
        error: function () {
            $('#messageContent').removeClass().addClass(failStyle).text('There was a problem...');
        },
        success: function (category) {
            $('#messageContent').removeClass().addClass(successStyle).text('Category added!');
            $('#createForm')[0].reset();
            $(function () {
                let row =
                    `<tr id="category-${category.CategoryId}">
                        <td>${category.categoryName}</td>
                        <td>${category.categoryDescription}</td>
                        <td>Refresh Page for Options</td>
                    </tr>`
                $('#categoriesTable').append(row);
                $('#categoryCreate').hide();
            });
        }
    });
});
// #endregion

// #region AJAX EDIT
// GET
let originalContent = null;
$('a.editLink').on('click', function (e) {
    e.preventDefault();
    let categoryId = $(this).attr('id');
    let row = $(`#category-${categoryId}`).children();
    originalContent = {
        CatId: categoryId,
        CatName: row[0].innerText,
        CatDesc: row[1].innerText,
    };
    $.get(`/Categories/Edit/${categoryId}`, function (data) {
        $(`#category-${categoryId}`).replaceWith(data)
    });
});

// POST
$(document).on('click', '#saveUpdate', function (e) {
    e.preventDefault();
    let formData = $('#editForm').serializeArray();
    $('#messageContent').removeClass().addClass('alert alert-info text-center').text("Please wait...");

    $.ajax({
        url: '/Categories/Edit',
        type: 'POST',
        data: formData,
        dataType: 'json',
        success: function (data) {
            $('#messageContent').removeClass().addClass(successStyle).text('Your record was successfully updated!');
            $('#editForm')[0].reset();
            $(function () {
                let row =
                    `<tr id="category-${data.categoryId}">
                        <td>${data.categoryName}</td>
                        <td>${data.categoryDescription}</td>
                        <td>Refresh Page for Options</td>
                        </tr>`;
                $(`#category-${data.categoryId}`).replaceWith(row);
            })
        },
        error: function (data) {
            console.log(data.responseText)
            $('#messageContent').removeClass().addClass(failStyle).text('There was an error.  Please try again or contact a site administrator.');
            $(function () {
                let row =
                    `<tr>
                        <td>${originalContent.CatName}</td>
                        <td>${originalContent.CatDesc}</td>
                        <td>Refresh Page for Options</td>
                        </tr>`;
                $(`#category-${originalContent.CatId}`).replaceWith(row);
            });
        }
    });
});
// #endregion
