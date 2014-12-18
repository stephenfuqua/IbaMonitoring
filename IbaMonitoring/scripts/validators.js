
/**
 * Validates text entry as time (without AM or PM). Intended for use with ASP.Net custom validator control.
 * @param sender Custom validator that invoked the method.
 * @param args Validation object for the Custom Validator.
 */
function ValidMorningTime(sender, args) {
    args.IsValid = false;

    if (args.Value.length < 5) {
        args.Value = "0" + args.Value;
    }

    // pattern number:number
    var regex = /[0-9]{2}:[0-9]{2}/;
    if (regex.test(args.Value)) {
        // now test to see if first part is a valid hour and second part valid minute
        var hour = parseInt(args.Value.substring(0, 2));
        var minutes = parseInt(args.Value.substring(3, 5));
        if (hour <= 12 && minutes <= 60) {
            args.IsValid = true;
        }
    }

    return;
}

/**
* Validates text entry as a temperature between 0 and 99 Fahrenheit (these are the reasonable/accepted range of values during migration bird monitoring).
* @param sender Custom validator that invoked the method.
* @param args Validation object for the Custom Validator.
*/
function ValidTemperature(sender, args) {
    args.IsValid = false;

    var regex = /[0-9]{1,2}/;
    if (regex.test(args.Value)) {
        var temp = parseInt(args.Value);
        // we don't know which scale is being used right now, so must
        // accept from -18 to 99 degrees
        if (temp >= -18 && temp <= 99) {
            args.IsValid = true;
        }
    }

    return;
}