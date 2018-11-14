using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonPicker.Models
{
    public class Pokemon
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public bool Fainted { get; set; }
        //public TypeTable typeTable { get; set; }

        public Pokemon(string json)
        {
            JObject o = JObject.Parse(json);
            Hashtable ht = new Hashtable();
            Fainted = false;
            Name = o["name"].ToString();
            ImageURL = o["sprites"]["front_default"].ToString();
            Type1 = o["types"][0]["type"]["name"].ToString();
            try
            {
                Type2 = o["types"][1]["type"]["name"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("This pokemon only has one type");
            }

            if(Type2 == null)
            {
                //typeTable = new TypeTable(Type1);
            }

            for (int i = 0; i < o["stats"].Count(); i++)
            {
                string statName = o["stats"][i]["stat"]["name"].ToString();

                int stat = int.Parse(o["stats"][i]["base_stat"].ToString());
                switch (statName)
                {
                    case "hp":
                        Hp = stat;
                        break;
                    case "attack":
                        Attack = stat;
                        break;
                    case "defense":
                        Defense = stat;
                        break;
                    case "special-attack":
                        SpecialAttack = stat;
                        break;
                    case "special-defense":
                        SpecialDefense = stat;
                        break;
                    case "speed":
                        Speed = stat;
                        break;
                }
                ht.Add(statName, stat);
            }
           
        }
    }
}