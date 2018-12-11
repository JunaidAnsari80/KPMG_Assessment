using KPMG_Assessment.Website.Data;
using KPMG_Assessment.Website.Models;
using KPMG_Assessment.Website.Repositories;
using KPMG_Assessment.Website.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KPMG_Assesment.Tests
{
    public class ExcelFileReaderServiceTests
    {
        private readonly Mock<IRepository<AccountTransaction>> _repository;

        public ExcelFileReaderServiceTests()
        {
            this._repository = new Mock<IRepository<AccountTransaction>>();

        }


        [Theory]
        [InlineData("1001", "GB", "DEBIT", "10.00")]
        public async Task AddTransaction_adding_a_transaction_with_invalid_CurrencyCode_Should_return_Error(string account, string currencyCode, string description, string amount)
        {
            // Arrange
            var transactions = new List<TransactionModel>()
            {
                new TransactionModel(){
                    account = account,
                    amount = amount,
                    currencyCode = currencyCode,
                    description = description,
                }
            };

            var excelFileReaderService = new ExcelFileReaderService(_repository.Object);
            // Act

            var response = excelFileReaderService.SaveTransactions(transactions).Result;
            // Assert

            Assert.NotNull(response);
            Assert.Equal(1, response.Count);
            Assert.Equal("Invalid CurrencyCode", response[0].errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData("1001", "GB", "DEBIT", "0.")]
        public async Task AddTransaction_adding_a_transaction_with_invalid_Amount_Should_return_Error(string account, string currencyCode, string description, string amount)
        {
            // Arrange
            var transactions = new List<TransactionModel>()
            {
                new TransactionModel(){
                    account = account,
                    amount = amount,
                    currencyCode = currencyCode,
                    description = description,
                }
            };

            var excelFileReaderService = new ExcelFileReaderService(_repository.Object);
            // Act

            var response = excelFileReaderService.SaveTransactions(transactions).Result;
            // Assert

            Assert.NotNull(response);
            Assert.Equal(1, response.Count);
            Assert.Equal("Invalid Decmal Amount", response[0].errors[0].ErrorMessage);
        }

        [Fact]
        public async Task AddTransaction_Should_Save()
        {
            //Arrange
            var excelFileReaderService = new ExcelFileReaderService(_repository.Object);
            var transactions = new List<TransactionModel> {
                new TransactionModel(){
                    account = "10101",
                    amount = "100.00",
                    currencyCode ="GBP",
                    description = "Credit",
                }
            };
            // Act
          var response = await excelFileReaderService.SaveTransactions(transactions);
            // Assert

            Assert.NotNull(response);
            Assert.Equal(0, response.Count);
        }
    }
}
