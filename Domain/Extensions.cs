namespace DecisionTables.Domain
{
    internal static class DomainExtensions
    {
        public static T CheckExpectedType<T>(this Item prop) where T : Item
        {
            if (prop.GetType() != typeof (T))
            {
                throw new InvalidLinkAssignmentException(
                    InvalidLinkAssignmentException.FormatMessage(typeof (T), prop.GetType()));
            }
            return (T) prop;
        }
    }
}