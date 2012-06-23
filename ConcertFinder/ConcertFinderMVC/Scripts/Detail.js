$(document).ready(function () {
    function initialize() {
        var myLatlng = new google.maps.LatLng(latitude, longitude);
        var myOptions = {
            zoom: 15,
            center: myLatlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        }
        var map = new google.maps.Map(document.getElementById("map"), myOptions);

        var marker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            title: salle
        });
    }

    $("#direction").click(function (){
        
        var directionsDisplay = new google.maps.DirectionsRenderer();
        var eventLatlng = new google.maps.LatLng(latitude, longitude);
            
        var myOptions = {
            zoom: 6,
            center: eventLatlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById('map'), myOptions);

        directionsDisplay.setMap(map);

        if (navigator.geolocation)
        {
            navigator.geolocation.getCurrentPosition(function(position)
            {
                var directionsService = new google.maps.DirectionsService();
                var pos = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);

                var request = {
                    origin:pos, 
                    destination:eventLatlng,
                    travelMode: google.maps.DirectionsTravelMode.DRIVING
                };
                directionsService.route(request, function(response, status)
                {
                    if (status == google.maps.DirectionsStatus.OK)
                    {
                        directionsDisplay.setDirections(response);
                    }
                });
            }, function()
            {
                handleNoGeolocation(true);
            });
        }
        else
        {
            // Browser doesn't support Geolocation
            handleNoGeolocation(false);
        }
    });

    initialize();
});