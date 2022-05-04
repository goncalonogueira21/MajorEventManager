function loadBingMap(latitude,longitude) {
    console.log(latitude,longitude);
    var map = new Microsoft.Maps.Map(document.getElementById('map'), {
        center: new Microsoft.Maps.Location(latitude,longitude)
    });
    var center = map.getCenter();

    var pushpin = new Microsoft.Maps.Pushpin(center, null);

    map.entities.push(pushpin);

    return "";
}