
function initMap() {
    if (modalId != undefined) {
        var modal = $('#' + modalId);
        var mapDiv = $(modal).find('.map')[0]; 
        const originalMapCenter = new google.maps.LatLng(30.0349136, 31.2177533);

        const map = new google.maps.Map(mapDiv, {
            zoom: 16,
            center: originalMapCenter,
            zoomControl: true,
            zoomControlOptions: {
                position: google.maps.ControlPosition.TOP_LEFT
            }
        });
        var markerImage = 'http://maps.google.com/mapfiles/kml/paddle/red-circle.png';
        var marker = null;
        if (modal.find("form").length > 0) {
            let latInput = $(modal).find('form').serializeArray().find(a => a.name === "Latitude").value;
            let lngInput = $(modal).find('form').serializeArray().find(a => a.name === "Longitude").value;
            if (latInput != undefined && latInput != null && lngInput != undefined && lngInput != null) {
                if (latInput != 0 && lngInput != 0) {
                    let pos = new google.maps.LatLng(parseFloat(latInput), parseFloat(lngInput));
                    marker = new google.maps.Marker({
                        map: map,
                        icon: markerImage,
                        position: pos,
                        animation: google.maps.Animation.DROP,
                    });
                    map.center = pos;
                }
            }
        }
        else{
            let latInput = $("#latitude").val();
            let lngInput = $("#longitude").val();
            if (latInput != undefined && latInput != null && lngInput != undefined && lngInput != null) {
                if (latInput != 0 && lngInput != 0) {
                    let pos = new google.maps.LatLng(parseFloat(latInput), parseFloat(lngInput));
                    marker = new google.maps.Marker({
                        map: map,
                        icon: markerImage,
                        position: pos,
                        animation: google.maps.Animation.DROP,
                    });
                    map.center = pos;
                }
            }
        }

        function setMarkerPosition(lat, lng) {
            let pos = new google.maps.LatLng(parseFloat(lat), parseFloat(lng));
            marker = new google.maps.Marker({
                map: map,
                icon: markerImage,
                position: pos,
                animation: google.maps.Animation.DROP
            });
            map.panTo(pos);
        }

        $('#latitude').on('input', function () {
            if (marker != null) {
                marker.setMap(null);
            }
            var lat = $(this).val();
            var lng = $('#longitude').val();
            if (lat && lng) {
                setMarkerPosition(lat, lng);
            }
        });

        // Event listener for longitude input
        $('#longitude').on('input', function () {
            if (marker != null) {
                marker.setMap(null);
            }
            var lat = $('#latitude').val();
            var lng = $(this).val();
            if (lat && lng) {
                setMarkerPosition(lat, lng);
            }
        });

        // Set marker to originalMapCenter if map is not clicked
        map.addListener('idle', function () {
            if (marker === null) {
                marker = new google.maps.Marker({
                    map: map,
                    icon: markerImage,
                    position: originalMapCenter,
                    animation: google.maps.Animation.DROP,
                });
            }
        });

        // Create the search box and link it to the UI element.
        let pacinput = "<input id='pac-input' class='pac-input controls' type='text'/>";
        $('#mapdialog').append(pacinput);
        let input = document.getElementById('pac-input');
        //let input = $(modal).find('.pac-input')[0];
        if (input != null) {

            const searchBox = new google.maps.places.SearchBox(input);

            map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

            // Bias the SearchBox results towards current map's viewport.
            map.addListener("bounds_changed", () => {
                searchBox.setBounds(map.getBounds());
            });

            // Listen for the event fired when the user selects a prediction and retrieve
            // more details for that place.
            searchBox.addListener("places_changed", () => {
                const places = searchBox.getPlaces();

                if (places.length == 0) {
                    return;
                }

                // Clear out the old marker.
                if (marker != null) {
                    marker.setMap(null);
                }
                marker = null;

                // For first place, get the icon, name and location.
                const bounds = new google.maps.LatLngBounds();
                if (!places[0].geometry || !places[0].geometry.location) {
                    console.log("Returned place contains no geometry");
                    return;
                }
                marker = new google.maps.Marker({
                    map: map,
                    icon: markerImage,
                    title: places[0].name,
                    position: places[0].geometry.location,
                    animation: google.maps.Animation.DROP,
                });

                if (places[0].geometry.viewport) {
                    // only geocodes have viewport.
                    bounds.union(places[0].geometry.viewport);
                } else {
                    bounds.extend(places[0].geometry.location);
                }
                var lat = places[0].geometry.location.lat();
                var lng = places[0].geometry.location.lng();
                $('#latitude').val(lat);
                $('#longitude').val(lng);
                map.fitBounds(bounds);
            });
        }

        //Marker on the map
        map.addListener('click', function (event) {
            if (marker != null) {
                marker.setMap(null);
            }

            marker = new google.maps.Marker({
                map: map,
                icon: markerImage,
                position: event.latLng,
                animation: google.maps.Animation.DROP,
            });
            let lat = event.latLng.lat();
            let lng = event.latLng.lng();
            $('#latitude').val(lat);
            $('#longitude').val(lng);
            marker.setMap(map);
        })

        
    }
}