$(document).ready(function () {
    $(DateTimeSearchInput).hide();
    $('#NewSearch').prop('checked', false);

    $('#NewSearch').change(function () {
        if ($("#NewSearch").is(":checked")) {
            $(DateTimeSearchInput).show();
        }
        else {
            $(DateTimeSearchInput).hide();
        }
    });
});