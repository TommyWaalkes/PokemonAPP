using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PokemonPicker.Models
{
    public class Trainer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Badges { get; set; }
        public int Money { get; set; }
        [MinLength(1)]
        [MaxLength(6)]
        List<Pokemon> Team { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
    }
}