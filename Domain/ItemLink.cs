namespace DecisionTables.Domain
{
    public abstract class ItemLink
    {
        public virtual Item Source { get; set; }
        public virtual Item Destination { get; set; }
    }

    public class TaxItemLink : ItemLink
    {
        public override Item Source
        {
            get { return base.Source; }
            set { base.Source = value.CheckExpectedType<TaxItem>(); }
        }

        public override Item Destination
        {
            get { return base.Destination; }
            set { base.Destination = value.CheckExpectedType<LineItem>(); }
        }
    }

    class TaxItemLinkImpl : TaxItemLink
    {
    }
}