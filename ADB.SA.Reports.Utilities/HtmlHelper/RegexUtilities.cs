using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ADB.SA.Reports.Utilities.HtmlHelper
{
    public class RegexUtilities
    {
        static Regex ValidEmailRegex = CreateValidEmailRegex();

        /// <summary>
        /// Taken from http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
        /// </summary>
        /// <returns></returns>
        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        public static bool EmailIsValid(string emailAddress)
        {
            bool isValid = ValidEmailRegex.IsMatch(emailAddress);

            return isValid;
        }

        //bool invalid = false;

        //public bool IsValidEmail(string strIn)
        //{
        //    invalid = false;
        //    if (String.IsNullOrEmpty(strIn))
        //        return false;

        //    // Use IdnMapping class to convert Unicode domain names. 
        //    try
        //    {
        //        strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
        //                              RegexOptions.None, TimeSpan.FromMilliseconds(200));
        //    }
        //    catch (RegexMatchTimeoutException)
        //    {
        //        return false;
        //    }

        //    if (invalid)
        //        return false;

        //    // Return true if strIn is in valid e-mail format. 
        //    try
        //    {
        //        return Regex.IsMatch(strIn,
        //              @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        //              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,24}))$",
        //              RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        //    }
        //    catch (RegexMatchTimeoutException)
        //    {
        //        return false;
        //    }
        //}

        //private string DomainMapper(Match match)
        //{
        //    // IdnMapping class with default property values.
        //    IdnMapping idn = new IdnMapping();

        //    string domainName = match.Groups[2].Value;
        //    try
        //    {
        //        domainName = idn.GetAscii(domainName);
        //    }
        //    catch (ArgumentException)
        //    {
        //        invalid = true;
        //    }
        //    return match.Groups[1].Value + domainName;
        //}
    }
}

