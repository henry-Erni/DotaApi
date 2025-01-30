using DotaApi.Domain.Entities;

namespace DotaApi.Domain.Contracts
{
    public record CreateHero(string Name, string Role);
    public record UpdateHero(string Name, string Role);
    public record DeleteHero(Guid Id);

    public record GetHero(Guid Id);

    public class GetHeroDTO
    {
        //public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Role { get; set; }

    }
}
