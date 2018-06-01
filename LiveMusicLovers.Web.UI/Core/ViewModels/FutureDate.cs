using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace LiveMusicLovers.Web.UI.Core.ViewModels
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime datetime;
            var IsValid = DateTime.TryParseExact(Convert.ToString(value),
                "d MMM yyyy",
                new CultureInfo("en-US"),
                DateTimeStyles.None,
                out datetime);

            return (IsValid && datetime > DateTime.Now);
        }
    }
}