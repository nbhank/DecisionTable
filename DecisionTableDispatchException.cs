using System;
using System.Runtime.Serialization;

namespace DecisionTables
{
    internal class DecisionTableDispatchException : Exception
    {
        public DecisionTableDispatchException()
        {
        }

        public DecisionTableDispatchException(string message)
            : base(message)
        {
        }

        public DecisionTableDispatchException(string message, Exception inner)
            : base(message, inner)
        {
        }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected DecisionTableDispatchException(SerializationInfo info,
                                                 StreamingContext context)
            : base(info, context)
        {
        }

        public static string FormatMessage(object arg)
        {
            return string.Format("DecisionTable does not have entry for specified key {0}", arg);
        }
    }
}