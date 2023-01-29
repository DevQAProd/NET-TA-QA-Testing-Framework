using ApplicationName.App.Api.Services;
using ApplicationName.Common.Enumerations.YahooFinance;
using ApplicationName.Common.Extensions;
using ApplicationName.Common.ParametersAndFacilities.Facilities.Enumeration;
using ApplicationName.Common.ParametersAndFacilities.Facilities.Services;
using ApplicationName.ModelsAndFacilities.Facilities.Interfaces;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Tests.ApplicationName.QA.Api.TestData.YahooFinance;

namespace Tests.ApplicationName.QA.Api
{
    [TestFixture]
    public class TestsYahooFinanceWithAuthenticationV1
    {
        private readonly YahooFinanceWithAuthenticationApiServiceV1 _yahooFinanceWithAuthenticationApiServiceV1 = new();
        private readonly IAuthParameters _iAmUserAuthParameters = new AuthParametersService().GetAuthParametersByUserName(Users.IAmUser.UserName);

        [Test]
        public void UserCanGetQuoteRecordsFromYahooFinanceByDateRangeWithBaseHttpHeadersAuthentication()
        {
            //GIVEN
            var expectedQuoteRecordA = TestDataYahooFinanceQuotes.NasdaqCompositeDaily20220711;
            var expectedQuoteRecordB = TestDataYahooFinanceQuotes.NasdaqCompositeDaily20220712;

            //WHEN
            var actualQuoteRecords = _yahooFinanceWithAuthenticationApiServiceV1.GetQuoteRecordsByDateRange(_iAmUserAuthParameters, Quote.NasdaqComposite, Frequency.Daily,
                expectedQuoteRecordA.Date, expectedQuoteRecordB.Date).TContent;

            //THEN
            actualQuoteRecords.Should().BeEquivalentTo(new List<QuoteEntityModel>() { expectedQuoteRecordA, expectedQuoteRecordB });
        }

        [Test]
        public void UserCanUpsertQuoteRecordsToYahooFinanceWithXApiKeyQueryStringAuthentication()
        {
            //GIVEN
            QuoteEntityModel expectedNewQuoteRecordA = TestDataYahooFinanceQuotes.GenerateRandomNasdaqCompositeQuote(DateTime.UtcNow.AddYears(-1000).ToMySqlDbDateTime());
            QuoteEntityModel expectedNewQuoteRecordB = TestDataYahooFinanceQuotes.GenerateRandomNasdaqCompositeQuote(DateTime.UtcNow.AddYears(-1000).AddSeconds(1).ToMySqlDbDateTime());
            QuoteEntityModel expectedExistingQuoteRecordA = TestDataYahooFinanceQuotes.GenerateRandomNasdaqCompositeQuote(new DateTime(1000, 01, 01));
            QuoteEntityModel expectedExistingQuoteRecordB = TestDataYahooFinanceQuotes.GenerateRandomNasdaqCompositeQuote(new DateTime(1000, 01, 02));

            //WHEN
            int actualAmountOfUpsertedQuoteRecords = _yahooFinanceWithAuthenticationApiServiceV1.UpsertQuoteRecords(_iAmUserAuthParameters, Quote.NasdaqComposite, Frequency.Daily,
                    new List<QuoteEntityModel>() { expectedNewQuoteRecordA, expectedNewQuoteRecordB, expectedExistingQuoteRecordA, expectedExistingQuoteRecordB }).TContent;

            //THEN
            actualAmountOfUpsertedQuoteRecords.Should().Be(4, "amount of affected records should be 2 added and 2 updated.");

            List<QuoteEntityModel> actualNewQuoteRecords = _yahooFinanceWithAuthenticationApiServiceV1.GetQuoteRecordsByDateRange(_iAmUserAuthParameters, Quote.NasdaqComposite, Frequency.Daily, 
                expectedNewQuoteRecordA.Date, expectedNewQuoteRecordB.Date).TContent;
            List<QuoteEntityModel> actualExistingQuoteRecords = _yahooFinanceWithAuthenticationApiServiceV1.GetQuoteRecordsByDateRange(_iAmUserAuthParameters, Quote.NasdaqComposite, Frequency.Daily,
                expectedExistingQuoteRecordA.Date, expectedExistingQuoteRecordB.Date).TContent;

            using (new AssertionScope())
            {
                actualNewQuoteRecords.Should().BeEquivalentTo(new List<QuoteEntityModel> { expectedNewQuoteRecordA, expectedNewQuoteRecordB });
                actualExistingQuoteRecords.Should().BeEquivalentTo(new List<QuoteEntityModel> { expectedExistingQuoteRecordA, expectedExistingQuoteRecordB });
            }
        }

        [Test]
        public void UserCanDeleteQuoteRecordsInYahooFinanceWithXApKeyCookiesAuthentication()
        {
            //GIVEN
            QuoteEntityModel expectedQuoteRecordForDeletionA = TestDataYahooFinanceQuotes.GenerateRandomNasdaqCompositeQuote(DateTime.UtcNow.AddYears(-1001).ToMySqlDbDateTime());
            QuoteEntityModel expectedQuoteRecordForDeletionB = TestDataYahooFinanceQuotes.GenerateRandomNasdaqCompositeQuote(DateTime.UtcNow.AddYears(-1001).AddSeconds(1).ToMySqlDbDateTime());

            int actualAmountOfUpsertedQuoteRecords = _yahooFinanceWithAuthenticationApiServiceV1.UpsertQuoteRecords(_iAmUserAuthParameters, Quote.NasdaqComposite, Frequency.Daily,
                new List<QuoteEntityModel>() { expectedQuoteRecordForDeletionA, expectedQuoteRecordForDeletionB }).TContent;
            actualAmountOfUpsertedQuoteRecords.Should().Be(2,
                $"quote records with dates '{expectedQuoteRecordForDeletionA.Date}' and '{expectedQuoteRecordForDeletionB.Date}' should exist in the database prior deletion");

            //WHEN 
            int actualAmountOfDeletedQuoteRecords = _yahooFinanceWithAuthenticationApiServiceV1.DeleteQuoteRecords(_iAmUserAuthParameters, Quote.NasdaqComposite, Frequency.Daily,
                new List<DateTime>() { expectedQuoteRecordForDeletionA.Date, expectedQuoteRecordForDeletionB.Date }).TContent;

            //THEN
            actualAmountOfDeletedQuoteRecords.Should().Be(2,
                $"2 records with dates '{expectedQuoteRecordForDeletionA.Date}' and '{expectedQuoteRecordForDeletionB.Date}' should have been deleted");

            List<QuoteEntityModel> actualQuoteRecordsAfterDelete = _yahooFinanceWithAuthenticationApiServiceV1.GetQuoteRecordsByDateRange(_iAmUserAuthParameters, Quote.NasdaqComposite, Frequency.Daily,
                expectedQuoteRecordForDeletionA.Date, expectedQuoteRecordForDeletionB.Date).TContent;

            actualQuoteRecordsAfterDelete.Should().BeEmpty();
        }
    }
}
