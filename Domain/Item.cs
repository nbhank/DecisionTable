namespace DecisionTables.Domain
{
    public abstract class Item
    {
    }

    public class TaxItem : Item
    {
        public TaxAuthority Authority { get; set; }
    }

    public class LineItem : Item
    {
    }
}