using System;
using System.Collections.Generic;
using DecisionTables.Domain;
using Microsoft.Practices.Unity;
using Xunit;

namespace DecisionTables
{
    public static class DictionaryExtensions
    {
        public static Tuple<decimal, decimal> Execute(
            this Dictionary<Type, Func<TaxItemLink, Tuple<decimal, decimal>>> decisionTable,
            TaxItemLink link)
        {
            Type key = new TaxKeyExtractor().ExtractKey(link);
            Func<TaxItemLink, Tuple<decimal, decimal>> f;
            if (decisionTable.TryGetValue(key, out f))
            {
                return f(link);
            }
            throw new DecisionTableDispatchException(DecisionTableDispatchException.FormatMessage(key));
        }
    }

    public class UnitTests
    {
        [Fact]
        public void TestGoodAssignmentDoesntThrow()
        {
            var link = new TaxItemLink();
            Assert.DoesNotThrow(() => { link.Source = new TaxItem(); });
            Assert.DoesNotThrow(() => { link.Destination = new LineItem(); });
        }

        [Fact]
        public void TestBadAssignmentThrows()
        {
            var link = new TaxItemLink();
            Assert.Throws<InvalidLinkAssignmentException>(() => { link.Source = new LineItem(); });
            Assert.Throws<InvalidLinkAssignmentException>(() => { link.Destination = new TaxItem(); });
        }


        [Fact]
        public void TestInitializeByClassMethod()
        {
            var decisonTable =
                new Dictionary<Type, Func<TaxItemLink, Tuple<decimal, decimal>>>();

            decisonTable.Add(typeof (SalesTax), ComputeSales);
            decisonTable.Add(typeof (LuxuryTax), ComputeLuxury);
            decisonTable.Add(typeof (VATTax), ComputeVAT);

            var link = new TaxItemLink
                {
                    Destination = new LineItem(),
                    Source = new TaxItem {Authority = new SalesTax()}
                };

            Tuple<decimal, decimal> result = decisonTable.Execute(link);
            Assert.True(result.Item1 == 1.0M);
        }

        [Fact]
        public void TestInitializeByStaticClassMethod()
        {
            var decisonTable =
                new Dictionary<Type, Func<TaxItemLink, Tuple<decimal, decimal>>>();

            decisonTable.Add(typeof (SalesTax), SalesCompute.Compute);
            decisonTable.Add(typeof (LuxuryTax), LuxuryCompute.Compute);
            decisonTable.Add(typeof (VATTax), VATCompute.Compute);

            var link = new TaxItemLink
                {
                    Destination = new LineItem(),
                    Source = new TaxItem {Authority = new LuxuryTax()}
                };

            Tuple<decimal, decimal> result = decisonTable.Execute(link);
            Assert.True(result.Item1 == 2.0M);
        }

        [Fact]
        public void TestInitializeByLambda()
        {
            var decisonTable =
                new Dictionary<Type, Func<TaxItemLink, Tuple<decimal, decimal>>>();

            decisonTable.Add(typeof (SalesTax), (lk) => new Tuple<decimal, decimal>(1.0M, 1.0M));
            decisonTable.Add(typeof (LuxuryTax), (lk) => new Tuple<decimal, decimal>(2.0M, 2.0M));
            decisonTable.Add(typeof (VATTax), (lk) => new Tuple<decimal, decimal>(3.0M, 3.0M));

            var link = new TaxItemLink
                {
                    Destination = new LineItem(),
                    Source = new TaxItem {Authority = new VATTax()}
                };

            Tuple<decimal, decimal> result = decisonTable.Execute(link);
            Assert.True(result.Item1 == 3.0M);
        }

        [Fact]
        public void TestConstructionIntializedStaticClassMethod()
        {
            var decisonTable =
                new Dictionary<Type, Func<TaxItemLink, Tuple<decimal, decimal>>>
                    {
                        {typeof (SalesTax), SalesCompute.Compute},
                        {typeof (LuxuryTax), LuxuryCompute.Compute},
                        {typeof (VATTax), VATCompute.Compute}
                    };

            var link = new TaxItemLink
                {
                    Destination = new LineItem(),
                    Source = new TaxItem {Authority = new VATTax()}
                };

            Tuple<decimal, decimal> result = decisonTable.Execute(link);
            Assert.True(result.Item1 == 3.0M);
        }

        [Fact]
        public void TestEncapsulatedDecisionTable()
        {
            IEncapsulatedDecisionTable<TaxItemLink, Tuple<decimal, decimal>> x =
                new TaxDecisionTable(new TaxKeyExtractor());
            var link = new TaxItemLink
                {
                    Destination = new LineItem(),
                    Source = new TaxItem {Authority = new VATTax()}
                };

            Tuple<decimal, decimal> result = x.Execute(link);
            Assert.True(result.Item1 == 3.0M);
        }

        [Fact]
        public void TestIOCDecisionTable()
        {
            var container = new UnityContainer();
            // Adding the extension initializes the DecisionTable
            container.AddNewExtension<ContainerExtension>();

            var link = new TaxItemLink
                {
                    Destination = new LineItem(),
                    Source = new TaxItem {Authority = new VATTax()}
                };

            Tuple<decimal, decimal> result =
                container.Resolve<IEncapsulatedDecisionTable<TaxItemLink, Tuple<Decimal, decimal>>>().Execute(link);
            Assert.True(result.Item1 == 3.0M);
        }

        [Fact]
        private void TestInvalidLink()
        {
            var extractor = new TaxKeyExtractor();
            Assert.Throws<InvalidLinkException>(() => extractor.ExtractKey(null));
            Assert.Throws<InvalidLinkException>(() => extractor.ExtractKey(new TaxItemLink()));
            Assert.Throws<InvalidLinkException>(() => extractor.ExtractKey(
                new TaxItemLink {Source = new TaxItem()}));
            Assert.Throws<InvalidLinkException>(() => extractor.ExtractKey(
                new TaxItemLink {Destination = new LineItem()}));
            Assert.Throws<InvalidLinkException>(() => extractor.ExtractKey(
                new TaxItemLink {Source = new TaxItem(), Destination = new LineItem()}));
        }

        [Fact]
        private void TestUnfilledTable()
        {
            IEncapsulatedDecisionTable<TaxItemLink, Tuple<decimal, decimal>> x =
                new UnfilledTaxDecisionTable(new TaxKeyExtractor());
            var link = new TaxItemLink
                {
                    Destination = new LineItem(),
                    Source = new TaxItem {Authority = new VATTax()}
                };
            Assert.Throws<DecisionTableDispatchException>(() => x.Execute(link));
        }


        private Tuple<decimal, decimal> ComputeSales(TaxItemLink link)
        {
            return new Tuple<decimal, decimal>(1.0M, 1.0M);
        }

        private Tuple<decimal, decimal> ComputeLuxury(TaxItemLink link)
        {
            return new Tuple<decimal, decimal>(2.0M, 2.0M);
        }

        private Tuple<decimal, decimal> ComputeVAT(TaxItemLink link)
        {
            return new Tuple<decimal, decimal>(3.0M, 3.0M);
        }
    }
}