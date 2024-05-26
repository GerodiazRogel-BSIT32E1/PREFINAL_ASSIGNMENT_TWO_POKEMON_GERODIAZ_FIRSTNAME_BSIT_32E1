using System.Collections.Generic;
using Newtonsoft.Json;

namespace PREFINAL_ASSIGNMENT_TWO_POKEMON_GERODIAZ_FIRSTNAME_BSIT_32E1.Models
{
    public class Pokemon
{
    public string? Name { get; set; }
    public string? Url { get; set; }
    public string? ImageUrl { get; set; }
    public List<PokemonMove>? Moves { get; set; }
    public List<AbilityEntry>? Abilities { get; set; }
    public string? Description { get; set; }
}

    public class PokemonList
    {
        public int Count { get; set; }
        public string? Next { get; set; }
        public string? Previous { get; set; }
        public List<Pokemon>? Results { get; set; }
    }

    public class AbilityEntry
    {
        public Ability? Ability { get; set; }
        public bool IsHidden { get; set; }
        public int Slot { get; set; }
    }

    public class Ability
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
    }

    public class PokemonMove
    {
        public Move? Move { get; set; }
        public List<VersionGroupDetail>? VersionGroupDetails { get; set; }
    }

    public class Move
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
    }

    public class VersionGroupDetail
    {
        public int LevelLearnedAt { get; set; }
        public MoveLearnMethod? MoveLearnMethod { get; set; }
        public VersionGroup? VersionGroup { get; set; }
    }

    public class MoveLearnMethod
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
    }

    public class VersionGroup
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
    }

    public class HomeViewModel
    {
        public List<Pokemon>? Pokemons { get; set; }
    }

    public class PokemonDetails
    {
        public Sprites? Sprites { get; set; }
    }

    public class Sprites
    {
        [JsonProperty("front_default")]
        public string? FrontDefault { get; set; }
    }
}