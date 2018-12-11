using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KPMG_Assessment.Website.Models
{
    public class TransactionResponseModel
    {      
        public long rowNumber { get; set; }
        public IList<ValidationResult> errors { get; set; }
    }
}
