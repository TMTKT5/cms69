function menuClick() {
    if ($('#navigation').hasClass("expanded")) {
        $('#navigation').removeClass("expanded");
    } else {
        $('#navigation').addClass("expanded");
    }
}

function headerColorProfile() {
    $('#body').removeClass().addClass($('#ContentPlaceHolderHeaderKleur_headerKleur').val());
}

function headerColorEdit() {
    $('#body').removeClass().addClass($('#Header').val());
}

function headerColorSwitch(color) {
    $('#Header').val(color);
    headerColorEdit();
}