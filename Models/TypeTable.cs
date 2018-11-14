using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace PokemonPicker.Models
{
    public class TypeTable
    {
        public List<string> types = new List<string>(new string[] {"normal", "fighting", "flying", "poison", "ground","rock", "bug", "ghost", "steel", "fire", "water", "grass", "electric", "psychic","ice", "dragon", "dark", "fairy" });
        public List<string> weakness = new List<string>();
        public List<string> resistance = new List<string>();

        public TypeTable(string type1)
        {
            //just grab the types and we're good
            //https://pokeapi.co/api/v2/type/

            HttpWebRequest request = WebRequest.CreateHttp("http://pokeapi.co/api/v2/type/" + type1);

            //This line is commented out since Poke API otherwise throws a 403 error
            //request.UserAgent = @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());

            String data = rd.ReadToEnd();

            Pokemon p = new Pokemon(data);

        }

        public TypeTable(string type1, string type2)
        {
            //grab both from the array and calculate the relationships
        }
    }
}