using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PokemonPicker.Models
{
    public class PokeContext : DbContext 
    {
        public PokeContext() : base()
        {

        }
        public DbSet<Pokemon> Pokemons { get; set; }

        public DbSet<Trainer> Trainers { get; set; }
    }
}