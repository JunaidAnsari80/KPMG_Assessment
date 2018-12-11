using KPMG_Assessment.Website.Data;
using KPMG_Assessment.Website.Models;
using KPMG_Assessment.Website.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace KPMG_Assessment.Website.Services
{
    public class ExcelFileReaderService : IFileReaderService
    {
        private readonly IRepository<AccountTransaction> _transactionRepository;

        public ExcelFileReaderService(IRepository<AccountTransaction> transactionRepository)
        {
            this._transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Validating and returning rows which failed validation with reasons
        /// </summary>
        /// <param name="transactionModels"></param>
        /// <returns></returns>
        public async Task<List<TransactionResponseModel>> SaveTransactions(IList<TransactionModel> transactionModels)
        {
            var response = new List<TransactionResponseModel>();

            var accountTransactions = new List<AccountTransaction>();

            foreach (var transaction in transactionModels)
            {
                var context = new ValidationContext(transaction, null, null);

                var results = new List<ValidationResult>();

                var transResponse = new TransactionResponseModel();

                transResponse.rowNumber = transaction.rowno;

                //Validating CurrencyCode and Amount and required fields
                if (!Validator.TryValidateObject(transaction, context, results, true))
                {
                    transResponse.errors = results;

                    response.Add(transResponse);
                }
                else
                {
                    accountTransactions.Add(new AccountTransaction
                    {
                        Account = transaction.account,
                        Amount = decimal.Parse(transaction.amount),
                        CurrencyCode = transaction.currencyCode,
                        Description = transaction.description
                    });
                }
            }

            if (accountTransactions.Count > 0)
            {
                await this._transactionRepository.Create(accountTransactions);
                this._transactionRepository.Save();
            }

            return response;
        }

        public PaginatedList<TransactionModel> TransactionList(int page, int pageSize, out int totalRecords)
        {
            List<TransactionModel> transactions;

            var accountTransactions = this._transactionRepository.GetAllWithPaging(page, pageSize, out totalRecords);

            transactions = accountTransactions.Select(x => new TransactionModel()
            {
                account = x.Account,
                amount = x.Amount.ToString(),
                currencyCode = x.CurrencyCode,
                description = x.Description,
                rowno = x.Id
            }).ToList();

            return new PaginatedList<TransactionModel>(transactions, totalRecords, page, pageSize);
        }
    }
}
