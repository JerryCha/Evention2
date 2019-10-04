// Event part.
class Event {
    constructor() {
    }
}


// MapBox part.
mapboxgl.accessToken = 'pk.eyJ1IjoiamVycnljaGEiLCJhIjoiY2sxNXNldmdmMHlibjNjdGM4MnAyZHR4aCJ9.OjElwhEEogXkUfGOgpX3mA';
function queryCoordinate(address) {
    var geocodingClient = mapboxSdk({ accessToken: mapboxgl.accessToken });
    geocodingClient.forwardGeocode({
        query: '11 Bond Street, Caulfield North, VIC3161',
        limit: 1
    })
        .send()
        .then(response => {
            console.log(response);
        });
}
