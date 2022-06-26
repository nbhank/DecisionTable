using System;
using System.Collections.Generic;
using DecisionTables.Domain;

namespace DecisionTables
{
    public class UnfilledTaxDecisionTable :
        FuncBasedEncapsulatedDecisionTable<Type,
            Func<TaxItemLink, Tuple<decimal, decimal>>,
            TaxItemLink,
            Tuple<decimal, decimal>>,
        IEncapsulatedDecisionTable<TaxItemLink, Tuple<decimal, decimal>>
    {
        public UnfilledTaxDecisionTable(IKeyExtractor<TaxItemLink, Type> extractor)
            : base(extractor)
        {
            _decisionTable =
                new Dictionary<Type, Func<TaxItemLink, Tuple<decimal, decimal>>>
                    {
                        {typeof (SalesTax), SalesCompute.Compute},
                        {typeof (LuxuryTax), LuxuryCompute.Compute},
                    };
        }

        public Tuple<decimal, decimal> Execute(TaxItemLink link)
        {
            Func<TaxItemLink, Tuple<decimal, decimal>> f;
            if (_decisionTable.TryGetValue(_extractor.ExtractKey(link), out f))
            {
                return f(link);
            }
            return base.Execute(link); //always throws
        }
    }

    public class TaxDecisionTable : UnfilledTaxDecisionTable
    {
        public TaxDecisionTable(IKeyExtractor<TaxItemLink, Type> extractor) : base(extractor)
        {
            _decisionTable.Add(typeof (VATTax), VATCompute.Compute);
        }
    }
}