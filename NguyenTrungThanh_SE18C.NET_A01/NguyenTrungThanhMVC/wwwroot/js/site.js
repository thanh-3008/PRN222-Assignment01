$(function () {
    var placeholderElement = $('#form-modal');

    $('body').on('click', 'a[data-bs-toggle="modal"], button[data-bs-toggle="modal"]', function (event) {
        var url = $(this).data('url');
        var title = $(this).data('title');

        // Dọn dẹp nội dung và tiêu đề cũ của modal TRƯỚC KHI gọi AJAX
        placeholderElement.find('.modal-title').text(title);
        placeholderElement.find('.modal-body').html('<p>Loading...</p>'); // Hiển thị chữ loading

        $.get(url).done(function (data) {
            placeholderElement.find('.modal-body').html(data);
            placeholderElement.modal('show');
        }).fail(function () {
            // Nếu có lỗi, hiển thị thông báo trong modal
            placeholderElement.find('.modal-body').html('<div class="alert alert-danger">An error occurred while loading the content. Please try again.</div>');
            placeholderElement.modal('show');
        });
    });

    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();
        var form = $(this).parents('.modal-content').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            if (data.success) {
                placeholderElement.modal('hide');
                location.reload();
            } else {
                placeholderElement.find('.modal-body').html(data);
            }
        }).fail(function () {
            alert('An error occurred while processing your request.');
        });
    });
});