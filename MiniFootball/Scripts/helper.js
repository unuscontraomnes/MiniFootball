// Calendar
$(function () {
	$('.datepicker').datepicker();
});


// Google Maps
$(document).ready(function () {
	if (window.location.href.indexOf("pitch") !== -1) {
		Initialize();
	}
});

function Initialize() {

	google.maps.visualRefresh = true;
	var fitnessHouse = new google.maps.LatLng(59.961397, 30.286913);

	var mapOptions = {
		zoom: 14,
		center: fitnessHouse,
		mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
	};

	var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);

	var marker = new google.maps.Marker({
		position: fitnessHouse,
		map: map,
		title: 'Our pitch'
	});
}