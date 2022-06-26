namespace DecisionTables
{
    /// <summary>
    ///     Interface used to extract DecisionTable key from a Domain object
    /// </summary>
    /// <typeparam name="TDomain">A domain class</typeparam>
    /// <typeparam name="TKey">The type of the DecisionTable key</typeparam>
    public interface IKeyExtractor<TDomain, TKey>
        where TDomain : class
        where TKey : class
    {
        TKey ExtractKey(TDomain arg);
    }
}