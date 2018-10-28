using System.Text.RegularExpressions;

namespace Artists.Subscribers.Data.Validation
{
    public static class ValidationMetods
    {

        public static bool ValidSubscriberEmail(string mail)
        {
            string pattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            bool result = Regex.IsMatch(mail ?? "", pattern) ? true : false;
            return result;
        }

        public static bool ValidSubscriberName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                string input = name.Trim();
                bool result = (!NumbersInString(name) && !SpecialChar(name)) ? true : false;
                return result;
            }
            return false;
        }

        // Numbers exsist in string
        private static bool NumbersInString(string str)
        {
            Regex regNum = new Regex(@"\d");
            bool result = regNum.IsMatch(str) ? true : false;
            return result;
        }
        //Special Characters exsist in string
        private static bool SpecialChar(string str)
        {
            Regex regCharSpecial = new Regex("[^a-zA-Z0-9_ ]+");
            bool SpecialCar = regCharSpecial.IsMatch(str) ? true : false;
            if (SpecialCar) return true;
            else return false;
        }
    }
}
