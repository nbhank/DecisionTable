using System;
using DecisionTables.Domain;

namespace DecisionTables
{
    public interface IComputeTax
    {
        Tuple<decimal, decimal> ComputeEx(TaxItemLink link);
    }

    public abstract class ComputeTax : IComputeTax
    {
        public abstract Tuple<decimal, decimal> ComputeEx(TaxItemLink link);
    }

    internal class LuxuryCompute : ComputeTax
    {
        public static Tuple<decimal, decimal> Compute(TaxItemLink link)
        {
            return new Tuple<decimal, decimal>(2.0M, 2.0M);
        }

        public override Tuple<decimal, decimal> ComputeEx(TaxItemLink link)
        {
            return Compute(link);
        }
    }

    internal class SalesCompute : ComputeTax
    {
        public static Tuple<decimal, decimal> Compute(TaxItemLink link)
        {
            return new Tuple<decimal, decimal>(1.0M, 1.0M);
        }

        public override Tuple<decimal, decimal> ComputeEx(TaxItemLink link)
        {
            return Compute(link);
        }
    }

    internal class VATCompute : ComputeTax
    {
        public static Tuple<decimal, decimal> Compute(TaxItemLink link)
        {
            return new Tuple<decimal, decimal>(3.0M, 3.0M);
        }

        public override Tuple<decimal, decimal> ComputeEx(TaxItemLink link)
        {
            return Compute(link);
        }
    }
}