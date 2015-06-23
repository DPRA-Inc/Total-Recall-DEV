function createIcon(lat, lon, tooltip, icon)
{

	var loc = ol.proj.transform([lon, lat], 'EPSG:4326', 'EPSG:3857');

	var iconFeature = new ol.Feature({
		geometry: new ol.geom.Point(loc),
		name: tooltip
	});


	var iconStyle = new ol.style.Style({
		image: new ol.style.Icon(/** @type {olx.style.IconOptions} */({
			anchor: [0.5, 0.5],
			anchorXUnits: 'fraction',
			anchorYUnits: 'fraction',
			opacity: 0.75,
			src: icon
		}))
	});

	iconFeature.setStyle(iconStyle);

	return iconFeature;

};

var vectorSource = new ol.source.Vector();

var vectorLayer = new ol.layer.Vector({
	source: vectorSource
});

var rasterLayer = new ol.layer.Tile({
	source: new ol.source.OSM()
});

var view = new ol.View({
	center: ol.proj.transform([-84, 36], 'EPSG:4326', 'EPSG:3857'),
	zoom: 5
})

function centerMapOnAllObjects()
{
	var extent = vectorLayer.getSource().getExtent();
	map.getView().fitExtent(extent, map.getSize());
}