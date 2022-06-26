using System;
using DecisionTables.Domain;

namespace DecisionTables
{
    public class TaxKeyExtractor : IKeyExtractor<TaxItemLink, Type>
    {
        public Type ExtractKey(TaxItemLink link)
        {
            if (null == link ||
                link.Source == null ||
                link.Destination == null ||
                (link.Source as TaxItem).Authority == null)
            {
                throw new InvalidLinkException(InvalidLinkException.FormatMessage());
            }
            return (link.Source as TaxItem).Authority.GetType();
        }
    }
}