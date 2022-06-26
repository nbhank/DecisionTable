using System;
using DecisionTables.Domain;

namespace DecisionTables
{
    public class TaxDecisionTableIoc : TaxDecisionTable,
                                       IEncapsulatedDecisionTableIoc<Type, ComputeTax>
    {
        public TaxDecisionTableIoc(IKeyExtractor<TaxItemLink, Type> extractor)
            : base(extractor)
        {
        }

        public void Clear()
        {
            _decisionTable.Clear();
        }

        public void Add(Type key, ComputeTax entry)
        {
            Func<TaxItemLink, Tuple<decimal, decimal>> ctax;
            if (!_decisionTable.TryGetValue(key, out ctax))
            {
                _decisionTable.Add(key, entry.ComputeEx);
            }
        }
    }
}