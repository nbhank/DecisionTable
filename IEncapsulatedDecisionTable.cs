namespace DecisionTables
{
    /// <summary>
    ///     Interface that causes the delegated call from the DecisionTable
    ///     <remarks>
    ///         Use this interface if the KeyValuePair.Value is based on generic Func
    ///     </remarks>
    /// </summary>
    /// <typeparam name="TDomain">
    ///     A Type from which the implementor can retrieve
    ///     in some manner a Key to the DecisionTable
    /// </typeparam>
    /// <typeparam name="TResult">
    ///     The type of the returned value from
    ///     the DecisionTable's Value execution, i.e. the return type of the generic Func
    /// </typeparam>
    public interface IEncapsulatedDecisionTable<TDomain, TResult>
        where TDomain : class
        where TResult : class
    {
        TResult Execute(TDomain arg);
    }

    /// <summary>
    ///     Interface that causes the delegated call from the DecisionTable
    /// </summary>
    /// <remarks>
    ///     Use this interface if the KeyValuePair.Value is based on generic Action
    /// </remarks>
    /// <typeparam name="TDomain">
    ///     A Type from which the implementor can retrieve
    ///     in some manner a Key to the DecisionTable
    /// </typeparam>
    public interface IEncapsulatedDecisionTable<TDomain>
        where TDomain : class
    {
        void Execute(TDomain arg);
    }
}