using KPMG_Assessment.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPMG_Assessment.Website.Services
{
    public interface IFileReaderService
    {
        Task<List<TransactionResponseModel>> SaveTransactions(IList<TransactionModel> transactionModels);
        PaginatedList<TransactionModel> TransactionList(int page , int pageSize, out int totalRecords);


    }
}
