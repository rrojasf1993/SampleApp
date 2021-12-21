using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace NbaPlayers
{
    public class NbaPlayer
    {
        public string first_name { get; set; }

        public string last_name { get; set; }

        public string h_in { get; set; }

        public string h_meters { get; set; }
    }

    public class Response
    {
        public List<NbaPlayer> values { get; set; }
    }


    public class Util
    {
        public  List<NbaPlayer> GetNbaPlayers()
        {
            List<NbaPlayer> players = null;
            var request = WebRequest.Create("https://mach-eight.uc.r.appspot.com/");
            using (WebResponse response = request.GetResponse())
            {
                //Console.WriteLine(response);
                using (var responseStream = response.GetResponseStream())
                {
                    using (StreamReader str = new StreamReader(responseStream))
                    {
                        string rawResp = str.ReadToEnd();
                        players = JsonConvert.DeserializeObject<Response>(rawResp).values;
                    }
                }
            }
            return players;
        }

        public  List<NbaPlayer> GetNbaPlayersWithHeigth(int heigth, List<NbaPlayer> players)
        {
            return players.Where((p => Convert.ToInt32(p.h_in) == heigth)).ToList();
        }
    }

    class Program
    {
        


        static void Main(string[] args)
        {
            int height = 0;
            Console.WriteLine("Write Height");
            height = Convert.ToInt32(Console.ReadLine());
            Util util = new Util(); 
            var playersWithHeight=util.GetNbaPlayersWithHeigth(height, util.GetNbaPlayers());
            if (playersWithHeight.Any())
            {
                for (int i = 0; i < playersWithHeight.Count() - 1; i++)
                {
                    Console.WriteLine($"{playersWithHeight.ElementAt(i).first_name} {playersWithHeight.ElementAt(i).last_name} - {playersWithHeight.ElementAt(i + 1).first_name} {playersWithHeight.ElementAt(i + 1).last_name}  {playersWithHeight.ElementAt(i).h_in} ");
                }
            }
            else
            {
                Console.WriteLine("No Items");
            }
            Console.ReadLine();
        }
    }
}
