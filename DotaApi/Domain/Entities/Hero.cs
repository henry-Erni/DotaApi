﻿namespace DotaApi.Domain.Entities
{
    public class Hero
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Role { get; set; }

        //public required ICollection<Item> Items { get; set; }
    }
}
