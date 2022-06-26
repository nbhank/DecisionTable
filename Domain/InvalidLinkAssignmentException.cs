using System;
using System.Runtime.Serialization;

namespace DecisionTables.Domain
{
    internal class InvalidLinkAssignmentException : Exception
    {
        public InvalidLinkAssignmentException()
        {
        }

        public InvalidLinkAssignmentException(string message)
            : base(message)
        {
        }

        public InvalidLinkAssignmentException(string message, Exception inner)
            : base(message, inner)
        {
        }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected InvalidLinkAssignmentException(SerializationInfo info,
                                                 StreamingContext context)
            : base(info, context)
        {
        }

        public static string FormatMessage(Type link, Type badAssignee)
        {
            return string.Format("{0} cannot be assigned to a {1}", badAssignee.FullName, link.FullName);
        }
    }
}