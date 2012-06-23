$(document).ready(function () {
    function getLatLong(address) {
        var geo = new google.maps.Geocoder;
        geo.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                var latlong = results[0].geometry.location
                $("input#Latitude").val((latlong.lat() + "").replace(".", ","));
                $("input#Longitude").val((latlong.lng() + "").replace(".", ","));
                return results[0].geometry.location;
            }
            else {
                alert("Geocode was not successful for the following reason: " + status);
            }
        });
    };


    $("input#address, input#city, input#code_postal, input#country").focusout(function () {
        if ($("input#address").val() != "" && $("input#city").val() != "" && $("input#code_postal").val() != "" && $("input#country").val() != "") {
            var address = $("input#address").val() + "," + $("input#city").val() + "," + $("input#code_postal").val() + "," + $("input#country").val();
            var latlong = getLatLong(address);
        }
    });

    $("#startDate, #endDate").datetimepicker({
        monthNames: ['Janvier', 'Février', 'Mars', 'Avril', 'Mai', 'Juin', 'Juillet',
                                'Août', 'Septembre', 'Octobre', 'Novembre', 'Décembre'],
        monthNamesShort: ['Jan', 'Fév', 'Mar', 'Avr', 'Mai', 'Jun',
                                    'Jul', 'Aoû', 'Sep', 'Oct', 'Nov', 'Déc'],
        dayNames: ['Dimanche', 'Lundi', 'Mardi', 'Mercredi',
                            'Jeudi', 'Vendredi', 'Samedi'],
        dayNamesMin: ['Dim', 'Lun', 'Mar', 'Mer', 'Jeu', 'Ven', 'Sam'],
        dayNamesShort: ['Dim', 'Lun', 'Mar', 'Mer', 'Jeu', 'Ven', 'Sam'],
        dateFormat: "dd/mm/yy",
        showOtherMonths: true,
        selectOtherMonths: true,
        showAnim: "fold",
        currentText: "Maintenant",
        closeText: "Valider",
        hourText: "Heure",
        timeText: "Horaire",
        stepMinute: 5
    });
});