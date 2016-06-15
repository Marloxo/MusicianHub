using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MusicianHub.ViewModel
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var s = Convert.ToString(value);
            var isValid = DateTime.TryParseExact(s,
                @"d MMM yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dateTime);


            return (isValid && dateTime > DateTime.Now);
        }
    }
}