using System.Text.RegularExpressions;

namespace safnet.iba.Static
{
    /// <summary>
    /// Provides functions for custom validation of form controls.
    /// </summary>
    public static class CustomValidation
    {
        /// <summary>
        /// Validates text entry as time (without AM or PM)
        /// </summary>
        /// <param name="input">string value to validate</param>
        /// <returns>bool</returns>
        public static bool ValidateMorning(string input)
        {
            bool isValid = false;

            // Make sure input has a leading zero if before 10:00
            input = input.PadLeft(5, '0');

            // make sure it has the pattern of HH:MM
            string pattern = "^[0-9]{2}:[0-9]{2}$";
            if (Regex.IsMatch(input, pattern))
            {
                // now test to see if first part is a valid hour and second part valid minute
                int hour = int.Parse(input.Substring(0, 2));
                int minutes = int.Parse(input.Substring(3, 2));
                if (hour <= 12 && minutes <= 60)
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        /// <summary>
        /// Validates text entry as a temperature between 0 and 99 Fahrenheit (these
        /// are the reasonable/accepted range of values during migration bird monitoring).
        /// </summary>
        /// <param name="input">Temperature to validate</param>
        /// <param name="scale">Scale - F or C</param>
        /// <returns>bool</returns>
        public static bool ValidateTemperature(string input, string scale)
        {
            int temp;
            bool isValid = false;
            if (int.TryParse(input, out temp))
            {
                int low = 0; int high = 99;
                if (scale == "C")
                {
                    low = -18;
                    high = 37;
                }
                if (temp >= low && temp <= high)
                {
                    isValid = true;
                }
            }
            return isValid;
        }

    }
}