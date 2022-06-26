using System.Collections.Generic;

namespace DecisionTables
{
    public abstract class EncapsulatedDecisionTable<TKey, TValue, TDomain>
        where TKey : class
        where TValue : class
        where TDomain : class
    {
        public readonly IKeyExtractor<TDomain, TKey> _extractor;
        protected Dictionary<TKey, TValue> _decisionTable;

        protected EncapsulatedDecisionTable(IKeyExtractor<TDomain, TKey> extractor)
        {
            _extractor = extractor;
        }
    }

    /// <summary>
    ///     Abstract base class for an Encapsulated Decision Table with a Func Value
    /// </summary>
    /// <typeparam name="TKey">Key Type of Decision Table Dictionary</typeparam>
    /// <typeparam name="TValue">
    ///     Value Type of Decision Table Dictionary, a generic Func</remarks></typeparam>
    /// <typeparam name="TDomain">Type of a Domain object from which an instance of K can be extracted</typeparam>
    /// <typeparam name="TResult">Return Typed</typeparam>
    public abstract class FuncBasedEncapsulatedDecisionTable<TKey, TValue, TDomain, TResult>
        : EncapsulatedDecisionTable<TKey, TValue, TDomain>
        where TKey : class
        where TValue : class
        where TDomain : class
        where TResult : class
    {
        protected FuncBasedEncapsulatedDecisionTable(IKeyExtractor<TDomain, TKey> extractor)
            : base(extractor)
        {
        }

        protected virtual TResult Execute(TDomain arg)
        {
            throw new DecisionTableDispatchException(DecisionTableDispatchException.FormatMessage(arg));
        }
    }


    /// <summary>
    ///     Abstract base class for an Encapsulated Decision Table with a Func Value
    /// </summary>
    /// <typeparam name="TKey">Key Type of Decision Table Dictionary</typeparam>
    /// <typeparam name="TValue">
    ///     Value Type of Decision Table Dictionary, a generic Action</remarks></typeparam>
    /// <typeparam name="TDomain">Type of a Domain object from which an instance of K can be extracted</typeparam>
    /// <typeparam name="R">Return Typed</typeparam>
    public abstract class ActionBasedEncapsulatedDecisionTable<TKey, TValue, TDomain>
        : EncapsulatedDecisionTable<TKey, TValue, TDomain>
        where TKey : class
        where TValue : class
        where TDomain : class
    {
        protected ActionBasedEncapsulatedDecisionTable(IKeyExtractor<TDomain, TKey> extractor)
            : base(extractor)
        {
        }

        protected virtual void Execute(TDomain arg)
        {
            throw new DecisionTableDispatchException(DecisionTableDispatchException.FormatMessage(arg));
        }
    }
}