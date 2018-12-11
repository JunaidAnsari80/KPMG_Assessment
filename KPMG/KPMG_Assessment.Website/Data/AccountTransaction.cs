using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPMG_Assessment.Website.Data
{
    public class AccountTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string Account { get; set; }
        [Required]
       
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        [Required]      
        public string Description { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
    }
}
