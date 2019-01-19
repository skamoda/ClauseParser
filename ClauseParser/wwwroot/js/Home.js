$('#step-submit-button').on('click', function () {
    var data = $('#step-submit-input').val();
    $.ajax({
        method: "POST",
        url: "Home/Parse",
        data: { text: data },
        dataType: "json"
    })
        .success(function () {
            alert("Data Saved: ");
        });
});

$('#button-panel').find('button').each(function () {
    $(this).click(function () {
        var textField = $('#step-submit-input');
        var caretPosition = textField.caret().begin;
        var text = textField.val();
        textField.val(text.substr(0, caretPosition) + $(this).html() + text.substr(caretPosition));
        textField.focus();
        textField.caret(caretPosition + 1);
    });
});