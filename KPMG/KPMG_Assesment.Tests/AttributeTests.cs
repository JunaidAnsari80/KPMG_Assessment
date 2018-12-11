using KPMG_Assessment.Website.Attributes;
using KPMG_Assessment.Website.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace KPMG_Assesment.Tests
{
    public class AttributeTests
    {
        TransactionModel transactionModel;

        public AttributeTests()
        {
           
        }

        [Theory]
        [InlineData("GPP")]
        public void WithGivenData_CurrencyCode_ShouldFail(string currencyCode)
        {
            // arrange
          
            var attrib = new CurrencyCodeAttribute();

            // act
            var result = attrib.IsValid(currencyCode);

            // assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("GBP")]
        public void WithGivenData_CurrencyCode_ShouldPass(string currencyCode)
        {
            // arrange

            var attrib = new CurrencyCodeAttribute();

            // act
            var result = attrib.IsValid(currencyCode);

            // assert
            Assert.True(result);
        }
    }
}
