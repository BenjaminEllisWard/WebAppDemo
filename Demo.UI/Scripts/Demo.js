//var isPastDueChecked

$(document).ready(function () {
    $(".select-add-placeholder").prepend("<option value='' disabled selected>Select Organization</option>");

    //Associates a timer with input for searching records.
    var typingTimer;
    $("#searchTextBox").on('input', function () {
        clearTimeout(typingTimer);
        typingTimer = setTimeout(searchRecords, 500);
    })

    //Checkbox filter is written into record search function.
    $("#showPastDueChkbx").on('input', function () {
        searchRecords();
    });

    $("#orgDdl").change(function () {
        FilterByOrg();
    });

    $("#minAmtBtn").click(function () {
        alert("This button doesn't work yet. Try again later. \r\n  -Ben, 4/23/19");
    });

    $("#recordCountBtn").click(function () {
        alert("This button doesn't work yet. Try again later. \r\n  -Ben, 4/26/19");
    });

    $("#minAmtTxt").on('input', function () {
        ForceNumericInput();
    });
});

function ForceNumericInput() {
    var input = $('#minAmtTxt').val();
    var lastChar = input[input.length - 1];

    //Brute force way of restricting user to numeric input of less than 7 digits. Does not work against pasted input.
    if ('01234567890'.indexOf(lastChar) == -1 || input.length > 6) {
        $('#minAmtTxt').val(input.length > 0 ? input.substring(0, input.length - 1) : '');
    }
}

function FilterByOrg() {
    var orgSelected = $("#orgDdl").val().trim();

    //TODO make this work without hardcoding url.
    $.ajax({
        url: '/Home/GetRecordsByOrg?organization=' + orgSelected,
        type: 'Get',
        success: function (result) {
            $("#tableResults").html(result);
        }
    });
};

function searchRecords() {
    var searchText = $("#searchTextBox").val().toLowerCase();
    var isPastDueChecked = $('#showPastDueChkbx').is(':checked');

    //Check each row
    $(".panel-table-row").each(function () {
        var data = $(this).html().toLowerCase();

        if (data.indexOf(searchText) > -1) {

            if (isPastDueChecked && $(this).find('.ModelStatus').data('status') == 'True') {
                $(this).css('display', 'none');
            }
            else {
                $(this).css("display", "table-row");
            }
        }
        else {
            $(this).css("display", "none");
        }
    });
};