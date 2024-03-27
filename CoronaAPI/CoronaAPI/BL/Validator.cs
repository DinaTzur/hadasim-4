using CoronaAPI.Models;
using System.Text.RegularExpressions;

namespace CoronaAPI.BL
{
    public class Validator
    {
        public static bool ValidateString(StringValidateModel field)
        {
            if (field.Required && string.IsNullOrWhiteSpace(field.Value) )
            {
                return false;
            }
            else if(string.IsNullOrWhiteSpace(field.Value))
            {
                return true;
            }

            if(field.MinLength.HasValue && field.Value.Length < field.MinLength)
            {
                return false;
            }

            if(field.MaxLength.HasValue && field.Value.Length > field.MaxLength)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(field.Pattern))
            {
                return Regex.IsMatch(field.Value, field.Pattern);
            }

            return true;
        }

        public static bool ValidateDateRange(DateTime firstDate, DateTime lastDate)
        {
            return firstDate <= lastDate;
        }

        public static bool IsValidIsraeliId(int id)
        {
            string idNumber = id.ToString().PadLeft(9, '0');

            // Calculate the check digit using the specified algorithm
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                int digit = int.Parse(idNumber[i].ToString());
                int factor = (i % 2 == 0) ? 1 : 2;
                int product = digit * factor;
                sum += (product > 9) ? product - 9 : product;
            }

            return (sum % 10 == 0);
        }
    }
}
