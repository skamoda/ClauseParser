function StepViewModel(title, top) {
    var self = this;
    //Data
    self.title = title;
    self.top = top;
}

function StepListViewModel(dataRoute) {
    var self = this;
    //Data
    self.steps = ko.observableArray([]);

    //Operations
    self.sendData = function () {
        self.steps.removeAll();
        $.ajax({
            method: "POST",
            url: dataRoute,
            data: { text: $('#step-submit-input').val() },
            dataType: "json",
            success: (function (json) {
                JSON.parse(json).forEach(function(element) {
                    self.steps.push(new StepViewModel(element.Title, element.Top));
                });
            })
        });

    };
}


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