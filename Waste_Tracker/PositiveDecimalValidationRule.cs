using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Waste_Tracker
{
    class PositiveDecimalValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal num = 0;

            if(num < 0)
            {
                return new ValidationResult(false, String.Format("input must be a postitive number"));
            }
            return new ValidationResult(true, null);

        }
    }
}
