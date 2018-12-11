using KPMG_Assessment.Website.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KPMG_Assessment.Website.Models
{
    public class TransactionModel
    {
        public long rowno { get; set; }

        [Required]
        [RegularExpression(@"^(\d*\.)?\d+$", ErrorMessage ="Invalid Decmal Amount")]
        public string amount { get; set; }
        [Required]
        public string account { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        [CurrencyCode(ErrorMessage ="Invalid CurrencyCode")]
        public string currencyCode { get; set; }
    }
}
