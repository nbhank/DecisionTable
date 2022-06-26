namespace DecisionTables
{
    /// <summary>
    ///     This interface allows access to the underlying DecisionTable
    ///     so that it can be populated dynamically during IOC initialization
    ///     For maximum encapsulation, this interface should not be expressed by any
    ///     concrete class to be directly instantiated via new
    /// </summary>
    /// <typeparam name="TKey">Type of the key of the DecisionTable</typeparam>
    /// <typeparam name="TValue">Type of the value of DescisioTable</typeparam>
    public interface IEncapsulatedDecisionTableIoc<TKey, TValue> 
        where TKey : class
        where TValue : class
    {
        void Clear();
        void Add(TKey condition, TValue action);
    }
}