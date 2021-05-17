$(".changeAccomodationType").click(function () {

    var accomodaationTypeID = $(this).attr("data-id");
    $(".acccomodationTypeRows").hide();
    $(".acccomodationTypeRows[data-id = " + accomodaationTypeID + "]").show();
})