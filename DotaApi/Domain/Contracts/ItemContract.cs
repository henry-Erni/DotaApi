using DotaApi.Domain.Entities;

namespace DotaApi.Domain.Contracts
{
    public record CreateItem
    (
        string Name,
        double Price,
        string Description
    );

    public record UpdateItem
    (
        string Name,
        double Price,
        string Description
    );

    public record DeleteItem(Guid Id);

    public record GetItem(Guid Id);

    public class GetItemDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public double Price { get; set; }
        public required string Description { get; set; }
        //public Guid HeroId { get; set; }
    }
}
