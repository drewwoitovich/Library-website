$(document).ready(function () {
    Date.prototype.toDateInputValue = (function () {
        var local = new Date(this);
        local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
        return local.toJSON().slice(0, 10);
    });

    $(DateTimeSearchInput).hide();
    $('#NewSearch').prop('checked', false);

    $('#NewSearch').change(function () {
        if ($("#NewSearch").is(":checked")) {
            $(DateTimeSearchInput).show();
            $(DateTimeSearchInput).val(new Date().toDateInputValue());
        }
        else {
            $(DateTimeSearchInput).hide();
        }
    });
});