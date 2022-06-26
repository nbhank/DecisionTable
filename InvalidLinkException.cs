using System;
using System.Runtime.Serialization;

namespace DecisionTables.Domain
{
    internal class InvalidLinkException : Exception
    {
        public InvalidLinkException()
        {
        }

        public InvalidLinkException(string message)
            : base(message)
        {
        }

        public InvalidLinkException(string message, Exception inner)
            : base(message, inner)
        {
        }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected InvalidLinkException(SerializationInfo info,
                                       StreamingContext context)
            : base(info, context)
        {
        }

        public static string FormatMessage()
        {
            return string.Format("ItemLink is null or uninitialized");
        }

        public static string FormatNullAuthorityMessage(TaxItemLink link)
        {
            return string.Format("{0} has null TaxAuthority.", link.GetType().FullName);
        }
    }
}