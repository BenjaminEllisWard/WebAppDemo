$(document).ready(function () {
    $("#testBtn").click(function () {
        searchRecords();
    })


    var typingTimer;
    $("#searchTextBox").on('input', function () {
        clearTimeout(typingTimer);
        typingTimer = setTimeout(searchRecords, 500);
    })
})

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