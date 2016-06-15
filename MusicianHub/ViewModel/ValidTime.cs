using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MusicianHub.ViewModel
{
    public class ValidTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            string time = Convert.ToString(value);

            var isValid = DateTime.TryParseExact(time,
                "HH:mm",
                CultureInfo.InvariantCulture,
                //CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out dateTime);

            return (isValid);
        }
    }
}