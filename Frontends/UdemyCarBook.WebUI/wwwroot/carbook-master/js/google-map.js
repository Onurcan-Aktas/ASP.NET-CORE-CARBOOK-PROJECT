var google;

function init() {
    // Sayfada 'map' ID'sine sahip bir div olup olmadığını kontrol et
    var mapElement = document.getElementById('map');

    // EĞER HARİTA DIV'I YOKSA (örneğin anasayfadaysak), KODU BURADA DURDUR
    if (!mapElement) {
        return;
    }

    // Basic options for a simple Google Map
    var myLatlng = new google.maps.LatLng(40.69847032728747, -73.9514422416687);

    var mapOptions = {
        // How zoomed in you want the map to start at (always required)
        zoom: 7,

        // The latitude and longitude to center the map (always required)
        center: myLatlng,

        // How you would like to style the map. 
        scrollwheel: false,
        styles: [
            {
                "featureType": "administrative.country",
                "elementType": "geometry",
                "stylers": [
                    {
                        "visibility": "simplified"
                    },
                    {
                        "hue": "#ff0000"
                    }
                ]
            }
        ]
    };

    // Create the Google Map using out element and options defined above
    var map = new google.maps.Map(mapElement, mapOptions);

    var addresses = ['New York'];

    for (var x = 0; x < addresses.length; x++) {
        $.getJSON('http://maps.googleapis.com/maps/api/geocode/json?address=' + addresses[x] + '&sensor=false', null, function (data) {
            var p = data.results[0].geometry.location
            var latlng = new google.maps.LatLng(p.lat, p.lng);
            new google.maps.Marker({
                position: latlng,
                map: map,
                icon: 'images/loc.png' // Not: Eğer resim 404 hatası verirse bunu '/images/loc.png' yapabilirsiniz
            });
        });
    }
}

google.maps.event.addDomListener(window, 'load', init);