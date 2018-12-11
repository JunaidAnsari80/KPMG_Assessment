using KPMG_Assessment.Website.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace KPMG_Assessment.Website.Attributes
{
    /// <summary>
    /// To validate ISO 4217 CurrenyCode
    /// </summary>
    /// 
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class CurrencyCodeAttribute : ValidationAttribute
    {
         /// <summary>
        /// override to perform validation
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            var currecnyCode = (String)value;
            bool result = true;
            if (currecnyCode != null)
            {
                result = IsValidCurrencyCode(currecnyCode);
            }
            return result;
        }

        /// <summary>
        /// Returning true and faulse if currency code matches with any region
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <returns></returns>
        internal bool IsValidCurrencyCode(string currencyCode)
        {
            ISO4217 code;
           return ISO4217.TryParse(currencyCode, out code);
         
        }

        /// <summary>
        /// returnin error message
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
                                ErrorMessageString, 
                                name);
        }
    }
}
