namespace LeadTheWay.Models.GraphLayer.Map.Domain
{
    public interface IGraph
    {
        int Id { get; set; }

        string Name { get; set; }

        string GraphString { get; set; }
    }
}
