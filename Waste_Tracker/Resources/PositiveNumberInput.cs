using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Waste_Tracker
{
    class PositiveNumberInput : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal LeftOver;
            if(decimal.TryParse(value.ToString(), out LeftOver))
            { 
                return new ValidationResult(false, "Please enter a number for the Left Over Value");
            }
            else if (LeftOver < 0)
            {
                return new ValidationResult(false, "Please enter a positive number for the Left Over Value");
            }
            else
            { 
            return new ValidationResult(true, null);
            }
        }
    }
}
