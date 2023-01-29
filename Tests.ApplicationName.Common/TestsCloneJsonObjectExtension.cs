using ApplicationName.Common.Extensions;
using FluentAssertions;

namespace Tests.ApplicationName.Common
{
    public class TestsCloneJsonObjectExtension
    {
        private class QuoteEntityObjectStandardT1
        {
            public DateTime Date { get; set; }
            public decimal Low { get; set; }
            public decimal Open { get; set; }
            public decimal Close { get; set; }
            public decimal AdjClose { get; set; }
            public decimal High { get; set; }
            public long Volume { get; set; }
        }

        private class QuoteEntityObjectExtendedT2
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public decimal Low { get; set; }
            public decimal Open { get; set; }
            public decimal Close { get; set; }
            public decimal AdjClose { get; set; }
            public decimal High { get; set; }
            public long Volume { get; set; }
        }

        private class QuoteEntityObjectReducedT3
        {
            public DateTime Date { get; set; }
            public decimal Low { get; set; }
            public decimal Open { get; set; }
            public decimal Close { get; set; }

            public decimal High { get; set; }
            public long Volume { get; set; }
        }

        private class QuoteEntityObjectInconsistentT4
        {
            public Int16 Volume { get; set; }
        }

        private readonly QuoteEntityObjectStandardT1 _standardObjectT1 = new()
        {
            Date = new DateTime(3000, 01, 01),
            Low = 15000.100000m,
            Open = 15000.100001m,
            Close = 15000.100002m,
            AdjClose = 15000.100002m,
            High = 15000.100003m,
            Volume = 99999999
        };

        [Test]
        public void ShouldCloneJsonStandardObjectT1ToStandardObjectT1()
        {
            //WHEN
            QuoteEntityObjectStandardT1 actualClonedJsonStandardObjectT1 = _standardObjectT1.CloneJson<QuoteEntityObjectStandardT1>();

            //THEN
            actualClonedJsonStandardObjectT1.Should().BeEquivalentTo(_standardObjectT1);
        }

        [Test]
        public void ShouldCloneJsonStandardObjectT1ToExtendedObjectT2()
        {
            //GIVEN
            QuoteEntityObjectExtendedT2 expectedExtendedObjectT2 = new()
            {
                Id = 0,
                Date = new DateTime(3000, 01, 01),
                Low = 15000.100000m,
                Open = 15000.100001m,
                Close = 15000.100002m,
                AdjClose = 15000.100002m,
                High = 15000.100003m,
                Volume = 99999999
            };

            //WHEN
            QuoteEntityObjectExtendedT2 actualClonedJsonExtendedObjectT2 = _standardObjectT1.CloneJson<QuoteEntityObjectExtendedT2>();

            //THEN
            actualClonedJsonExtendedObjectT2.Should().BeEquivalentTo(expectedExtendedObjectT2);
        }

        [Test]
        public void ShouldCloneJsonStandardObjectT1ToReducedObjectT3()
        {
            //GIVEN
            QuoteEntityObjectReducedT3 expectedReducedObjectT3 = new()
            {
                Date = new DateTime(3000, 01, 01),
                Low = 15000.100000m,
                Open = 15000.100001m,
                Close = 15000.100002m,
                //No AdjClose Property
                High = 15000.100003m,
                Volume = 99999999
            };

            //WHEN
            QuoteEntityObjectReducedT3 actualClonedJsonReducedObjectT3 = _standardObjectT1.CloneJson<QuoteEntityObjectReducedT3>();

            //THEN
            actualClonedJsonReducedObjectT3.Should().BeEquivalentTo(expectedReducedObjectT3);
        }

        [Test]
        public void ShouldNotCloneJsonStandardObjectT1ToInconsistentObjectT4()
        {
            //WHEN
            QuoteEntityObjectInconsistentT4 actualClonedJsonInconsistentObjectT4 = _standardObjectT1.CloneJson<QuoteEntityObjectInconsistentT4>();

            //THEN 
            actualClonedJsonInconsistentObjectT4.Should().BeNull();
        }
    }
}
