using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KPMG_Assessment.Website.Models;
using KPMG_Assessment.Website.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KPMG_Assessment.Website.Controllers
{
    [Produces("application/json")]
    [Route("api/Transaction")]
    public class TransactionController : Controller
    {
        private readonly IFileReaderService _excelFileReader;

        public TransactionController(IFileReaderService excelFileReader)
        {
            this._excelFileReader = excelFileReader;
        }

        /// <summary>
        /// Not implementing validation and data annotation in transaction model because it will slow down 
        /// when payload it big
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromBody]TransactionModel[] transactions)
        {
            if (transactions == null || transactions.Length == 0)
                return BadRequest();

            var results = await this._excelFileReader.SaveTransactions(transactions);

            return Ok(new {response=results });
        }


        /// <summary>
        /// Not implementing validation and data annotation in transaction model because it will slow down 
        /// when payload it big
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("gettransactions")]
        public async Task<IActionResult> GetTransactions(int pageIndex = 1, int pageSize = 5)
        {
            var totalRowCount = 0;

            var transactions =  this._excelFileReader.TransactionList(pageIndex, pageSize, out totalRowCount);

            return PartialView("_ViewTransactions", transactions);
        }
    }
}