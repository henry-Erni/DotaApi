namespace DotaApi.Domain.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public double Price { get; set; }
        public required string Description { get; set; }
        public Guid HeroId { get; set; }
        public required string Hero {  get; set; }
    }
}
