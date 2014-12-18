// Assumes that prototype.js has already been included

function displayBlock(elementname) {
	var el = document.getElementById(elementname);
	el.style.display = "block";
//	($(elementname)).show();
}

function hideBlock(elementname) {
	var el = document.getElementById(elementname);
	el.style.display = "none";
//	($(elementname)).hide();
}
