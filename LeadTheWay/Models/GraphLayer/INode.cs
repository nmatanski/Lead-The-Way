namespace LeadTheWay.Models.GraphLayer
{
    public interface INode
    {
        string Name { get; set; }
        string Description { get; set; }
        void Reset();
    }
}