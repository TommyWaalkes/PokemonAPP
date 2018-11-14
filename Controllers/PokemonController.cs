using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PokemonPicker.Models;

namespace PokemonPicker.Controllers
{
    public class PokemonController : Controller
    {
        // GET: Pokemon
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BattleSetup()
        {
            return View();
        }
        public ActionResult BattleResult()
        {
            Pokemon p = (Pokemon)Session["winner"];
            ViewBag.winner = p.Name;
            return View();
        }

        public ActionResult Battle(int poke1, int poke2)
        {
            Pokemon p1 = GetPokemon(poke1);
            Pokemon p2 = GetPokemon(poke2);
            Session["p1"] = p1;
            Session["p2"] = p2;
            List<Pokemon> pokes = new List<Pokemon>();
            pokes.Add(p1);
            pokes.Add(p2);
            ViewBag.pl = pokes;
            return View();
        }

        public Pokemon UpDateHp(Pokemon attacker, Pokemon defender, Random r)
        {
            int critRoll = r.Next(1, 101);
            int crit = 1;
            if (attacker.Fainted == false && defender.Fainted == false )
            {
                if (critRoll > 94)
                {
                    crit = 2;
                }
                double damage = 1;
                if (attacker.Attack > attacker.SpecialAttack)
                {
                    //Normal Attack
                    damage = (attacker.Attack / defender.Defense) * r.Next(70, 100) + 1;

                }
                else
                {
                    //Special Attack
                    damage = (attacker.SpecialAttack / defender.SpecialDefense) * r.Next(70, 100) + 1;
                }
                defender.Hp -= (int)Math.Ceiling(damage / 10) * crit;
                if (defender.Hp <= 0)
                {
                    defender.Fainted = true;
                }
            }
            return defender;
        }

        public ActionResult Fight()
        {
            if(Session["p1"] != null && Session["p2"] != null)
            {
                Pokemon p1 = (Pokemon) Session["p1"];
                Pokemon p2 = (Pokemon) Session["p2"];

                if (p1.Fainted == false && p2.Fainted == false)
                {
                    Random r = new Random();
                    if (p1.Speed > p2.Speed)
                    {
                        Session["p2"] = UpDateHp(p1, p2, r);
                        Session["p1"] = UpDateHp(p2, p1, r);
                    }
                    else
                    {
                        Session["p1"] = UpDateHp(p2, p1, r);
                        Session["p2"] = UpDateHp(p1, p2, r);
                    }

                    List<Pokemon> pokes = new List<Pokemon>();

                    pokes.Add((Pokemon)Session["p1"]);
                    pokes.Add((Pokemon)Session["p2"]);

                    ViewBag.pl = pokes;
                }
                if (p1.Fainted)
                {
                    ViewBag.Output = p2.Name + " wins!";
                    Session["winner"] = p2;
                    return RedirectToAction("BattleResult");
                }
                else if(p2.Fainted)
                {
                    ViewBag.Output = p1.Name + " wins!";
                    Session["winner"] = p1;
                    return RedirectToAction("BattleResult");
                }
            }
            else
            {
                ViewBag.Output = "Error: One or both of the pokemon weren't retrieved from the API";
            }
            return View();
        }

        public Pokemon GetPokemon(int pokeNum)
        {
            HttpWebRequest request = WebRequest.CreateHttp("http://pokeapi.co/api/v2/pokemon/" + pokeNum);

            //This line is commented out since Poke API otherwise throws a 403 error
            //request.UserAgent = @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());

            String data = rd.ReadToEnd();
          
            Pokemon p = new Pokemon(data);

            return p;
        }

        public ActionResult Result(int pokeNum)
        {
            Pokemon p = GetPokemon(pokeNum);
            ViewBag.img = p.ImageURL;
            //JObject o = JObject.Parse(data);

            //string name = o["name"].ToString();

            //ViewBag.name = name;
            //ViewBag.ImgSrc = o["sprites"]["front_default"].ToString();
            //ViewBag.Type1 = o["types"][0]["type"]["name"];
            //try
            //{
            //    ViewBag.Type2 = o["types"][1]["type"]["name"];
            //}
            //catch(Exception e)
            //{

            //}
            //Hashtable ht = new Hashtable();
            //List<int> stats = new List<int>();
            //for(int i = 0; i < o["stats"].Count(); i++)
            //{
            //    string statName = o["stats"][i]["stat"]["name"].ToString();

            //    int stat = int.Parse(o["stats"][i]["base_stat"].ToString());

            //    ht.Add(statName, stat);
            //}

            //ViewBag.Speed = ht["speed"];
            //ViewBag.SpecialDef = ht["special-defense"];
            //ViewBag.SpecialAttack = ht["special-attack"];
            //ViewBag.Defense = ht["defense"];
            //ViewBag.Attack = ht["attack"];
            //ViewBag.Hp = ht["hp"];
            //ViewBag.Num = pokeNum;
 
            return View(p);
        }
    }
}