$(document).ready(function () {
    $(".select-add-placeholder").prepend("<option value='' disabled selected>Select Organization</option>");

    $("#testBtn").click(function () {
        searchRecords();
    })

    var typingTimer;
    $("#searchTextBox").on('input', function () {
        clearTimeout(typingTimer);
        typingTimer = setTimeout(searchRecords, 500);
    })

    $("#showPastDueChkbx").change(function () {
        TogglePastDue();
    });

    $(".control-group-wrapper").mouseenter(function () {
        $(this).css("box-shadow", "0px 0px 6px 2px lightblue");
    });

    $(".control-group-wrapper").mouseleave(function () {
        $(this).css("box-shadow", "0px 0px 0px 0px #ccc");
    });

    $("#orgDdl").change(function () {
        FilterByOrg();
    });

    $("#minAmtBtn").click(function () {
        alert("This button doesn't work yet. Try again later. \r\n  -Ben, 4/23/19");
    })

    $("#minAmtTxt").on('input', function () {
        ForceNumericInput();
    })
});

function ForceNumericInput() {
    var input = $('#minAmtTxt').val();
    var lastChar = input[input.length - 1];

    if ('01234567890'.indexOf(lastChar) == -1 || input.length > 6) {
        $('#minAmtTxt').val(input.length > 0 ? input.substring(0, input.length - 1) : '');
    }
}

function FilterByOrg() {
    var orgSelected = $("#orgDdl").val().trim();

    $.ajax({
        //url: filterUrl,
        url: '/Home/GetRecordsByOrg?organization=' + orgSelected,
        type: 'Get',
        //data: orgSelected,
        //dataType: 'string',
        success: function (result) {
            $("#tableResults").html(result);
        }
    });

    //$("#tableResults").load('@(Url.Action("GetRecordsByOrg","Home",null, Request.Url.Scheme))?organization=' + orgSelected);
};

var isPastDueChecked = false

function TogglePastDue() {
    var isChecked = $("#showPastDueChkbx").is(':checked');
    isPastDueChecked = isChecked ? true : false;

    $('.ModelStatus[data-status=true]').parent().parent().toggle(!isChecked);
}

function searchRecords() {
    var searchText = $("#searchTextBox").val().toLowerCase();
    var noResultsFound = true;

    $(".panel-table-row").each(function () {
        var data = $(this).html().toLowerCase();

        if (data.indexOf(searchText) > -1) {
            $(this).css("display", "table-row");
        }
        else {
            $(this).css("display", "none");
        }
    })
}